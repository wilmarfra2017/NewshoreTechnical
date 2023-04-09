using MediatR;
using Newshore.Technical.Domain.ResponseModels;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Domain.Queries.Flights
{
    public class GetFlightByIdService : IRequest<FlightResponse?>
    {
        public int Id { get; set; }
    }
    public class GetFlightByIdQueryHandler : IRequestHandler<GetFlightByIdService, FlightResponse?>
    {        
        private readonly IFlightFinder _finder;               
        public GetFlightByIdQueryHandler(IFlightFinder finder)
        {
            _finder = finder;
        }                
        public async Task<FlightResponse?> Handle(GetFlightByIdService request, CancellationToken cancellationToken)
        {
            Log.Information("GetFlightByIdService -- Start --> Id: {@Id}]", request.Id);
            FlightResponse? result = null;
            try
            {
                Flight? flightInfo = await _finder.GetById(request.Id);
                if (flightInfo != null)
                {
                    result = new()
                    {
                        Destination= flightInfo.Destination,
                        Id= flightInfo.Id,
                        Origin= flightInfo.Origin,
                        Price = flightInfo.Price,
                        TransportId = flightInfo.TransportId
                    };
                    Log.Information("GetFlightByIdService -- Success --> Id: {@Id} -- Flight found: {@FlightInfo}", request.Id, result);
                }
                else
                {
                    Log.Warning($"GetFlightByIdService -- Success --> Id: {request.Id} -- Flight not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("GetFlightByIdService -- Error --> Id: {@Id}", ex, request.Id);
                throw;
            }
            return result;
        }        
    }
}
