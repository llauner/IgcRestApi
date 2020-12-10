using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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

        Task DeleteFileAsync(string filename);
        IList<string> GetFilenameList();
    }
}