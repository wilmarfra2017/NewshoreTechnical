using MediatR;
using Newshore.Technical.Domain.ResponseModels;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Domain.Queries.Journeys
{
    public class GetJourneyListByPlacesService : IRequest<List<JourneyResponse>?>
    {
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
    }

    public class GetJourneyListByPlacesQueryHandler : IRequestHandler<GetJourneyListByPlacesService, List<JourneyResponse>?>
    {        
        private readonly IJourneyFinder _finder;        
        public GetJourneyListByPlacesQueryHandler(IJourneyFinder finder)
        {
            _finder = finder;
        }
        
        public async Task<List<JourneyResponse>?> Handle(GetJourneyListByPlacesService request, CancellationToken cancellationToken)
        {
            Log.Information($"GetJourneyListByPlacesService -- Start --> Origin: {request.Origin}, Destination: {request.Destination}");
            List<JourneyResponse>? result = null;
            try
            {
                List<Journey>? journeyList = await _finder.GetListByPlaces(request.Origin, request.Destination);
                if (journeyList != null && journeyList.Any())
                {
                    result = new();
                    journeyList.ForEach(journeyInfo =>
                    {
                        result.Add(new JourneyResponse()
                        {
                            Destination = journeyInfo.Destination,
                            Id = journeyInfo.Id,
                            Origin = journeyInfo.Origin,
                            Price = journeyInfo.Price,
                            IsDirectFlight = journeyInfo.IsDirectFlight,
                            IsRoundTripFlight = journeyInfo.IsRoundTripFlight
                        });
                    });
                    Log.Information($"GetJourneyListByPlacesService -- Success --> Origin: {request.Origin}, Destination: {request.Destination} -- Journey founds");
                }
                else
                {
                    Log.Warning($"GetJourneyListByPlacesService -- Success --> Origin: {request.Origin}, Destination: {request.Destination} -- Journeys not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("GetJourneyListByPlacesService -- Error --> Origin: {Origin}, Destination: {Destination}", ex, request.Origin, request.Destination);
                throw;
            }
            return result;
        }        
    }
}
