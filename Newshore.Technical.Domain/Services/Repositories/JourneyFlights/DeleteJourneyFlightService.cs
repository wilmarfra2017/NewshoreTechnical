using MediatR;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Domain.Commands.JourneyFlights
{
    public class DeleteJourneyFlightService : IRequest
    {
        public int Id { get; set; }
        public int JourneyId { get; set; }
        public int FlightId { get; set; }
    }

    public class DeleteJourneyFlightCommandHandler : IRequestHandler<DeleteJourneyFlightService>
    {        
        private readonly IJourneyFlightRepository _repository;
                
        public DeleteJourneyFlightCommandHandler(IJourneyFlightRepository repository)
        {
            _repository = repository;
        }
                
        public async Task<Unit> Handle(DeleteJourneyFlightService request, CancellationToken cancellationToken)
        {
            Log.Information("DeleteJourneyFlightService -- Start --> Journey Flight Info: {@JourneyFlightInfo}", request);
            try
            {
                JourneyFlight journeyFlightInfo = new()
                {
                    Id = request.Id,
                    JourneyId = request.JourneyId,
                    FlightId = request.FlightId
                };
                bool result = await _repository.Delete(journeyFlightInfo);
                Log.Information("DeleteJourneyFlightService -- Success: {@result} --> Journey Flight Info: {@JourneyFlightInfo}", result, request);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("DeleteJourneyFlightService -- Error --> Journey Flight Info: {@JourneyFlightInfo}", ex, request);
                throw;
            }
            return Unit.Value;
        }
        
    }
}
