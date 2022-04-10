
using CoJourney.DAL;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.Common.Tests.Factories
{
    public class DbContextTestingInMemoryFactory: IDbContextFactory<CoJourneyDbContext>
    {
        private readonly string _databaseName;
        private readonly bool _seedTestingData;

        public DbContextTestingInMemoryFactory(string databaseName, bool seedTestingData = false)
        {
            _databaseName = databaseName;
            _seedTestingData = seedTestingData;
        }

        public CoJourneyDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<CoJourneyDbContext> contextOptionsBuilder = new();
            contextOptionsBuilder.UseInMemoryDatabase(_databaseName);
            
            // contextOptionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
            // builder.EnableSensitiveDataLogging();
            
            return new CoJourneyTestingDbContext(contextOptionsBuilder.Options, _seedTestingData);
        }
    }
}