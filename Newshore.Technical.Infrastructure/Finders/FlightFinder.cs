using Microsoft.EntityFrameworkCore;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Infrastructure.Finders
{
    public class FlightFinder : IFlightFinder
    {        
        private readonly SqlServerDbContext _dbContext;                
        public FlightFinder(SqlServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }                
        
        public async Task<Flight?> GetById(int flightId)
        {
            Log.Information($"Get flight by id -- Start --> Flight id: {flightId}");
            Flight? result;
            try
            {
                result = await _dbContext.Flights.Include(f => f.Transport).FirstOrDefaultAsync(f => f.Id == flightId);
                if (result == null)
                {
                    Log.Warning($"Get flight by id -- Success --> Flight id: {flightId}. Flight not found");
                }
                else
                {
                    Log.Information("Get flight by id -- Success --> Flight id: {flightId}. Flight found: {@FlightInfo}", flightId, result);
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Get flight by id -- Error --> Flight id: {flightId}.", ex);
                throw;
            }
            return result;
        }

        public async Task<List<Flight>?> GetListByJourney(int journeyId)
        {
            Log.Information($"Get flights by journey id -- Start --> Journey id: {journeyId}");
            List<Flight> result;
            try
            {
                result = await _dbContext.Flights
                    .Join(_dbContext.JourneyFlights,
                    flights => flights.Id,
                    journeyFlights => journeyFlights.FlightId,
                    (flights, journeyFlights) => new { flights, journeyFlights })
                    .Where(flightsByJourney => flightsByJourney.journeyFlights.JourneyId == journeyId)
                    .Select(flightsByJourney => flightsByJourney.flights).ToListAsync();
                if (result == null)
                {
                    Log.Warning($"Get flights by journey id -- Success --> Journey id: {journeyId}. Flights not found");
                }
                else
                {
                    Log.Information("Get flights by journey id -- Success");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Get flights by journey id -- Error --> Journey id: {journeyId}", ex, journeyId);
                throw;
            }
            return result;
        }

        public async Task<List<Flight>?> GetAll()
        {
            Log.Information("Get all flights -- Start");
            List<Flight> result;
            try
            {
                result = await _dbContext.Flights.Include(f => f.Transport).ToListAsync();
                if (result == null)
                {
                    Log.Warning("Get all flights -- Success --> No flights found");
                }
                else
                {
                    Log.Information("Get all flights -- Success");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Get all flights -- Error", ex);
                throw;
            }
            return result;
        }

        public async Task<List<Flight>?> GetListByPlaces(string? origin, string? destination)
        {
            Log.Information($"Get flights by places -- Start --> Origin: {origin}, Destination: {destination}");
            List<Flight>? result;
            try
            {
                if (!string.IsNullOrEmpty(origin))
                {
                    if (!string.IsNullOrEmpty(destination))
                    {
                        result = await _dbContext.Flights
                            .Where(f => f.Origin == origin && f.Destination == destination)
                            .ToListAsync();
                    }
                    else
                    {
                        result = await _dbContext.Flights
                            .Where(f => f.Origin == origin)
                            .ToListAsync();
                    }
                }
                else 
                {
                    if (!string.IsNullOrEmpty(destination))
                    {
                        result = await _dbContext.Flights
                            .Where(f => f.Destination == destination)
                            .ToListAsync();
                    }
                    else
                    {
                        result = GetAll().Result;
                    }
                }
                if (result == null)
                {
                    Log.Warning($"Get flights by places -- Success --> Origin: {origin}, Destination: {destination}. Flights not found");
                }
                else
                {
                    Log.Information($"Get flights by places -- Success --> Origin: {origin}, Destination: {destination}");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Get flights by places -- Error --> Origin: {origin}, Destination: {destination}", ex, origin, destination);
                throw;
            }
            return result;
        }

        public async Task<List<Flight>?> GetListByTransportId(int transportId)
        {
            Log.Information($"Get flights by transport id -- Start --> Transport id: {transportId}");
            List<Flight> result;

            try
            {
                result = await _dbContext.Flights
                    .Where(f => f.TransportId == transportId)
                    .ToListAsync();
                if (result == null)
                {
                    Log.Warning($"Get flights by transport id -- Success --> Transport id: {transportId}");
                }
                else
                {
                    Log.Information("Get flights by transport id -- Success");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Get flights by transport id -- Error --> Transport id: {transportId}", ex, transportId);
                throw;
            }
            return result;
        }

        public async Task<Flight?> GetExistsFlight(Flight flightInfo)
        {
            Log.Information("Get exists flight -- Start --> Flight info: {@flightInfo}", flightInfo);
            Flight? result;
            try
            {
                if (flightInfo.Id == 0)
                {
                    result = await _dbContext.Flights.FirstOrDefaultAsync(f => f.Origin == flightInfo.Origin && f.Destination == flightInfo.Destination && f.TransportId == flightInfo.TransportId);
                }
                else
                {
                    result = await _dbContext.Flights.FirstOrDefaultAsync(f => f.Origin == flightInfo.Origin && f.Destination == flightInfo.Destination && f.TransportId == flightInfo.TransportId && f.Id != flightInfo.Id);
                }
                if (result == null)
                {
                    Log.Warning("Get exists flight -- Success --> Flight info: {@flightInfo}. Flight not found", flightInfo);
                }
                else
                {
                    Log.Information("Get exists flight -- Success --> Flight info: {@flightInfo}. Flight found", flightInfo);
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Get exists flight -- Error --> Flight info: {@flightInfo}", ex, flightInfo);
                throw;
            }
            return result;
        }        
    }
}
