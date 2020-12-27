namespace IgcRestApi.Services.Interfaces
{
    public interface IConfigurationService
    {
        string FtpNetcoupeIgcHost { get; }
        string FtpNetcoupeIgcUsername { get; }
        string FtpNetcoupeIgcPassword { get; }

        string GcpProjectId { get; }
        string GcpSecretKeyIgcRestApiInternalApiKey { get; }
        string GcpSecretKeyIgcRestApiPubliclApiKey { get; }

        string FirestoreCollectionName { get; }
        string FirestoreDocumentName { get; }
        string FirestorFieldLastProcessedFile { get; }
        public int StoreProgressInterval { get; }

        public string FirestoreCollectionNameTracemapProgress { get; }
        public string FirestoreDocumentNameTracemapProgress { get; }


        public string FirestoreCollectionNameHeatmapProgress { get; }
        public string FirestoreDocumentNameHeatmapProgress { get; }


        public string FirestoreFieldNameProgress { get; }
        public string FirestoreFieldNameStatistics { get; }


        string StorageBucketName { get; }

        string ApiDefaultLogin { get; }
        string ApiDefaultPassword { get; }
    }
}
