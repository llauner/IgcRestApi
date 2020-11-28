using Microsoft.Extensions.Configuration;
using System;

namespace IgcRestApi.Services
{
    public class ConfigurationService : IConfigurationService
    {
        // ########## FTP IGC ##########
        public string FtpNetcoupeIgcHost => _configuration?["FtpNetcoupeIgcHost"];
        public string FtpNetcoupeIgcUsername => _configuration?["FtpNetcoupeIgcUsername"];
        public string FtpNetcoupeIgcPassword => _configuration?["FtpNetcoupeIgcPassword"];

        // ########## Firestore ##########
        public string FirestoreCollectionName => GetSetting("firestoreCollectionName", "igc");
        public string FirestoreDocumentName => GetSetting("firestoreDocumentName", "NetcoupeIgcExtractor");
        public string FirestorFieldLastProcessedFile => GetSetting("firestorFieldLastProcessedFile", "lastProcessedFile");
        public int StoreProgressInterval => GetSetting("StoreProgressInterval", 10);

        // ########## Storage Bucket ##########
        public string StorageBucketName => GetSetting("StorageBucketName", "netcoupe-igc-" + DateTime.Now.Year);

        #region Configuration Service
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// GetSetting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private T GetSetting<T>(string key, T defaultValue = default(T)) where T : IConvertible
        {
            var val = _configuration?[key];
            if (string.IsNullOrEmpty(val))
                val = Environment.GetEnvironmentVariable(key);

            val = val ?? "";

            T result = defaultValue;
            if (!string.IsNullOrEmpty(val))
            {
                var typeDefault = default(T);
                if (typeof(T) == typeof(string))
                {
                    typeDefault = (T)(object)string.Empty;
                }
                result = (T)Convert.ChangeType(val, typeDefault.GetTypeCode());
            }
            return result;
        }
        #endregion

    }
}
