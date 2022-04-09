using System;
using System.Threading.Tasks;
using CoJourney.Common.Tests;
using CoJourney.Common.Tests.Factories;
using CoJourney.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace CoJourney.DAL.Tests;

public class  DbContextTestsBase : IAsyncLifetime
{
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);
        
        // DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);
        // DbContextFactory = new DbContextLocalDBTestingFactory(GetType().FullName!, seedTestingData: true);
        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);
        
        CoJourneyDbContextSUT = DbContextFactory.CreateDbContext();
    }

    protected IDbContextFactory<CoJourneyDbContext> DbContextFactory { get; }
    protected CoJourneyDbContext CoJourneyDbContextSUT { get; }


    public async Task InitializeAsync()
    {
        await CoJourneyDbContextSUT.Database.EnsureDeletedAsync();
        await CoJourneyDbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await CoJourneyDbContextSUT.Database.EnsureDeletedAsync();
        await CoJourneyDbContextSUT.DisposeAsync();
    }
}