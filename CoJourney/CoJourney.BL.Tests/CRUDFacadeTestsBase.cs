using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using CoJourney.BL.Models;
using CoJourney.Common.Tests;
using CoJourney.Common.Tests.Factories;
using CoJourney.DAL;
using CoJourney.DAL.Factories;
using CoJourney.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace CoJourney.BL.Tests;

public class  CRUDFacadeTestsBase : IAsyncLifetime
{
    protected CRUDFacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        // DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);
        // DbContextFactory = new DbContextLocalDBTestingFactory(GetType().FullName!, seedTestingData: true);
        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);

        var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(new[]
                {
                    typeof(BusinessLogic),
                });
                cfg.AddCollectionMappers();
                
                using var dbContext = DbContextFactory.CreateDbContext();
                cfg.UseEntityFrameworkCoreModel<CoJourneyDbContext>(dbContext.Model);
            }
        );
        Mapper = new Mapper(configuration);
        Mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    protected IDbContextFactory<CoJourneyDbContext> DbContextFactory { get; }

    protected Mapper Mapper { get; }

    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
    public static void FixCarIds(UsersDetailModel expectedModel, UsersDetailModel returnedModel)
    {
        returnedModel.Id = expectedModel.Id;

        foreach (var carDetailModel in returnedModel.OwnedCars)
        {
            var carDetailModelExp = expectedModel.OwnedCars.FirstOrDefault(i =>
                i.Producer == carDetailModel.Producer
                && i.ModelName == carDetailModel.ModelName
                && i.ImageURl == carDetailModel.ImageURl
                && i.FirstRegistrationDate == carDetailModel.FirstRegistrationDate
                && i.Capacity == carDetailModel.Capacity);

            if (carDetailModelExp != null)
            {
                carDetailModelExp.Id = carDetailModel.Id;
                carDetailModelExp.OwnerId = carDetailModel.OwnerId;
            }
        }
    }
}