using IgcRestApi.Dto;
using IgcRestApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IgcRestApi.Controllers
{
    [ApiController]
    [Route("")]
    [Route("[controller]")]
    public class DefaultController : ControllerBase
    {
        

        private readonly ILogger<DefaultController> _logger;
        private readonly IConfigurationService _configuration;

        public DefaultController(ILogger<DefaultController> logger, IConfigurationService configuration)
        {
            _logger = logger;
            _configuration = configuration;

            var ftp = _configuration.FtpNetcoupeIgcHost;
            _logger.LogDebug(ftp);
            
        }

        [HttpGet]
        public PingResponse Get()
        {
            return new PingResponse("IgcRestApi");
        }
    }
}
