using MediatR;
using Newshore.Technical.Domain.ResponseModels;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Domain.Queries.Transports
{
    public class GetAllTransportsService : IRequest<List<TransportResponse>?>
    {
    }
    public class GetAllTransportsQueryHandler : IRequestHandler<GetAllTransportsService, List<TransportResponse>?>
    {        
        private readonly ITransportFinder _finder;                
        public GetAllTransportsQueryHandler(ITransportFinder finder)
        {
            _finder = finder;
        }
                
        public async Task<List<TransportResponse>?> Handle(GetAllTransportsService request, CancellationToken cancellationToken)
        {
            Log.Information("GetAllTransportsService -- Start");
            List<TransportResponse>? result = null;
            try
            {
                List<Transport>? transportList = await _finder.GetAll();
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

                    Log.Information("GetAllTransportsService -- Success -- Transport founds");
                }
                else
                {
                    Log.Warning($"GetAllTransportsService -- Success -- Transports not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Domain Get all transport -- Error", ex);
                throw;
            }
            return result;
        }        
    }
}
