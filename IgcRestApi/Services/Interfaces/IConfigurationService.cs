namespace IgcRestApi.Services
{
    public interface IConfigurationService
    {
        string FtpNetcoupeIgcHost { get; }
        string FtpNetcoupeIgcUsername { get; }
        string FtpNetcoupeIgcPassword { get; }


        string FirestoreCollectionName { get; }
        string FirestoreDocumentName { get; }
        string FirestorFieldLastProcessedFile { get; }
        public int StoreProgressInterval { get; }


        string StorageBucketName { get; }

        string ApiDefaultLogin { get; }
        string ApiDefaultPassword { get; }
    }
}
