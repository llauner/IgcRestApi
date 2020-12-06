using IgcRestApi.DataConversion;
using IgcRestApi.Dto;
using IgcRestApi.Models;
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
        private readonly IDataConverter _dataConverter;
        private readonly IAggregatorService _aggregatorService;


        public DefaultController(ILogger<DefaultController> logger,
                                IConfigurationService configuration,
                                IDataConverter dataConverter,
                                IAggregatorService aggregatorService)
        {
            _logger = logger;
            _configuration = configuration;
            _dataConverter = dataConverter;
            _aggregatorService = aggregatorService;
        }

        /// <summary>
        /// Ping
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PingResponse Ping()
        {
            return new PingResponse("IgcRestApi");
        }


        /// <summary>
        /// GetNetcoupeFlightsFromFtp
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetNetcoupeFlightsFromFtpAsync()
        {
            var processedFilesList = await _aggregatorService.RunAsync(10);

            return Ok(new ApiResponseModel(HttpStatusCode.OK, processedFilesList));
        }


        /// <summary>
        /// DeleteFlightAsync
        /// </summary>
        [HttpDelete("{flightNumber}")]
        public async Task<IActionResult> DeleteFlightAsync(int flightNumber)
        {
            var igcFlightDto = await _aggregatorService.DeleteFlightAsync(flightNumber);
            var igcFlightModel = _dataConverter.Convert<IgcFlightModel>(igcFlightDto);
            return Ok(new ApiResponseModel(HttpStatusCode.OK, igcFlightModel));
        }

    }
}
