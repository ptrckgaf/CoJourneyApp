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
                )
            {
                CarCapacity = returnedUser.OwnedCars[0].Capacity,
                DriverName = returnedUser.Name,
                DriverSurname = returnedUser.Surname
            };
            var returnedJourney = await _facadeJourneySUT.SaveAsync(Journey);
            Journey.Id = returnedJourney.Id;
            //Assert

            DeepAssert.Equal(Journey, returnedJourney);
        }
        
        [Fact]
        public async Task UpdateJourney_InsertOrUpdate_JourneyAddedUpdated()
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
                        Capacity:CarSeeds.Punto.Capacity),
                    
                    new CarDetailModel(
                        Producer:CarSeeds.Golf.Producer,
                        ModelName:CarSeeds.Golf.ModelName,
                        FirstRegistrationDate:CarSeeds.Golf.FirstRegistrationDate,
                        Capacity:CarSeeds.Golf.Capacity)
                }
            };
            var returnedUser = await _facadeUserSUT.SaveAsync(user);
            FixCarIds(user, returnedUser);
            
            var Journey = new JourneyDetailModel(
                    StartLocation: JourneySeeds.Journey1.StartLocation,
                    TargetLocation: JourneySeeds.Journey1.TargetLocation,
                    BeginTime: JourneySeeds.Journey1.BeginTime,
                    DriverId: returnedUser.Id,
                    CarId: returnedUser.OwnedCars[0].Id
                );
            var returnedJourney = await _facadeJourneySUT.SaveAsync(Journey);
            Journey.Id = returnedJourney.Id;
            //Act
            returnedJourney.TargetLocation = "Plzeň";
            returnedJourney.CarId = returnedUser.OwnedCars[1].Id;
            returnedJourney.StartLocation = "Ostrava";
            var returnedJourneyEdited = await _facadeJourneySUT.SaveAsync(returnedJourney);
            returnedJourney.Id = returnedJourneyEdited.Id;
            //Assert

            DeepAssert.Equal(returnedJourney, returnedJourneyEdited);
        }
        
        [Fact]
        public async Task DeleteExistingJourney_Delete_JourneyDeleted()
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
                        Capacity:CarSeeds.Punto.Capacity),

                    new CarDetailModel(
                        Producer:CarSeeds.Golf.Producer,
                        ModelName:CarSeeds.Golf.ModelName,
                        FirstRegistrationDate:CarSeeds.Golf.FirstRegistrationDate,
                        Capacity:CarSeeds.Golf.Capacity)
                }
            };
            var returnedUser = await _facadeUserSUT.SaveAsync(user);
            FixCarIds(user, returnedUser);

            var Journey = new JourneyDetailModel(
                    StartLocation: JourneySeeds.Journey1.StartLocation,
                    TargetLocation: JourneySeeds.Journey1.TargetLocation,
                    BeginTime: JourneySeeds.Journey1.BeginTime,
                    DriverId: returnedUser.Id,
                    CarId: returnedUser.OwnedCars[0].Id
                );
            var returnedJourney = await _facadeJourneySUT.SaveAsync(Journey);
            Journey.Id = returnedJourney.Id;
            //Act
            await _facadeJourneySUT.DeleteAsync(Journey);
            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            
            await Assert.ThrowsAsync<InvalidOperationException>(() => dbxAssert.Journeys.SingleAsync(i => i.Id == Journey.Id));
        }
        
        [Fact]
        public async Task GetJourneyTargetById_JourneyExists_LoadOKNoThrow()
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
                        Capacity:CarSeeds.Punto.Capacity),

                    new CarDetailModel(
                        Producer:CarSeeds.Golf.Producer,
                        ModelName:CarSeeds.Golf.ModelName,
                        FirstRegistrationDate:CarSeeds.Golf.FirstRegistrationDate,
                        Capacity:CarSeeds.Golf.Capacity)
                }
            };
            var returnedUser = await _facadeUserSUT.SaveAsync(user);
            FixCarIds(user, returnedUser);

            var Journey = new JourneyDetailModel(
                    StartLocation: JourneySeeds.Journey1.StartLocation,
                    TargetLocation: JourneySeeds.Journey1.TargetLocation,
                    BeginTime: JourneySeeds.Journey1.BeginTime,
                    DriverId: returnedUser.Id,
                    CarId: returnedUser.OwnedCars[0].Id
                );
            var returnedJourney = await _facadeJourneySUT.SaveAsync(Journey);
            Journey.Id = returnedJourney.Id;
            //Act
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var JourneyFromDb = await dbxAssert.Journeys.SingleAsync(i => i.Id == Journey.Id);
            //Assert
            returnedJourney.Id = JourneyFromDb.Id;
            DeepAssert.Equal(returnedJourney.TargetLocation, JourneyFromDb.TargetLocation);
        }

        [Fact]
        public async Task GetJourneyById_JourneyNotExists_Throws()
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
                        Producer:CarSeeds.Golf.Producer,
                        ModelName:CarSeeds.Golf.ModelName,
                        FirstRegistrationDate:CarSeeds.Golf.FirstRegistrationDate,
                        Capacity:CarSeeds.Golf.Capacity),

                        new CarDetailModel(
                        Producer:CarSeeds.Punto.Producer,
                        ModelName:CarSeeds.Punto.ModelName,
                        FirstRegistrationDate:CarSeeds.Punto.FirstRegistrationDate,
                        Capacity:CarSeeds.Punto.Capacity)

                }

            };
            var Journey = new JourneyDetailModel(
                   StartLocation: JourneySeeds.Journey1.StartLocation,
                   TargetLocation: JourneySeeds.Journey1.TargetLocation,
                   BeginTime: JourneySeeds.Journey1.BeginTime,
                   DriverId: UserSeeds.Felos.Id,
                   CarId: CarSeeds.Punto.Id
               );
            var returnedUser = await _facadeUserSUT.SaveAsync(user);
            FixCarIds(user, returnedUser);

            //Act and Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            
            await Assert.ThrowsAsync<InvalidOperationException>(() => dbxAssert.Journeys.SingleAsync(Journey => Journey.Id == Journey.Id));
        }
    } 
}
