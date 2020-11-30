using System;


namespace IgcRestApi.Services
{
    public class NetcoupeService : INetcoupeService
    {
        private static readonly string _netcoupeFileName = "Netcoupe{0}_{1}.igc";
        private static readonly string IgcSuffix = ".igc";

        /// <summary>
        /// GetIgcFileNameById
        /// </summary>
        /// <param name="netcoupeFlightId"></param>
        /// <param name="currentYear"></param>
        /// <returns></returns>
        public string GetIgcFileNameById(int netcoupeFlightId, int? currentYear = null)
        {
            currentYear = (currentYear == null) ? DateTime.Now.Year : currentYear;
            var netcoupeIgcFilename = string.Format(_netcoupeFileName, currentYear, netcoupeFlightId);

            return netcoupeIgcFilename;
        }
    }
}
