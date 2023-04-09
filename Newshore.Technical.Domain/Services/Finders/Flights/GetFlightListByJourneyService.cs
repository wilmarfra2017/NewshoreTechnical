using MediatR;
using Newshore.Technical.Domain.ResponseModels;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Domain.Queries.Flights
{
    public class GetFlightListByJourneyService : IRequest<List<FlightResponse>?>
    {
        public int JourneyId { get; set; }
    }

    public class GetFlightListByJourneyQueryHandler : IRequestHandler<GetFlightListByJourneyService, List<FlightResponse>?>
    {        
        private readonly IFlightFinder _finder;                
        public GetFlightListByJourneyQueryHandler(IFlightFinder finder)
        {
            _finder = finder;
        }
               
        public async Task<List<FlightResponse>?> Handle(GetFlightListByJourneyService request, CancellationToken cancellationToken)
        {
            Log.Information($"GetFlightListByJourneyService -- Start --> journey id: {request.JourneyId}");
            List<FlightResponse>? result = null;
            try
            {
                List<Flight>? flightList = await _finder.GetListByJourney(request.JourneyId);
                if (flightList != null)
                {
                    result = new();
                    flightList.ForEach(flightInfo =>
                    {
                        result.Add(new FlightResponse()
                        {
                            Destination = flightInfo.Destination,
                            Id = flightInfo.Id,
                            Origin = flightInfo.Origin,
                            Price = flightInfo.Price,
                            TransportId = flightInfo.TransportId
                        });
                    });
                    Log.Information($"GetFlightListByJourneyService -- Success --> journey id: {request.JourneyId} -- Flight founds");
                }
                else
                {
                    Log.Warning($"GetFlightListByJourneyService -- Success --> journey id: {request.JourneyId} -- Flights not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("GetFlightListByJourneyService -- Error --> journey id: {JourneyId}", ex, request.JourneyId);
                throw;
            }
            return result;
        }        
    }
}
