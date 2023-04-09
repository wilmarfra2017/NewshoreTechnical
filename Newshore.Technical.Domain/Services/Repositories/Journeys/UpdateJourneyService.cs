using MediatR;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Newshore.Technical.Domain.Commands.Journeys
{
    public class UpdateJourneyService : IRequest
    {
        public int Id { get; set; }

        [MaxLength(4)]
        public string Origin { get; set; } = string.Empty;

        [MaxLength(4)]
        public string Destination { get; set; } = string.Empty;
        public double Price { get; set; }
        public bool? IsDirectFlight { get; set; }
        public bool? IsRoundTripFlight { get; set; }
    }

    public class UpdateJourneyCommandHandler : IRequestHandler<UpdateJourneyService>
    {        
        private readonly IJourneyRepository _repository;
                
        public UpdateJourneyCommandHandler(IJourneyRepository repository)
        {
            _repository = repository;
        }
                
        public async Task<Unit> Handle(UpdateJourneyService request, CancellationToken cancellationToken)
        {
            Log.Information("UpdateJourneyService -- Start --> Journey Info: {@JourneyInfo}", request);
            try
            {
                Journey transportInfo = new()
                {
                    Id = request.Id,
                    Destination = request.Destination,
                    Origin = request.Origin,
                    Price = request.Price,
                    IsDirectFlight = request.IsDirectFlight,
                    IsRoundTripFlight = request.IsRoundTripFlight
                };
                bool result = await _repository.Update(transportInfo);
                Log.Information("UpdateJourneyService -- Success: {@result} --> Journey Info: {@JourneyInfo}", result, request);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("UpdateJourneyService -- Error --> Journey Info: {@JourneyInfo}", ex, request);
                throw;
            }
            return Unit.Value;
        }        
    }
}
