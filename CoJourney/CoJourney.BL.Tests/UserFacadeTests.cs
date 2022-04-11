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
    public class UserFacadeTests : CRUDFacadeTestsBase
    {
        private readonly UsersFacade _facadeSUT;

        public UserFacadeTests(ITestOutputHelper output) : base(output)
        {
            _facadeSUT = new UsersFacade(UnitOfWorkFactory, Mapper);
        }
        
        [Fact]
        public async Task Create_WithNonExistingItem_DoesNotThrow()
        {
            var user = new UsersDetailModel
            (
                Name: "Ivan",
                Surname: "Felos",
                State: "Jsem pripraven na nova dobrodruzstvi"
            );
            user = await _facadeSUT.SaveAsync(user);
        }

        [Fact]
        public async Task GetAll_Single_SeededFelos()
        {
            var users = await _facadeSUT.GetAsync();
            var singleUser = users.Single(i => i.Id == UserSeeds.Felos.Id);
            DeepAssert.Equal(Mapper.Map<UsersListModel>(UserSeeds.Felos), singleUser);
        }
        
        [Fact]
        public async Task GetById_SeededFelos()
        {
            var ingredient = await _facadeSUT.GetAsync(UserSeeds.Felos.Id);

            DeepAssert.Equal(Mapper.Map<UsersDetailModel>(UserSeeds.Felos), ingredient);
        }

        
        [Fact]
        public async Task GetById_NonExisting()
        {
            var ingredient = await _facadeSUT.GetAsync(UserSeeds.EmptyUser.Id);

            Assert.Null(ingredient);
        }
        
        [Fact]
        public async Task SeededFelos_DeleteById_Deleted()
        {
            await _facadeSUT.DeleteAsync(UserSeeds.Felos.Id);

            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Users.AnyAsync(i => i.Id == UserSeeds.Felos.Id));
        }

        [Fact]
        public async Task NewUser_InsertOrUpdate_UserAdded()
        {
            //Arrange
            var user = new UsersDetailModel
            (
                Name: "Ivan",
                Surname: "Felos",
                State: "Jsem pripraven na nova dobrodruzstvi"
            );

            //Act
            user = await _facadeSUT.SaveAsync(user);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var userFromDb = await dbxAssert.Users.SingleAsync(i => i.Id == user.Id);
            DeepAssert.Equal(user, Mapper.Map<UsersDetailModel>(userFromDb));
        }
        [Fact]
        public async Task SeededFelos_InsertOrUpdate_UserUpdated()
        {
            //Arrange
            var user = new UsersDetailModel
            (
                Name:UserSeeds.Felos.Name,
                Surname: UserSeeds.Felos.Surname,
                State: UserSeeds.Felos.State
            )
            {
                Id = UserSeeds.Felos.Id
            };
            user.Name += " - UPDATED";
            user.Surname += " - UPDATED";
            user.State += " - UPDATED";
            user.ImageUrl += "https://www.iconsdb.com/icons/preview/red/new-xxl.png";

            //Act
            await _facadeSUT.SaveAsync(user);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var userFromDb = await dbxAssert.Users.SingleAsync(i => i.Id == user.Id);
            DeepAssert.Equal(user, Mapper.Map<UsersDetailModel>(userFromDb));
        }
    } 
}
