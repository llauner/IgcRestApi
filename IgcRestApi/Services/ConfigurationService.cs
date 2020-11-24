using Microsoft.Extensions.Configuration;

namespace IgcRestApi.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public string FtpNetcoupeIgcHost => _configuration?["FtpNetcoupeIgcHost"];
        public string FtpNetcoupeIgcUsername => _configuration?["FtpNetcoupeIgcUsername"];
        public string FtpNetcoupeIgcPassword => _configuration?["FtpNetcoupeIgcPassword"];

        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        
    }
}
