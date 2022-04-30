using CoJourney.BL.Facades;
using Xunit;
using Xunit.Abstractions;
using System;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using CoJourney.BL.Models;
using CoJourney.Common.Tests;
using CoJourney.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;

namespace CoJourney.BL.Tests
{
    public class InvitationFacadeTests : CRUDFacadeTestsBase
    {
        private readonly UsersFacade _facadeSUT;
        private readonly InvitationFacade _facadeInvitationSUT;
        private readonly JourneyFacade _facadeJourneySUT;
        private readonly CarFacade _facadeCarSUT;

        public InvitationFacadeTests(ITestOutputHelper output) : base(output)
        {
            _facadeSUT = new UsersFacade(UnitOfWorkFactory, Mapper);
            _facadeJourneySUT = new JourneyFacade(UnitOfWorkFactory, Mapper);
            _facadeCarSUT = new CarFacade(UnitOfWorkFactory, Mapper);
            _facadeInvitationSUT = new InvitationFacade(UnitOfWorkFactory, Mapper);
        }
        
        [Fact]
        public async Task Create_WithNonExistingItem_DoesNotThrow()
        {
            var reciever = await _facadeSUT.GetAsync(UserSeeds.Patejdl.Id);

           
            var user = new UsersDetailModel
            (
                Name: "Ivan",
                Surname: "Felos",
                State: "Jsem pripraven na nova dobrodruzstvi"
            )
            {
                OwnedCars =
                {
                    new CarDetailModel
                        (
                        Producer:CarSeeds.Golf.Producer,
                        ModelName:CarSeeds.Picaso.ModelName,
                        FirstRegistrationDate:CarSeeds.Picaso.FirstRegistrationDate,
                        Capacity:CarSeeds.Picaso.Capacity
                        )
                }

            };
            user.OwnedCars.Add( new CarDetailModel(
                Producer: CarSeeds.Picaso.Producer,
                ModelName: CarSeeds.Picaso.ModelName,
                FirstRegistrationDate: CarSeeds.Picaso.FirstRegistrationDate,
                Capacity: CarSeeds.Picaso.Capacity
            ));
            
            var returnedUser = await _facadeSUT.SaveAsync(user);
            FixCarIds(user, returnedUser);

            var journey = new JourneyDetailModel
            (
                StartLocation: JourneySeeds.Journey1.StartLocation,
                TargetLocation: JourneySeeds.Journey1.TargetLocation,
                BeginTime: JourneySeeds.Journey1.BeginTime,
                DriverId: returnedUser.Id,
                CarId: returnedUser.OwnedCars[0].Id);

            var returnedJourney = await _facadeJourneySUT.SaveAsync(journey);
            reciever = await _facadeSUT.SaveAsync(reciever);

            var invit = new InvitationDetailModel
            (
                SenderUserId: returnedUser.Id,
                JourneyId: returnedJourney.Id,
                Accepted: true,
                ReceiverUserId:reciever.Id
            );
            var returnedInvit = await _facadeInvitationSUT.SaveAsync(invit);
            FixInvitationIds(invit,returnedInvit);
            DeepAssert.Equal(invit,returnedInvit);
        }

       
    } 
}
