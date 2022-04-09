﻿using CoJourney.DAL;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.Common.Tests.Factories;

public class DbContextSQLiteTestingFactory : IDbContextFactory<CoJourneyDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextSQLiteTestingFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }
    public CoJourneyDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<CoJourneyDbContext> builder = new();
        builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");
        
        // contextOptionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        // builder.EnableSensitiveDataLogging();
        
        return new CoJourneyTestingDbContext(builder.Options, _seedTestingData);
    }
}