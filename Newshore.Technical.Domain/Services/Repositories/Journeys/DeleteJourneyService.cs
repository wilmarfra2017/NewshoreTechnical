using MediatR;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Newshore.Technical.Domain.Commands.Journeys
{
    public class DeleteJourneyService : IRequest
    {
        public int Id { get; set; }

        [MaxLength(4)]
        public string Origin { get; set; } = string.Empty;

        [MaxLength(4)]
        public string Destination { get; set; } = string.Empty;
        public bool? IsDirectFlight { get; set; }
        public bool? IsRoundTripFlight { get; set; }
        public double Price { get; set; }
    }

    public class DeleteJourneyCommandHandler : IRequestHandler<DeleteJourneyService>
    {        
        private readonly IJourneyRepository _repository;
                
        public DeleteJourneyCommandHandler(IJourneyRepository repository)
        {
            _repository = repository;
        }
                
        public async Task<Unit> Handle(DeleteJourneyService request, CancellationToken cancellationToken)
        {
            Log.Information("DeleteJourneyService -- Start --> Journey Info: {@JourneyInfo}", request);
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

                bool result = await _repository.Delete(transportInfo);
                Log.Information("DeleteJourneyService -- Success: {@result} --> Journey Info: {@JourneyInfo}", result, request);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("DeleteJourneyService -- Error --> Journey Info: {@JourneyInfo}", ex, request);
                throw;
            }

            return Unit.Value;
        }        
    }
}
