namespace IgcRestApi.Services.Interfaces
{
    public interface IFirestoreNetcoupeExtractorService
    {
        /// <summary>
        /// GetLastProcessedFile
        /// </summary>
        /// <returns></returns>
        string GetLastProcessedFile();

        void UpdateLastProcessedFile(string lastProcessedFilename);
    }
}