using MediatR;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Domain.Aggregates.Interfaces;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Newshore.Technical.Domain.Commands.Flights
{
    public class DeleteFlightService : IRequest
    {
        public int Id { get; set; }
        [MaxLength(4)]
        public string Origin { get; set; } = string.Empty;

        [MaxLength(4)]
        public string Destination { get; set; } = string.Empty;
        public double Price { get; set; }
        public int TransportId { get; set; }
    }

    public class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightService>
    {        
        private readonly IFlightRepository _repository;                
        public DeleteFlightCommandHandler(IFlightRepository repository)
        {
            _repository = repository;
        }
                
        public async Task<Unit> Handle(DeleteFlightService request, CancellationToken cancellationToken)
        {
            Log.Information("DeleteFlightService -- Start --> Flight Info: {@FlightInfo}", request);
            try
            {
                Flight transportInfo = new()
                {
                    Id = request.Id,
                    Destination= request.Destination,
                    Origin = request.Origin,
                    Price = request.Price,
                    TransportId= request.TransportId
                };
                bool result = await _repository.Delete(transportInfo);
                Log.Information("DeleteFlightService -- Success: {@result} --> Flight Info: {@FlightInfo}", result, request);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("DeleteFlightService -- Error --> Flight Info: {@FlightInfo}", ex, request);
                throw;
            }
            return Unit.Value;
        }        
    }
}
