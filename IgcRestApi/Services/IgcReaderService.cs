using DotIGC;

namespace IgcRestApi.Services
{
    public class IgcReaderService
    {

        public IgcDocumentHeader GetHeader(string filePath)
        {
            var header = IgcDocumentHeader.Load(filePath);
            return header;
        }
    }
}
