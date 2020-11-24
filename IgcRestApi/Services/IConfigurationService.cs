namespace IgcRestApi.Services
{
    public interface IConfigurationService
    {
        string FtpNetcoupeIgcHost { get; }
        string FtpNetcoupeIgcUsername { get; }
        string FtpNetcoupeIgcPassword { get; }
    }
}
