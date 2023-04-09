using Microsoft.EntityFrameworkCore;
using Newshore.Technical.Domain.Aggregates.Entities;

namespace Newshore.Technical.Infrastructure
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
        : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Journey> Journeys { get; set; }
        public DbSet<JourneyFlight> JourneyFlights { get; set; }
        public DbSet<Transport> Transports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transport>()
                .Property(e => e.FlightCarrier)
                .IsUnicode(false);

            modelBuilder.Entity<Transport>()
                .Property(e => e.FlightNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Flight>()
                .Property(e => e.Destination)
                .IsUnicode(false);

            modelBuilder.Entity<Flight>()
                .Property(e => e.Origin)
                .IsUnicode(false);

            modelBuilder.Entity<Journey>()
                .Property(e => e.Destination)
                .IsUnicode(false);

            modelBuilder.Entity<Journey>()
                .Property(e => e.Origin)
                .IsUnicode(false);
        }        
    }
}
