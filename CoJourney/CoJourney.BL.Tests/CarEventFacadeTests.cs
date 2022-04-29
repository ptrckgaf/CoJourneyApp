using CoJourney.BL.Facades;
using Xunit;
using Xunit.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CoJourney.BL.Models;
using CoJourney.Common.Tests;
using CoJourney.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.BL.Tests
{
    public class CarEventFacadeTests : CRUDFacadeTestsBase
    {
        private readonly UsersFacade _facadeSUT;
        private readonly CarEventFacade _facadeEventSUT;

        public CarEventFacadeTests(ITestOutputHelper output) : base(output)
        {
            _facadeSUT = new UsersFacade(UnitOfWorkFactory, Mapper);
            _facadeEventSUT = new CarEventFacade(UnitOfWorkFactory, Mapper);
        }

        [Fact]
        public async Task CreateEvent_WithNonExistingItem_DoesNotThrow()
        {
            //Arrange
            var evente = new CarEventDetailModel
            (

                Name: "Cesta na mars",
                BeginTime: new DateTime(2023,12,12,12,12,12),
                EndTime: new DateTime(2028,12,24,20,20,20),
                TargetLocation:"Mesiac"

            ){InstitutorId = UserSeeds.Felos.Id};

            //Act
            evente = await _facadeEventSUT.SaveAsync(evente);
            

            //Assert

        }

        [Fact]
        public async Task test_if_in_database()
        {
            var event1 = new CarEventDetailModel(
                BeginTime: new DateTime(2022, 5, 2, 16, 00, 00),
                EndTime: new DateTime(2022, 5, 2, 16, 10, 00),
                TargetLocation: "Nezname",
                Name: "Obhajoba"

            )
            {
                InstitutorId = UserSeeds.Felos.Id
            };
            var returnevent = await _facadeEventSUT.SaveAsync(event1);
            FixEventIds(event1, returnevent);


            //Assert
            DeepAssert.Equal(returnevent, event1);
        }
        [Fact]
        public async Task update_event_from_database()
        {
            var event2 = new CarEventDetailModel(
                BeginTime: CarEventSeeds.Event1.BeginTime,
                EndTime: CarEventSeeds.Event1.EndTime,
                Name: CarEventSeeds.Event1.Name,
                TargetLocation: CarEventSeeds.Event1.TargetLocation
            )
            {
                InstitutorId = CarEventSeeds.Event1.InstitutorId
            };
            event2.Name += "- UPDATED";
            event2.TargetLocation = "- UPDATED";
            //Act
            var returnevent = await _facadeEventSUT.SaveAsync(event2);
            FixEventIds(event2,returnevent);
            //Assert

            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var userFromDb = await dbxAssert.Events.SingleAsync(i => i.Id == returnevent.Id);
            DeepAssert.Equal(returnevent, Mapper.Map<CarEventDetailModel>(userFromDb));
        }

        [Fact]

        public async Task Nonexisting_event()
        {
            var returnevent = await _facadeEventSUT.GetAsync(CarEventSeeds.EmptyUser.Id);

            Assert.Null(returnevent);
        }

        [Fact]

        public async Task new_user_instituting_an_Event()
        {
            var user = new UsersDetailModel
            (
                Name: "Jozo",
                Surname: "Procko",
                State: "stale komikom"
            );
            var retrunuser = await _facadeSUT.SaveAsync(user);
            var event1 = new CarEventDetailModel
                (
                BeginTime: new DateTime(2022, 12,22,4,55,00),
                EndTime: new DateTime( 2022,12,25,4,55,30),
                Name:"Utek",
                TargetLocation: "Izba"
                ) {   InstitutorId = retrunuser.Id };

            var returnevent = await _facadeEventSUT.SaveAsync(event1);
            
            DeepAssert.Equal(retrunuser.Id,returnevent.InstitutorId);
        }

    } 
}
