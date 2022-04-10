using CoJourney.BL.Facades;
using Xunit;
using Xunit.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using CoJourney.BL.Models;
using CoJourney.Common.Tests;
using CoJourney.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.BL.Tests
{
    public class CarFacadeTests : CRUDFacadeTestsBase
    {
        private readonly UsersFacade _facadeSUT;
        private readonly CarFacade _facadeCarSUT;

        public CarFacadeTests(ITestOutputHelper output) : base(output)
        {
            _facadeSUT = new UsersFacade(UnitOfWorkFactory, Mapper);
            _facadeCarSUT = new CarFacade(UnitOfWorkFactory, Mapper);
        }

        [Fact]
        public async Task CreateNewUserAndAddCar_InsertOrUpdate_CarrAdded()
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
                        Producer:CarSeeds.Picaso.Producer,
                        ModelName:CarSeeds.Picaso.ModelName,
                        FirstRegistrationDate:CarSeeds.Picaso.FirstRegistrationDate,
                        Capacity:CarSeeds.Picaso.Capacity)
                }
            };

            //Act
            var returnedUser = await _facadeSUT.SaveAsync(user);


            //Assert
            FixCarIds(user, returnedUser);
            DeepAssert.Equal(user, returnedUser);
        }

        [Fact]
        public async Task UpdateCar_InsertOrUpdate_CarrAdded()
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
                        Producer:CarSeeds.Picaso.Producer,
                        ModelName:CarSeeds.Picaso.ModelName,
                        FirstRegistrationDate:CarSeeds.Picaso.FirstRegistrationDate,
                        Capacity:CarSeeds.Picaso.Capacity)
                }
            };
            var returnedUser = await _facadeSUT.SaveAsync(user);
            FixCarIds(user, returnedUser);

            var car = await _facadeCarSUT.GetAsync(returnedUser.OwnedCars[0].Id);
            car.Producer = "Lamborghini";
            car.ModelName = "Aventador";
            car.FirstRegistrationDate = new DateTime(2020, 10, 15);
            car.Capacity = 2;


            //Act
            await _facadeCarSUT.SaveAsync(car);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var CarFromDb = await dbxAssert.Cars.SingleAsync(i => i.Id == car.Id);
            DeepAssert.Equal(car, Mapper.Map<CarDetailModel>(CarFromDb));

          
        }

        private static void FixCarIds(UsersDetailModel expectedModel, UsersDetailModel returnedModel)
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
}
