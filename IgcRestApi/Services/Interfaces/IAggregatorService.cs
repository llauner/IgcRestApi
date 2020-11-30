using IgcRestApi.Dto;
using System.Threading.Tasks;

namespace IgcRestApi.Services
{
    public interface IAggregatorService
    {
        /// <summary>
        /// RunAsync
        /// Entry point for the igc extraction and storage
        /// </summary>
        void RunAsync();

        Task<IgcFlightDto> DeleteFlight(int flightNumber);
    }
}