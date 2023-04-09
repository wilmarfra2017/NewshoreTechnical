using Microsoft.EntityFrameworkCore;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Infrastructure.Finders
{
    public class TransportFinder : ITransportFinder
    {
        
        private readonly SqlServerDbContext _dbContext;                
        public TransportFinder(SqlServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
                       
        public async Task<Transport?> GetById(int transportId)
        {
            Log.Information($"Get transport by id -- Start --> Transport id: {transportId}");
            Transport? result;
            try
            {
                result = await _dbContext.Transports
                    .FirstOrDefaultAsync(j => j.Id == transportId);
                
                if (result == null)
                {
                    Log.Warning($"Get transport by id -- Success --> Transport id: {transportId}. Transport not found");
                }
                else
                {
                    Log.Information("Get transport by id -- Success --> Transport id: {transportId}. Transport found: {@transportInfo}", transportId, result);
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Get transport by id -- Error --> Transport id: {transportId}.", ex, transportId);

                throw;
            }
            return result;
        }

        public async Task<List<Transport>?> GetListByFlightId(int flightId)
        {
            Log.Information($"Get transports by flight id -- Start --> Flight id: {flightId}");
            List<Transport> result;
            try
            {
                result = await _dbContext.Transports
                    .Join(_dbContext.Flights,
                    transport => transport.Id,
                    flight => flight.TransportId,
                    (transport, flight) => new { transport, flight })
                    .Where(flightTransports => flightTransports.flight.Id == flightId)
                    .Select(flightTransports => flightTransports.transport)
                    .ToListAsync();
                if (result == null)
                {
                    Log.Warning($"Get transports by flight id -- Success --> Transport id: {flightId}. Transports not found");
                }
                else
                {
                    Log.Information("Get transports by flight id -- Success");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Get transports by flight id -- Error --> Flight id: {flightId}", ex, flightId);

                throw;
            }
            return result;
        }

        public async Task<List<Transport>?> GetAll()
        {
            Log.Information("Get all transports -- Start");
            List<Transport> result;
            try
            {
                result = await _dbContext.Transports
                    .ToListAsync();
                if (result == null)
                {
                    Log.Warning("Get all transports -- Success --> transports not found");
                }
                else
                {
                    Log.Information("Get all transports -- Success");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Get all transports -- Error", ex);
                throw;
            }
            return result;
        }

        public async Task<Transport?> GetExistsTransport(Transport transportInfo)
        {
            Log.Information("Get exists transport -- Start --> Transport info: {@transportInfo}", transportInfo);
            Transport? result;
            try
            {
                if (transportInfo.Id == 0)
                {
                    result = await _dbContext.Transports.FirstOrDefaultAsync(t => t.FlightCarrier == transportInfo.FlightCarrier && t.FlightNumber == transportInfo.FlightNumber);
                }
                else
                {
                    result = await _dbContext.Transports.FirstOrDefaultAsync(t => t.FlightCarrier == transportInfo.FlightCarrier && t.FlightNumber == transportInfo.FlightNumber && t.Id != transportInfo.Id);
                }
                if (result == null)
                {
                    Log.Warning("Get exists transport -- Success --> Transport info: {@transportInfo}. Transport not found", transportInfo);
                }
                else
                {
                    Log.Information("Get exists transport -- Success --> Transport info: {@transportInfo}. Transport found", transportInfo);
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Get exists transport -- Error --> Transport info: {@transportInfo}", ex, transportInfo);
                throw;
            }
            return result;
        }        
    }
}
