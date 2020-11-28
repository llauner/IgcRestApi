using System.IO;
using DotIGC;

namespace IgcRestApi.Services
{
    public interface IIgcReaderService
    {
        IgcDocumentHeader GetHeader(string filePath);

        /// <summary>
        /// GetHeaderFromZip
        /// </summary>
        /// <param name="zipFilePath"></param>
        /// <returns></returns>
        IgcDocumentHeader GetHeaderFromZip(string zipFilePath);

        /// <summary>
        /// GetHeaderFromStream
        /// </summary>
        /// <param name="igcStream"></param>
        /// <returns></returns>
        IgcDocumentHeader GetHeaderFromStream(Stream igcStream);
    }
}