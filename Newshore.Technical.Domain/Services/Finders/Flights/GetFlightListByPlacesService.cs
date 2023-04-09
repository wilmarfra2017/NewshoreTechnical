using MediatR;
using Newshore.Technical.Domain.ResponseModels;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Domain.Queries.Flights
{
    public class GetFlightListByPlacesService : IRequest<List<FlightResponse>?>
    {
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
    }

    public class GetFlightListByPlacesQueryHandler : IRequestHandler<GetFlightListByPlacesService, List<FlightResponse>?>
    {        
        private readonly IFlightFinder _finder;
                
        public GetFlightListByPlacesQueryHandler(IFlightFinder finder)
        {
            _finder = finder;
        }
                
        public async Task<List<FlightResponse>?> Handle(GetFlightListByPlacesService request, CancellationToken cancellationToken)
        {
            Log.Information($"GetFlightListByPlacesService -- Start --> Origin: {request.Origin}, Destination: {request.Destination}");
            List<FlightResponse>? result = null;
            try
            {
                List<Flight>? flightList = await _finder.GetListByPlaces(request.Origin, request.Destination);
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
                    Log.Information($"GetFlightListByPlacesService -- Success --> Origin: {request.Origin}, Destination: {request.Destination} -- Flight founds");
                }
                else
                {
                    Log.Warning($"GetFlightListByPlacesService -- Success --> Origin: {request.Origin}, Destination: {request.Destination} -- Flights not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("GetFlightListByPlacesService -- Error --> Origin: {Origin}, Destination: {Destination}", ex, request.Origin, request.Destination);
                throw;
            }
            return result;
        }        
    }
}
