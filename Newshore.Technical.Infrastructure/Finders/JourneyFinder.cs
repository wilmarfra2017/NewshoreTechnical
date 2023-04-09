using Microsoft.EntityFrameworkCore;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Infrastructure.Finders
{
    public class JourneyFinder : IJourneyFinder
    {        
        private readonly SqlServerDbContext _dbContext;                
        public JourneyFinder(SqlServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
                        
        public async Task<Journey?> GetById(int journeyId)
        {
            Log.Information($"Get journey by id -- Start --> Journey id: {journeyId}");
            Journey? result;
            try
            {
                result = await _dbContext.Journeys
                    .FirstOrDefaultAsync(j => j.Id == journeyId);
                if (result == null)
                {
                    Log.Warning($"Get journey by id -- Success --> Journey id: {journeyId}. Journey not found");
                }
                else
                {
                    Log.Information("Get journey by id -- Success --> journey id: {journeyId}. Journey found: {@journeyInfo}", journeyId, result);
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Get journey by id -- Error --> Journey id: {journeyId}.", ex, journeyId);
                throw;
            }
            return result;
        }

        public async Task<List<Journey>?> GetListByFlightId(int flightId)
        {
            Log.Information($"Get journeys by flight id -- Start --> Flight id: {flightId}");
            List<Journey> result;
            try
            {
                result = await _dbContext.Journeys
                    .Join(_dbContext.JourneyFlights,
                    journeys => journeys.Id,
                    journeyFlights => journeyFlights.JourneyId,
                    (journeys, journeyFlights) => new { journeys, journeyFlights })
                    .Where(flightsByJourney => flightsByJourney.journeyFlights.FlightId == flightId)
                    .Select(flightsByJourney => flightsByJourney.journeys)
                    .ToListAsync();
                if (result == null)
                {
                    Log.Warning($"Get journeys by flight id -- Success --> Flight id: {flightId}. Journeys not found");
                }
                else
                {
                    Log.Information("Get journeys by flight id -- Success");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Get journeys by flight id -- Error --> Flight id: {journeyId}", ex, flightId);
                throw;
            }
            return result;
        }

        public async Task<List<Journey>?> GetAll()
        {
            Log.Information("Get all journeys -- Start");
            List<Journey> result;
            try
            {
                result = await _dbContext.Journeys
                    .ToListAsync();
                if (result == null)
                {
                    Log.Warning("Get all journeys -- Success --> Journeys not found");
                }
                else
                {
                    Log.Information("Get all journeys -- Success");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Get all journeys -- Error", ex);
                throw;
            }
            return result;
        }

        public async Task<List<Journey>?> GetListByPlaces(string? origin, string? destination)
        {
            Log.Information($"Get journeys by places -- Start --> Origin: {origin}, Destination: {destination}");
            List<Journey>? result;
            try
            {
                if (!string.IsNullOrEmpty(origin))
                {
                    if (!string.IsNullOrEmpty(destination))
                    {
                        result = await _dbContext.Journeys
                            .Where(j => j.Origin == origin && j.Destination == destination)
                            .ToListAsync();
                    }
                    else
                    {
                        result = await _dbContext.Journeys
                            .Where(j => j.Origin == origin)
                            .ToListAsync();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(destination))
                    {
                        result = await _dbContext.Journeys
                            .Where(j => j.Destination == destination)
                            .ToListAsync();
                    }
                    else
                    {
                        result = GetAll().Result;
                    }
                }

                if (result == null)
                {
                    Log.Warning($"Get journeys by places -- Success --> Origin: {origin}, Destination: {destination}. Journeys not found");
                }
                else
                {
                    Log.Information($"Get journeys by places -- Success --> Origin: {origin}, Destination: {destination}");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Get journeys by places -- Error --> Origin: {origin}, Destination: {destination}", ex, origin, destination);
                throw;
            }
            return result;
        }        
    }
}
