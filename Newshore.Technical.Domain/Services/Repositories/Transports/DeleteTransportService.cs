using MediatR;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Newshore.Technical.Domain.Commands.Transports
{
    public class DeleteTransportService : IRequest
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FlightCarrier { get; set; } = string.Empty;
        [MaxLength(50)]
        public string FlightNumber { get; set; } = string.Empty;
    }

    public class DeleteTransportCommandHandler : IRequestHandler<DeleteTransportService>
    {        
        private readonly ITransportRepository _repository;                
        public DeleteTransportCommandHandler(ITransportRepository repository)
        {
            _repository = repository;
        }
                
        public async Task<Unit> Handle(DeleteTransportService request, CancellationToken cancellationToken)
        {
            Log.Information("DeleteTransportService -- Start --> Transport Info: {@TransportInfo}", request);
            try
            {
                Transport transportInfo = new()
                {
                    Id = request.Id,
                    FlightCarrier = request.FlightCarrier,
                    FlightNumber = request.FlightNumber
                };
                bool result = await _repository.Delete(transportInfo);
                Log.Information("DeleteTransportService -- Success: {@result} --> Transport Info: {@TransportInfo}", result, request);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("DeleteTransportService -- Error --> Transport Info: {@TransportInfo}", ex, request);
                throw;
            }
            return Unit.Value;
        }        
    }
}
