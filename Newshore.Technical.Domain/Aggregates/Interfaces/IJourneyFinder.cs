using Newshore.Technical.Domain.Aggregates.Entities;

namespace Newshore.Technical.Infrastructure.Interfaces
{
    public interface IJourneyFinder
    {
        public Task<Journey?> GetById(int journeyId);

        public Task<List<Journey>?> GetAll();

        public Task<List<Journey>?> GetListByFlightId(int flightId);

        public Task<List<Journey>?> GetListByPlaces(string? origin, string? destination);
    }
}
