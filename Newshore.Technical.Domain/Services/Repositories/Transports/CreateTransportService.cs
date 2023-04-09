using MediatR;
using Newshore.Technical.Domain.ResponseModels;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Newshore.Technical.Domain.Commands.Transports
{
    public class CreateTransportService : IRequest<TransportResponse>
    {
        [MaxLength(100)]
        public string FlightCarrier { get; set; } = string.Empty;

        [MaxLength(50)]
        public string FlightNumber { get; set; } = string.Empty;
    }

    public class CreateTransportCommandHandler : IRequestHandler<CreateTransportService, TransportResponse>
    {        
        private readonly ITransportRepository _repository;
        private readonly ITransportFinder _finder;
                
        public CreateTransportCommandHandler(ITransportRepository repository, ITransportFinder finder)
        {
            _repository = repository;
            _finder = finder;
        }
                
        public async Task<TransportResponse> Handle(CreateTransportService request, CancellationToken cancellationToken)
        {
            Log.Information("CreateTransportService -- Start --> Transport Info: {@TransportInfo}", request);
            TransportResponse result = null;
            try
            {
                Transport newTransport = new()
                {
                    FlightCarrier = request.FlightCarrier,
                    FlightNumber = request.FlightNumber
                };

                Transport? transportInfo = await _finder.GetExistsTransport(newTransport);
                if (transportInfo == null)
                {
                    transportInfo = await _repository.Create(newTransport);
                }
                Log.Information("CreateTransportService -- Success --> Transport Info: {@TransportInfo}", request);
                if (transportInfo != null)
                {
                    result = new()
                    {
                        Id = transportInfo.Id,
                        FlightCarrier = transportInfo.FlightCarrier,
                        FlightNumber = transportInfo.FlightNumber
                    };
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("CreateTransportService -- Error --> Transport Info: {@TransportInfo}", ex, request);
                throw;
            }
            return result;
        }        
    }
}
