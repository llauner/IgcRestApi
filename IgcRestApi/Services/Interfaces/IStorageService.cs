using System.IO;

namespace IgcRestApi.Services
{
    public interface IStorageService
    {
        /// <summary>
        /// UploadToBucket
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="inStream"></param>
        void UploadToBucket(string objectName, Stream inStream);

        void DeleteFileAsync(string filename);
    }
}