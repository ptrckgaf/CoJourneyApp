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
        public async Task InsertTwoExistingUser_Car_InsertOrUpdate_CarrAdded()
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
            var returnedUser = await _facadeSUT.SaveAsync(user);
            FixCarIds(user, returnedUser);
            
            CarDetailModel car1 = await _facadeCarSUT.GetAsync(returnedUser.OwnedCars[0].Id) ?? CarDetailModel.Empty;
            CarDetailModel car2 = await _facadeCarSUT.GetAsync(returnedUser.OwnedCars[1].Id) ?? CarDetailModel.Empty;
       

            //Act
            await _facadeCarSUT.SaveAsync(car1);
            await _facadeCarSUT.SaveAsync(car2);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var CarFromDb = await dbxAssert.Cars.SingleAsync(i => i.Id == car1.Id);
            DeepAssert.Equal(car1, Mapper.Map<CarDetailModel>(CarFromDb));

            CarFromDb = await dbxAssert.Cars.SingleAsync(i => i.Id == car2.Id);
            DeepAssert.Equal(car2, Mapper.Map<CarDetailModel>(CarFromDb));

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

            CarDetailModel car = await _facadeCarSUT.GetAsync(returnedUser.OwnedCars[0].Id) ?? CarDetailModel.Empty;
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

        [Fact]
        public async Task DeleteExistingCar_Delete_CarrAdded()
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
            var returnedUser = await _facadeSUT.SaveAsync(user);
            FixCarIds(user, returnedUser);

            CarDetailModel car2 = await _facadeCarSUT.GetAsync(returnedUser.OwnedCars[1].Id) ?? CarDetailModel.Empty;
            CarDetailModel car1 = await _facadeCarSUT.GetAsync(returnedUser.OwnedCars[0].Id) ?? CarDetailModel.Empty;
            await _facadeCarSUT.SaveAsync(car1);
            await _facadeCarSUT.SaveAsync(car2);

            //Act
            await _facadeCarSUT.DeleteAsync(car1);
            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            //check if the first car is deleted and second is stored still
            await Assert.ThrowsAsync<InvalidOperationException> (() => dbxAssert.Cars.SingleAsync(i => i.Id == car1.Id));
            var CarFromDb = await dbxAssert.Cars.SingleAsync(i => i.Id == car2.Id);
            DeepAssert.Equal(car2, Mapper.Map<CarDetailModel>(CarFromDb));
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
