using Newshore.Technical.Domain.Aggregates.Entities;

namespace Newshore.Technical.Infrastructure.Interfaces
{
    public interface ITransportFinder
    {
        public Task<Transport?> GetById(int journeyId);

        public Task<List<Transport>?> GetAll();

        public Task<List<Transport>?> GetListByFlightId(int flightId);

        public Task<Transport?> GetExistsTransport(Transport transportInfo);
    }
}
