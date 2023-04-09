using MediatR;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Newshore.Technical.Domain.Commands.Transports
{
    public class UpdateTransportService : IRequest
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FlightCarrier { get; set; } = string.Empty;
        [MaxLength(50)]
        public string FlightNumber { get; set; } = string.Empty;
    }

    public class UpdateTransportCommandHandler : IRequestHandler<UpdateTransportService>
    {        
        private readonly ITransportRepository _repository;
                
        public UpdateTransportCommandHandler(ITransportRepository repository)
        {
            _repository = repository;
        }
                
        public async Task<Unit> Handle(UpdateTransportService request, CancellationToken cancellationToken)
        {
            Log.Information("UpdateTransportService -- Start --> Transport Info: {@TransportInfo}", request);
            try
            {
                Transport transportInfo = new()
                {
                    Id = request.Id,
                    FlightCarrier = request.FlightCarrier,
                    FlightNumber = request.FlightNumber
                };
                bool result = await _repository.Update(transportInfo);
                Log.Information("UpdateTransportService -- Success: {@result} --> Transport Info: {@TransportInfo}", result, request);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("UpdateTransportService -- Error --> Transport Info: {@TransportInfo}", ex, request);
                throw;
            }
            return Unit.Value;
        }        
    }
}
