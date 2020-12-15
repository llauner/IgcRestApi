namespace IgcRestApi.Services.Interfaces
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

        public string FirestoreCollectionNameTracemapProgress { get; }
        public string FirestoreDocumentNameTracemapProgress { get; }
        public string FirestoreFieldNameTracemapProgress { get; }


        string StorageBucketName { get; }

        string ApiDefaultLogin { get; }
        string ApiDefaultPassword { get; }
    }
}
