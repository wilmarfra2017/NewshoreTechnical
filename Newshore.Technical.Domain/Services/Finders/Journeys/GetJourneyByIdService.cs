using MediatR;
using Newshore.Technical.Domain.ResponseModels;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Domain.Queries.Journeys
{
    public class GetJourneyByIdService : IRequest<JourneyResponse?>
    {
        public int Id { get; set; }
    }

    public class GetJourneyByIdQueryHandler : IRequestHandler<GetJourneyByIdService, JourneyResponse?>
    {        
        private readonly IJourneyFinder _finder;                
        public GetJourneyByIdQueryHandler(IJourneyFinder finder)
        {
            _finder = finder;
        }
                
        public async Task<JourneyResponse?> Handle(GetJourneyByIdService request, CancellationToken cancellationToken)
        {
            Log.Information("GetJourneyByIdService -- Start --> Id: {@Id}]", request.Id);
            JourneyResponse? result = null;
            try
            {
                Journey? journeyInfo = await _finder.GetById(request.Id);
                if (journeyInfo != null)
                {
                    result = new()
                    {
                        Destination= journeyInfo.Destination,
                        Id= journeyInfo.Id,
                        Origin= journeyInfo.Origin,
                        Price = journeyInfo.Price
                    };

                    Log.Information("GetJourneyByIdService -- Success --> Id: {@Id} -- Journey found: {@JourneyInfo}", request.Id, result);
                }
                else
                {
                    Log.Warning($"GetJourneyByIdService -- Success --> Id: {request.Id} -- Journey not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("GetJourneyByIdService -- Error --> Id: {@Id}", ex, request.Id);
                throw;
            }
            return result;
        }        
    }
}
