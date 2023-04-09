using MediatR;
using Newshore.Technical.Domain.ResponseModels;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Domain.Queries.Journeys
{
    public class GetAllJourneysService : IRequest<List<JourneyResponse>?>
    {
    }

    public class GetAllJourneysQueryHandler : IRequestHandler<GetAllJourneysService, List<JourneyResponse>?>
    {        
        private readonly IJourneyFinder _finder;                
        public GetAllJourneysQueryHandler(IJourneyFinder finder)
        {
            _finder = finder;
        }
                
        public async Task<List<JourneyResponse>?> Handle(GetAllJourneysService request, CancellationToken cancellationToken)
        {
            Log.Information("GetAllJourneysService -- Start");
            List<JourneyResponse>? result = null;
            try
            {
                List<Journey>? journeyList = await _finder.GetAll();
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
                    Log.Information("GetAllJourneysService -- Success -- Journey founds");
                }
                else
                {
                    Log.Warning($"GetAllJourneysService -- Success -- Journeys not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("GetAllJourneysService -- Error", ex);
                throw;
            }
            return result;
        }
        
    }
}
