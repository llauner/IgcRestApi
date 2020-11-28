﻿using FluentFTP;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace IgcRestApi.Services
{
    public class FtpService : IFtpService
    {
        private readonly IConfigurationService _configuration;
        private FtpClient _client = null;

        public FtpService(IConfigurationService configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// GetFileList
        /// </summary>
        /// <returns></returns>
        public List<FtpListItem> GetFileList()
        {
            InitClient();
            var fileList = _client.GetListing("/");

            // Sort list by Alphabetical order on the file name
            var sortedFileList = fileList.OrderBy(f => f.Name.Length).ThenBy(f => f.Name).ToList();

            return sortedFileList;
        }

        /// <summary>
        /// DownloadFile
        /// </summary>
        /// <param name="remoteFileName"></param>
        /// <returns></returns>
        public Stream DownloadFile(string remoteFileName)
        {
            var outStream = new MemoryStream();
            return _client.Download(outStream, remoteFileName) ? outStream : null;
        }

        /// <summary>
        /// InitClient
        /// </summary>
        private void InitClient()
        {
            if (_client == null || !_client.IsConnected)
            {
                _client = new FtpClient(_configuration.FtpNetcoupeIgcHost)
                {
                    Credentials = new NetworkCredential(_configuration.FtpNetcoupeIgcUsername, _configuration.FtpNetcoupeIgcPassword)
                };
            }

        }



    }
}
