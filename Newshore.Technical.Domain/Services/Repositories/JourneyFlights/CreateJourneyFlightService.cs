using MediatR;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Domain.Commands.JourneyFlights
{
    public class CreateJourneyFlightService : IRequest
    {
        public int JourneyId { get; set; }
        public int FlightId { get; set; }
    }

    public class CreateJourneyFlightCommandHandler : IRequestHandler<CreateJourneyFlightService>
    {        
        private readonly IJourneyFlightRepository _repository;                
        public CreateJourneyFlightCommandHandler(IJourneyFlightRepository repository)
        {
            _repository = repository;
        }
                
        public async Task<Unit> Handle(CreateJourneyFlightService request, CancellationToken cancellationToken)
        {
            Log.Information("CreateJourneyFlightService-- Start --> Journey flight Info: {@JourneyFlightInfo}", request);
            try
            {
                JourneyFlight newJourneyFlight = new()
                {
                    JourneyId = request.JourneyId,
                    FlightId = request.FlightId
                };
                JourneyFlight result = await _repository.Create(newJourneyFlight);
                Log.Information("CreateJourneyFlightService -- Success --> Journey Flight Info: {@JourneyFlightInfo}", result, request);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("CreateJourneyFlightService -- Error --> Journey Flight Info: {@JourneyFlightInfo}", ex, request);
                throw;
            }
            return Unit.Value;
        }        
    }
}
