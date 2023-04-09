
using Newshore.Technical.Transverse.Dto;

namespace Newshore.Technical.Domain.Interfaces
{
    public interface IJourneyDomain
    {
        public Task<List<JourneyDto>?> FindItinerariesFromOriginToDestination(string origin, string destination);
    }
}
