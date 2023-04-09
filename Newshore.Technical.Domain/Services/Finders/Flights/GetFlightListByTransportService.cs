using MediatR;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Domain.ResponseModels;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Domain.Queries.Flights
{
    public class GetFlightListByTransportService : IRequest<List<FlightResponse>?>
    {
        public int TransportId { get; set; }
    }

    public class GetFlightListByTransportQueryHandler : IRequestHandler<GetFlightListByTransportService, List<FlightResponse>?>
    {        
        private readonly IFlightFinder _finder;                
        public GetFlightListByTransportQueryHandler(IFlightFinder finder)
        {
            _finder = finder;
        }
                
        public async Task<List<FlightResponse>?> Handle(GetFlightListByTransportService request, CancellationToken cancellationToken)
        {
            Log.Information($"GetFlightListByTransportService -- Start --> transport id: {request.TransportId}");
            List<FlightResponse>? result = null;
            try
            {
                List<Flight>? flightList = await _finder.GetListByTransportId(request.TransportId);
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
                    Log.Information($"GetFlightListByTransportService -- Success --> transport id: {request.TransportId} -- Flight founds");
                }
                else
                {
                    Log.Warning($"GetFlightListByTransportService -- Success --> transport id: {request.TransportId} -- Flights not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("GetFlightListByTransportService -- Error --> transport id: {TransportId}", ex, request.TransportId);
                throw;
            }
            return result;
        }        
    }
}
