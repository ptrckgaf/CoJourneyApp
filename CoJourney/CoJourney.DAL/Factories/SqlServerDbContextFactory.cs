using Microsoft.EntityFrameworkCore;

namespace CoJourney.DAL.Factories
{
    public class SqlServerDbContextFactory : IDbContextFactory<CoJourneyDbContext>
    {
        private readonly string _connectionString;
        private readonly bool _seedDemoData;

        public SqlServerDbContextFactory(string connectionString, bool seedDemoData = false)
        {
            _connectionString = connectionString;
            _seedDemoData = seedDemoData;
        }

        public CoJourneyDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CoJourneyDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            //optionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
            //optionsBuilder.EnableSensitiveDataLogging();

            return new CoJourneyDbContext(optionsBuilder.Options, _seedDemoData);
        }
    }
}