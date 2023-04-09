using MediatR;
using Newshore.Technical.Domain.ResponseModels;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Domain.Queries.Journeys
{
    public class GetJourneyListByFlightService : IRequest<List<JourneyResponse>?>
    {
        public int FlightId { get; set; }
    }

    public class GetJourneyListByJourneyQueryHandler : IRequestHandler<GetJourneyListByFlightService, List<JourneyResponse>?>
    {        
        private readonly IJourneyFinder _finder;                
        public GetJourneyListByJourneyQueryHandler(IJourneyFinder finder)
        {
            _finder = finder;
        }
                
        public async Task<List<JourneyResponse>?> Handle(GetJourneyListByFlightService request, CancellationToken cancellationToken)
        {
            Log.Information($"GetJourneyListByFlightService -- Start --> flight id: {request.FlightId}");
            List<JourneyResponse>? result = null;
            try
            {
                List<Journey>? journeyList = await _finder.GetListByFlightId(request.FlightId);
                if (journeyList != null)
                {
                    result = new();
                    journeyList.ForEach(journeyInfo =>
                    {
                        result.Add(new JourneyResponse()
                        {
                            Destination = journeyInfo.Destination,
                            Id = journeyInfo.Id,
                            Origin = journeyInfo.Origin,
                            Price = journeyInfo.Price
                        });
                    });

                    Log.Information($"GetJourneyListByFlightService -- Success --> flight id: {request.FlightId} -- Journeys founds");
                }
                else
                {
                    Log.Warning($"GetJourneyListByFlightService -- Success --> flight id: {request.FlightId} -- Journeys not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("GetJourneyListByFlightService -- Error --> flight id: {Flight}", ex, request.FlightId);
                throw;
            }
            return result;
        }        
    }
}
