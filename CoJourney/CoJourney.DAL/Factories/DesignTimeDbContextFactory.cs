using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CoJourney.DAL.Factories
{
    /// <summary>
    /// EF Core CLI migration generation uses this DbContext to create model and migration
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CoJourneyDbContext>
    {
        public CoJourneyDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<CoJourneyDbContext> builder = new();
            builder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;
                Initial Catalog = CoJourney;
                MultipleActiveResultSets = True;
                Integrated Security = True; ");

            return new CoJourneyDbContext(builder.Options);
        }
    }
}