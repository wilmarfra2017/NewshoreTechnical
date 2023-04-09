using MediatR;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Domain.Aggregates.Interfaces;
using Newshore.Technical.Domain.ResponseModels;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Transverse.Utils;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace Newshore.Technical.Domain.Commands.Flights
{
    public class CreateFlightService : IRequest<FlightResponse>
    {
        [MaxLength(4)]
        public string Origin { get; set; } = string.Empty;

        [MaxLength(4)]
        public string Destination { get; set; } = string.Empty;
        public double Price { get; set; }
        public int TransportId { get; set; }
    }

    public class CreateFlightCommandHandler : IRequestHandler<CreateFlightService, FlightResponse?>
    {        
        private readonly IFlightRepository _repository;
        private readonly IFlightFinder _finder;
                
        public CreateFlightCommandHandler(IFlightRepository repository, IFlightFinder finder)
        {
            _repository = repository;
            _finder = finder;
        }
                
        public async Task<FlightResponse?> Handle(CreateFlightService request, CancellationToken cancellationToken)
        {
            Log.Information("CreateFlightService -- Handle --> Flight Info: {@FlightInfo}", request);
            FlightResponse? result = null;
            try
            {
                Flight newFlight = new()
                {
                    Destination = request.Destination,
                    Origin = request.Origin,
                    Price = request.Price,
                    TransportId = request.TransportId
                };
                Flight? flightInfo = await _finder.GetExistsFlight(newFlight);
                if(flightInfo == null) 
                {
                    flightInfo = await _repository.Create(newFlight);
                }
                Log.Information("CreateFlightService -- Success --> Flight Info: {@FlightInfo}", request);

                if (flightInfo != null)
                {
                    result = new()
                    {
                        Id = flightInfo.Id,
                        Destination = flightInfo.Destination,
                        Origin = flightInfo.Origin,
                        Price = flightInfo.Price,
                        TransportId = flightInfo.TransportId
                    };
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("CreateFlightService -- Error --> Flight Info: {@Flightnfo}", ex, request);
                throw;
            }
            return result;
        }        
    }
}
