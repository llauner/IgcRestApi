using IgcRestApi.DataConversion;
using IgcRestApi.Dto;
using IgcRestApi.Filters;
using IgcRestApi.Models;
using IgcRestApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace IgcRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NetcoupeController : ControllerBase
    {
        private readonly ILogger<NetcoupeController> _logger;
        private readonly IConfigurationService _configuration;
        private readonly IDataConverter _dataConverter;
        private readonly IAggregatorService _aggregatorService;
        private readonly IStorageService _storageService;
        private readonly IFirestoreService _firestoreService;


        public NetcoupeController(ILogger<NetcoupeController> logger,
                                IConfigurationService configuration,
                                IDataConverter dataConverter,
                                IAggregatorService aggregatorService,
                                IStorageService storageService,
                                IFirestoreService firestoreService)
        {
            _logger = logger;
            _configuration = configuration;
            _dataConverter = dataConverter;
            _aggregatorService = aggregatorService;
            _storageService = storageService;
            _firestoreService = firestoreService;
        }


        /// <summary>
        /// Ping with Jwt authentication required
        /// </summary>
        /// <returns></returns>
        [HttpGet("jwt")]
        [Authorize]
        public PingResponse PingJwt()
        {
            var username = User.Identity.Name;
            var msg = $"User is visiting jwt auth with token: {username}";
            _logger.LogInformation(msg);

            return new PingResponse(msg);
        }

        #region Flights
        /// <summary>
        /// GetNetcoupeFlightsFromFtp
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetNetcoupeFlightsFromFtpAsync([FromQuery(Name = "limit")] int? limit)
        {
            var processedFilesList = await _aggregatorService.RunAsync(limit);

            return Ok(new ApiResponseModel(HttpStatusCode.OK, processedFilesList));
        }


        /// <summary>
        /// GetStoredNetcoupeFlightsList
        /// </summary>
        /// <returns></returns>
        [HttpGet("flights")]
        [BasicAuth]
        public IActionResult GetStoredNetcoupeFlightsList()
        {
            var fileList = _storageService.GetFilenameList();
            return Ok(new ApiResponseModel(HttpStatusCode.OK, fileList));
        }


        /// <summary>
        /// DeleteFlightAsync
        /// </summary>
        [HttpDelete("flights/{flightNumber}")]
        public async Task<IActionResult> DeleteFlightAsync(int flightNumber)
        {
            var igcFlightDto = await _aggregatorService.DeleteFlightAsync(flightNumber);
            var igcFlightModel = _dataConverter.Convert<IgcFlightModel>(igcFlightDto);
            return Ok(new ApiResponseModel(HttpStatusCode.OK, igcFlightModel));
        }
        #endregion

        #region Cumulative Tracks
        /// <summary>
        /// GetDailyCumulativeTracksProcessedDays
        /// Get the available processed days for the daily cumulative tracks.
        /// Information is retrieved from a Firestore DB on GCP
        /// </summary>
        /// <returns></returns>
        [HttpGet("tracks")]
        public IActionResult GetDailyCumulativeTracksProcessedDays()
        {
            var tracksList = _firestoreService.GetCumulativeTrackBuilderProcessedDays();
            return Ok(new ApiResponseModel(HttpStatusCode.OK, tracksList));
        }

        /// <summary>
        /// GetDailyCumulativeTracksStatistics
        /// </summary>
        /// <returns></returns>
        [HttpGet("tracks/statistics")]
        public IActionResult GetDailyCumulativeTracksStatistics()
        {
            var statisticsDto = _firestoreService.GetCumulativeTrackBuilderStatistics();
            var statisticsModel = _dataConverter.Convert<List<CumulativeTracksStatModel>>(statisticsDto);

            return Ok(new ApiResponseModel(HttpStatusCode.OK, statisticsModel));
        }
        #endregion


    }
}
