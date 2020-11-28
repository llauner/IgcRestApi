using Google.Cloud.Storage.V1;
using System.IO;

namespace IgcRestApi.Services
{
    public class StorageService : IStorageService
    {
        private readonly IConfigurationService _configuration;
        private readonly StorageClient _storageClient;

        public StorageService(IConfigurationService configuration)
        {
            _configuration = configuration;
            _storageClient = StorageClient.Create();
        }


        /// <summary>
        /// UploadToBucket
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="inStream"></param>
        public void UploadToBucket(string objectName, Stream inStream)
        {
            _storageClient.UploadObject(_configuration.StorageBucketName, objectName, "text/plain", inStream);
        }
    }
}
