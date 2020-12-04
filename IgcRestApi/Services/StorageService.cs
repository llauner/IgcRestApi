using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IgcRestApi.Services
{
    public class StorageService : IStorageService
    {
        private readonly ILogger _logger;
        private readonly IConfigurationService _configuration;
        private readonly StorageClient _storageClient;

        public StorageService(ILoggerFactory loggerFactory, IConfigurationService configuration)
        {
            _logger = loggerFactory.CreateLogger<StorageService>();
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


        /// <summary>
        /// DeleteFileAsync
        /// </summary>
        /// <param name="filename"></param>
        public async Task DeleteFileAsync(string filename)
        {
            var enumerable = _storageClient.ListObjects(_configuration.StorageBucketName);
            var list = enumerable.ToList();
            var fileFullPath = list.SingleOrDefault(o => o.Name.ToLower().Contains(filename.ToLower()));

            if (fileFullPath == null)
            {
                var message = $"Could not find file in GCP bucket: {filename}";
                _logger.LogError(message);
                throw new FileNotFoundException(message);
            }

            await _storageClient.DeleteObjectAsync(_configuration.StorageBucketName, fileFullPath.Name);
        }


    }
}
