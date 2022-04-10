using CoJourney.Common.Tests.Seeds;
using CoJourney.DAL;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.Common.Tests
{
    public class CoJourneyTestingDbContext : CoJourneyDbContext
    {
        private readonly bool _seedTestingData;

        public CoJourneyTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
            : base(contextOptions, seedDemoData:false)
        {
            _seedTestingData = seedTestingData;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (_seedTestingData)
            {
                UserSeeds.Seed(modelBuilder);
                CarSeeds.Seed(modelBuilder);
                //ADD seeds TODO
            }
        }
    }
}
