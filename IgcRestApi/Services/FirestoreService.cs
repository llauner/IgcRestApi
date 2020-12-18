using Google.Cloud.Firestore;
using IgcRestApi.Dto;
using IgcRestApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace IgcRestApi.Services
{
    public class FirestoreService : IFirestoreService
    {
        private readonly IConfigurationService _configuration;
        private readonly FirestoreDb _firestoreDb = null;

        public FirestoreService(IConfigurationService configuration)
        {
            _configuration = configuration;
            _firestoreDb = FirestoreDb.Create("igcheatmap");
        }


        #region Netcoupe Extractor
        /// <summary>
        /// GetLastProcessedFile
        /// </summary>
        /// <returns></returns>
        public string GetLastProcessedFile()
        {
            var docRef = GetDocumentRef();
            var docSnapshot = docRef.GetSnapshotAsync().Result;
            var docDict = docSnapshot.ToDictionary();
            var lastProcessedFile = (string)docDict.GetValueOrDefault(_configuration.FirestorFieldLastProcessedFile, null);
            return lastProcessedFile;
        }


        /// <summary>
        /// UpdateLastProcessedFile
        /// </summary>
        /// <param name="lastProcessedFilename"></param>
        public void UpdateLastProcessedFile(string lastProcessedFilename)
        {
            if (!string.IsNullOrEmpty(lastProcessedFilename))
            {
                var docRef = GetDocumentRef();
                var updatedDocument = new Dictionary<string, object>
                {
                    { _configuration.FirestorFieldLastProcessedFile, lastProcessedFilename },
                };
                docRef.UpdateAsync(updatedDocument).GetAwaiter().GetResult();
            }

        }


        /// <summary>
        /// DocumentReference
        /// </summary>
        /// <returns></returns>
        private DocumentReference GetDocumentRef()
        {
            var igcRef = _firestoreDb.Collection(_configuration.FirestoreCollectionName);
            var docRef = igcRef.Document(_configuration.FirestoreDocumentName);
            return docRef;
        }
        #endregion

        #region Tracemap Progress

        /// <summary>
        /// GetCumulativeTrackBuilderProcessedDays
        /// List of processed days with data (hash is not null)
        /// </summary>
        /// <returns>Sorted list of processed days</returns>
        public List<string> GetCumulativeTrackBuilderProcessedDays()
        {
            var igcRef = _firestoreDb.Collection(_configuration.FirestoreCollectionNameTracemapProgress);
            var docRef = igcRef.Document(_configuration.FirestoreDocumentNameTracemapProgress);
            var docSnapshot = docRef.GetSnapshotAsync().Result;

            var processedDaysDict = docSnapshot.GetValue<Dictionary<string, string>>(_configuration.FirestoreFieldNameTracemapProgress);

            var listProcessedDays = processedDaysDict.Where(x => x.Value != null)
                                                    .Select(x => x.Key)
                                                    .ToList();

            listProcessedDays = listProcessedDays.OrderBy(f => f.Length).ThenBy(f => f).ToList();
            return listProcessedDays;
        }

        public List<CumulativeTracksStatDto> GetCumulativeTrackBuilderStatistics()
        {
            var igcRef = _firestoreDb.Collection(_configuration.FirestoreCollectionNameTracemapProgress);
            var docRef = igcRef.Document(_configuration.FirestoreDocumentNameTracemapProgress);
            var docSnapshot = docRef.GetSnapshotAsync().Result;

            var statisticsDic = docSnapshot.GetValue<Dictionary<string, int>>(_configuration.FirestoreFieldNameStatistics);
            var listStatistics = statisticsDic.Select(e => new CumulativeTracksStatDto(e.Key, e.Value)).ToList();


            listStatistics = listStatistics.OrderBy(f => f.Date.Length).ThenBy(f => f.Date).ToList();
            return listStatistics;
        }

        #endregion

    }
}
