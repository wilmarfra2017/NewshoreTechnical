using MediatR;
using Newshore.Technical.Domain.ResponseModels;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Domain.Queries.Transports
{
    public class GetTransportListByFlightService : IRequest<List<TransportResponse>?>
    {
        public int FlightId { get; set; }
    }

    public class GetTransportListByTransportQueryHandler : IRequestHandler<GetTransportListByFlightService, List<TransportResponse>?>
    {        
        private readonly ITransportFinder _finder;                
        public GetTransportListByTransportQueryHandler(ITransportFinder finder)
        {
            _finder = finder;
        }
                
        public async Task<List<TransportResponse>?> Handle(GetTransportListByFlightService request, CancellationToken cancellationToken)
        {
            Log.Information($"GetTransportListByFlightService -- Start --> flight id: {request.FlightId}");
            List<TransportResponse>? result = null;
            try
            {
                List<Transport>? transportList = await _finder.GetListByFlightId(request.FlightId);
                if (transportList != null)
                {
                    result = new();
                    transportList.ForEach(transportInfo =>
                    {
                        result.Add(new TransportResponse()
                        {
                            FlightCarrier = transportInfo.FlightCarrier,
                            FlightNumber = transportInfo.FlightNumber,
                            Id = transportInfo.Id
                        });
                    });
                    Log.Information($"GetTransportListByFlightService -- Success --> flight id: {request.FlightId} -- Transports founds");
                }
                else
                {
                    Log.Warning($"GetTransportListByFlightService -- Success --> flight id: {request.FlightId} -- Transports not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("GetTransportListByFlightService -- Error --> flight id: {Flight}", ex, request.FlightId);
                throw;
            }
            return result;
        }        
    }
}
