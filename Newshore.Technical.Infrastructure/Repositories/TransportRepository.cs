using Microsoft.EntityFrameworkCore;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Infrastructure.Repository
{
    public class TransportRepository : ITransportRepository
    {        
        private readonly SqlServerDbContext _dbContext;                
        public TransportRepository(SqlServerDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
                
        public async Task<Transport> Create(Transport transport)
        {
            Log.Information("Create Transport -- Start --> Transport Info: {@TransportInfo}", transport);
            try
            {
                _dbContext.Entry(transport).State = EntityState.Added;
                await _dbContext.SaveChangesAsync();
                await _dbContext.Entry(transport).ReloadAsync();
                Log.Information("Create Transport -- Success --> Transport Info: {@TransportInfo}", transport);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Create Transport -- Error --> Transport Info: {@TransportInfo}", ex, transport);
                throw;
            }
            return transport;
        }
        
        public async Task<bool> Update(Transport transport)
        {
            Log.Information("Update Transport -- Start --> Transport Info: {@TransportInfo}", transport);
            bool result;
            try
            {
                _dbContext.Entry(transport).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                Log.Information("Update Transport -- Success --> Transport Info: {@TransportInfo}", transport);
                result = true;
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Update Transport -- Error --> Transport Info: {@TransportInfo}", ex, transport);
                throw;
            }

            return result;
        }

        public async Task<bool> Delete(Transport transport)
        {
            Log.Information("Delete Transport -- Start --> Transport Info: {@TransportInfo}", transport);
            bool result;
            try
            {
                _dbContext.Entry(transport).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
                Log.Information("Delete Transport -- Success --> Transport Info: {@TransportInfo}", transport);
                result = true;
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Delete Transport -- Error --> Transport Info: {@TransportInfo}", ex, transport);
                throw;
            }
            return result;
        }
    }
}
