﻿using IgcRestApi.Dto;
using IgcRestApi.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;

namespace IgcRestApi.Services
{
    public class AggregatorService : IAggregatorService
    {
        private readonly ILogger _logger;
        private readonly IConfigurationService _configuration;
        private readonly IFtpService _ftpService;
        private readonly IFirestoreService _firestoreService;
        private readonly IStorageService _storageService;
        private readonly IIgcReaderService _igcReaderService;
        private readonly INetcoupeService _netcoupeService;

        public AggregatorService(ILoggerFactory loggerFactory,
            IConfigurationService configuration,
            IFtpService ftpService,
            IFirestoreService fireStoreService,
            IStorageService storageService,
            IIgcReaderService igcReaderService,
            INetcoupeService netcoupeService)
        {
            _logger = loggerFactory.CreateLogger<AggregatorService>();
            _configuration = configuration;
            _ftpService = ftpService;
            _firestoreService = fireStoreService;
            _storageService = storageService;
            _igcReaderService = igcReaderService;
            _netcoupeService = netcoupeService;
        }


        /// <summary>
        /// RunAsync
        /// Entry point for the igc extraction and storage
        /// </summary>
        public async void
            RunAsync()
        {
            var lastProcessedFilename = _firestoreService.GetLastProcessedFile();
            var filesList = _ftpService.GetFileList();

            // Remove files already processed from list
            filesList.RemoveAll(o =>
                int.Parse(Path.GetFileNameWithoutExtension(o.Name)) <=
                int.Parse(Path.GetFileNameWithoutExtension(lastProcessedFilename)));

            // #### Process files ###
            var processedItemCount = 0;                 // Keep track of processed items # so that we can regularly store the progress
            var totalProcessedItemCount = 0;
            string lastProcessedFileName = null;
            foreach (var f in filesList)
            {
                _logger.LogInformation($"Dealing with: {f.Name}");

                // --- Get file from FTP ---
                var fileStream = _ftpService.DownloadFile(f.Name);
                fileStream.Seek(0, SeekOrigin.Begin);

                // --- Unzip stream file content into stream ---
                var archive = new ZipArchive(fileStream);
                var igcFile = archive.Entries[0];
                var unzippedStream = igcFile.Open();

                // --- Retrieve flight date ---
                var isProcessingDone = false;
                var targetFolderName = "";
                await using (var igcStream = igcFile.Open())
                {
                    try
                    {
                        var igcHeader = _igcReaderService.GetHeaderFromStream(igcStream);
                        targetFolderName = igcHeader.Date.ToString("yyyy_MM_dd") + "/";
                        isProcessingDone = true;
                        lastProcessedFileName = f.Name;
                        igcStream.Close();
                    }
                    catch (Exception e)
                    {
                        _logger.LogDebug($"Could not extract header from file:{e.Message}");
                        isProcessingDone = false;
                    }

                }

                if (isProcessingDone)
                {
                    // --- Store into a GCP bucket
                    _storageService.UploadToBucket(targetFolderName + igcFile.Name, unzippedStream);
                }

                // --- Store progress ---
                totalProcessedItemCount++;
                processedItemCount++;
                if (processedItemCount >= _configuration.StoreProgressInterval)
                {
                    // Store progress in GCP Firestore so that we don't go over all files next time
                    _firestoreService.UpdateLastProcessedFile(f.Name);
                    processedItemCount = 0;
                }

                // Clean up
                unzippedStream.Close();
                unzippedStream.Dispose();
                fileStream.Close();
                fileStream.Dispose();

                if (totalProcessedItemCount == 100)
                {
                    break;
                }
            }
            // --- Store last processed file progress
            _firestoreService.UpdateLastProcessedFile(lastProcessedFileName);
        }


        /// <summary>
        /// DeleteFlight
        /// </summary>
        /// <param name="flightNumber"></param>
        public async Task<IgcFlightDto> DeleteFlight(int flightNumber)
        {
            var filename = _netcoupeService.GetIgcFileNameById(flightNumber);

            try
            {
                await _storageService.DeleteFileAsync(filename);
            }
            catch (FileNotFoundException e)
            {
                throw new CoreApiException(HttpStatusCode.NotFound, e.Message);
            }


            var flightInfo = new IgcFlightDto()
            {
                Id = flightNumber,
                Name = filename,
                Status = FlightStatus.DELETED
            };

            return flightInfo;
        }




    }
}
