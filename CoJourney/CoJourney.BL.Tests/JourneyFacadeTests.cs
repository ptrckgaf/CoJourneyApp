using CoJourney.BL.Facades;
using Xunit;
using Xunit.Abstractions;
using System;
using System.Threading.Tasks;
using CoJourney.BL.Models;
using CoJourney.Common.Tests;
using CoJourney.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.BL.Tests
{
    public class JourneyFacadeTests : CRUDFacadeTestsBase
    {
        private readonly UsersFacade _facadeUserSUT;
        private readonly CarFacade _facadeCarSUT;
        private readonly JourneyFacade _facadeJourneySUT;
        public JourneyFacadeTests(ITestOutputHelper output) : base(output)
        {
            _facadeUserSUT = new UsersFacade(UnitOfWorkFactory, Mapper);
            _facadeJourneySUT = new JourneyFacade(UnitOfWorkFactory, Mapper);
            _facadeCarSUT = new CarFacade(UnitOfWorkFactory, Mapper);
        }

        [Fact]
        public async Task AddNewJourney_ExistingUser_ExistingCar_InsertOrUpdate_JourneyAdded()
        {
            //Arrange
            var user = new UsersDetailModel
            (
                Name: "Abraham",
                Surname: "LoutColn",
                State: "Nemam cas ani penize"
            )
            {
                OwnedCars =
                {
                    new CarDetailModel(
                        Producer:CarSeeds.Punto.Producer,
                        ModelName:CarSeeds.Punto.ModelName,
                        FirstRegistrationDate:CarSeeds.Punto.FirstRegistrationDate,
                        Capacity:CarSeeds.Punto.Capacity)
                }
            };
            var returnedUser = await _facadeUserSUT.SaveAsync(user);
            FixCarIds(user, returnedUser);
            //Act
            var Journey = new JourneyDetailModel(
                    StartLocation: JourneySeeds.Journey1.StartLocation,
                    TargetLocation: JourneySeeds.Journey1.TargetLocation,
                    BeginTime: JourneySeeds.Journey1.BeginTime,
                    DriverId: returnedUser.Id,
                    CarId: returnedUser.OwnedCars[0].Id
                );
            var returnedJourney = await _facadeJourneySUT.SaveAsync(Journey);
            Journey.Id = returnedJourney.Id;
            //Assert

            DeepAssert.Equal(Journey, returnedJourney);
        }
    } 
}
