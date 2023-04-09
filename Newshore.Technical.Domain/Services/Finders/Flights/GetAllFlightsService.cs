using MediatR;
using Newshore.Technical.Domain.ResponseModels;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Domain.Queries.Flights
{
    public class GetAllFlightsService : IRequest<List<FlightResponse>?>
    {
    }

    public class GetAllFlightsQueryHandler : IRequestHandler<GetAllFlightsService, List<FlightResponse>?>
    {        
        private readonly IFlightFinder _finder;
                
        public GetAllFlightsQueryHandler(IFlightFinder finder)
        {
            _finder = finder;
        }
                
        public async Task<List<FlightResponse>?> Handle(GetAllFlightsService request, CancellationToken cancellationToken)
        {
            Log.Information("GetAllFlightsService -- Start");
            List<FlightResponse>? result = null;
            try
            {
                List<Flight>? flightList = await _finder.GetAll();
                if (flightList != null)
                {
                    result = new();
                    flightList.ForEach(flightInfo =>
                    {
                        result.Add(new FlightResponse()
                        {
                            Destination = flightInfo.Destination,
                            Id = flightInfo.Id,
                            Origin = flightInfo.Origin,
                            Price = flightInfo.Price,
                            TransportId = flightInfo.TransportId
                        });
                    });
                    Log.Information("GetAllFlightsService -- Success -- Flight founds");
                }
                else
                {
                    Log.Warning($"GetAllFlightsService -- Success -- Flights not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("GetAllFlightsService -- Error", ex);
                throw;
            }
            return result;
        }        
    }
}
