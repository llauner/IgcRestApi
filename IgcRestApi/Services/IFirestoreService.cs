namespace IgcRestApi.Services
{
    public interface IFirestoreService
    {
        /// <summary>
        /// GetLastProcessedFile
        /// </summary>
        /// <returns></returns>
        string GetLastProcessedFile();

        void UpdateLastProcessedFile(string lastProcessedFilename);
    }
}