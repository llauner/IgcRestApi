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
        private readonly IAggregatorService _aggregatorService;


        public DefaultController(ILogger<DefaultController> logger, IConfigurationService configuration, IAggregatorService aggregatorService)
        {
            _logger = logger;
            _configuration = configuration;
            _aggregatorService = aggregatorService;
        }

        [HttpGet]
        public PingResponse Get()
        {
            _aggregatorService.Run();
            return new PingResponse("IgcRestApi");
        }
    }
}
