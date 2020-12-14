using Google.Cloud.Firestore;
using IgcRestApi.Services.Interfaces;
using System.Collections.Generic;

namespace IgcRestApi.Services
{
    public class FirestoreNetcoupeExtractorNetcoupeExtractorService : IFirestoreNetcoupeExtractorService
    {
        private readonly IConfigurationService _configuration;
        private readonly FirestoreDb _firestoreDb = null;

        public FirestoreNetcoupeExtractorNetcoupeExtractorService(IConfigurationService configuration)
        {
            _configuration = configuration;
            _firestoreDb = FirestoreDb.Create("igcheatmap");
        }

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
            var docRef = GetDocumentRef();
            var updatedDocument = new Dictionary<string, object>
            {
                { _configuration.FirestorFieldLastProcessedFile, lastProcessedFilename },
            };
            docRef.UpdateAsync(updatedDocument).GetAwaiter().GetResult();
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




    }
}
