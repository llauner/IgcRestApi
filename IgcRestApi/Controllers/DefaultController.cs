using IgcRestApi.Dto;
using IgcRestApi.Exceptions;
using IgcRestApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PingResponse Get()
        {
            //_aggregatorService.RunAsync();
            return new PingResponse("IgcRestApi");
        }


        /// <summary>
        /// DeleteFlight
        /// </summary>
        [HttpDelete("{flightNumber}")]
        public async Task<IgcFlightDto> DeleteFlight(int flightNumber)
        {
            //var igcFlightDto = await _aggregatorService.DeleteFlight(flightNumber);

            throw new CoreApiException(HttpStatusCode.NotFound, $"Cannot find flight with flightNumber={flightNumber}");

            //if (igcFlightDto == null)
            //{
            //    return null;
            //}
            //else
            //{
            //    return null;
            //}

        }

    }
}
