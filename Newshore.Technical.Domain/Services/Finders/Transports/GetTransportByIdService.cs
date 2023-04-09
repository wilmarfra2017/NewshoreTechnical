using MediatR;
using Newshore.Technical.Domain.ResponseModels;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Domain.Queries.Transports
{
    public class GetTransportByIdService : IRequest<TransportResponse?>
    {
        public int Id { get; set; }
    }

    public class GetTransportByIdQueryHandler : IRequestHandler<GetTransportByIdService, TransportResponse?>
    {        
        private readonly ITransportFinder _finder;                
        public GetTransportByIdQueryHandler(ITransportFinder finder)
        {
            _finder = finder;
        }
                
        public async Task<TransportResponse?> Handle(GetTransportByIdService request, CancellationToken cancellationToken)
        {
            Log.Information("GetTransportByIdService -- Start --> Id: {@Id}", request.Id);
            TransportResponse? result = null;
            try
            {
                Transport? transportInfo = await _finder.GetById(request.Id);
                if (transportInfo != null)
                {
                    result = new ()
                    {
                        FlightCarrier = transportInfo.FlightCarrier,
                        FlightNumber = transportInfo.FlightNumber,
                        Id = transportInfo.Id
                    };
                    Log.Information("GetTransportByIdService -- Success --> Id: {@Id} -- Transport found: {@TransportInfo}", request.Id, result);
                }
                else
                {
                    Log.Warning($"GetTransportByIdService -- Success --> Id: {request.Id} -- Transport not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("GetTransportByIdService -- Error --> Id: {@Id}", ex, request.Id);
                throw;
            }
            return result;
        }        
    }
}
