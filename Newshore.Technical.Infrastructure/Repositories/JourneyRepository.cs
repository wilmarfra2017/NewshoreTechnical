using Microsoft.EntityFrameworkCore;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Transverse.Utils;
using Serilog;

namespace Newshore.Technical.Infrastructure.Repository
{
    public class JourneyRepository : IJourneyRepository
    {
        
        private readonly SqlServerDbContext _dbContext;                
        public JourneyRepository(SqlServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
                
        public async Task<Journey> Create(Journey journey)
        {
            Log.Information("Create Journey -- Start --> Journey Info: {@JourneyInfo}", journey);
            try
            {
                _dbContext.Entry(journey).State = EntityState.Added;
                await _dbContext.SaveChangesAsync();
                await _dbContext.Entry(journey).ReloadAsync();

                Log.Information("Create Journey -- Success --> Journey Info: {@JourneyInfo}", journey);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Create Journey -- Error --> Journey Info: {@JourneyInfo}", ex, journey);

                throw;
            }
            return journey;
        }
        
        public async Task<bool> Update(Journey journey)
        {
            Log.Information("Update Journey -- Start --> Journey Info: {@JourneyInfo}", journey);
            bool result;
            try
            {
                _dbContext.Entry(journey).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                Log.Information("Update Journey -- Success --> Journey Info: {@JourneyInfo}", journey);
                result = true;
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Update Journey -- Error --> Journey Info: {@JourneyInfo}", ex, journey);
                throw;
            }
            return result;
        }

        public async Task<bool> Delete(Journey journey)
        {
            Log.Information("Delete Journey -- Start --> Journey Info: {@JourneyInfo}", journey);
            bool result;
            try
            {
                _dbContext.Entry(journey).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
                Log.Information("Delete Journey -- Success --> Journey Info: {@JourneyInfo}", journey);
                result = true;
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("Delete Journey -- Error --> Journey Info: {@JourneyInfo}", ex, journey);
                throw;
            }
            return result;
        }
    }
}
