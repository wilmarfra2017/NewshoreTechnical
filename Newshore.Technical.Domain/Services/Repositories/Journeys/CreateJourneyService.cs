using MediatR;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Domain.ResponseModels;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Newshore.Technical.Domain.Commands.Journeys
{
    public class CreateJourneyService : IRequest<JourneyResponse>
    {
        [MaxLength(4)]
        public string Origin { get; set; } = string.Empty;

        [MaxLength(4)]
        public string Destination { get; set; } = string.Empty;
        public double Price { get; set; }        
        public bool? IsDirectFlight { get; set; }
        public bool? IsRoundTripFlight { get; set; }
    }

    public class CreateJourneyCommandHandler : IRequestHandler<CreateJourneyService, JourneyResponse?>
    {        
        private readonly IJourneyRepository _repository;                
        public CreateJourneyCommandHandler(IJourneyRepository repository)
        {
            _repository = repository;
        }
                
        public async Task<JourneyResponse?> Handle(CreateJourneyService request, CancellationToken cancellationToken)
        {
            Log.Information("CreateJourneyService -- Start --> Journey Info: {@JourneyInfo}", request);
            JourneyResponse result = null;
            try
            {
                Journey newJourney = new()
                {
                    Destination = request.Destination,
                    Origin = request.Origin,
                    Price = request.Price,
                    IsDirectFlight = request.IsDirectFlight,
                    IsRoundTripFlight = request.IsRoundTripFlight
                };
                Journey journeyInfo = await _repository.Create(newJourney);
                Log.Information("CreateJourneyService -- Success --> Journey Info: {@JourneyInfo}", request);
                if (journeyInfo != null)
                {
                    result = new()
                    {
                        Id = journeyInfo.Id,
                        Destination = journeyInfo.Destination,
                        Origin = journeyInfo.Origin,
                        Price = journeyInfo.Price,
                        IsDirectFlight = journeyInfo.IsDirectFlight,
                        IsRoundTripFlight = journeyInfo.IsRoundTripFlight
                    };
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("CreateJourneyService -- Error --> Journey Info: {@Journeynfo}", ex, request);
                throw;
            }
            return result;
        }        
    }
}
