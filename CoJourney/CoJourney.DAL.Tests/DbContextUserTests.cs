using System;
using System.Linq;
using System.Threading.Tasks;
using CoJourney.Common.Tests.Seeds;
using CoJourney.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace CoJourney.DAL.Tests
{
    public class DbContextUserTests : DbContextTestsBase
    {
        public DbContextUserTests(ITestOutputHelper output) : base(output)
        {
        }
        
        [Fact]
        public async Task AddNew_User_Persisted()
        {
            //Arrange
            UserEntity entity = new(
            Id: Guid.Parse(input: "76107b1c-c63b-4b99-9ff7-a1855d2da1c3"),
            Name: "Brano",
            Surname: "Mojsej",
            ImageUrl: "https://1884403144.rsc.cdn77.org/foto/brano-mojsej/Zml0LWluLzk3OHg5OTk5L2ZpbHRlcnM6cXVhbGl0eSg4NSk6bm9fdXBzY2FsZSgpL2ltZw/1848007.jpg?v=0&st=jK5FVq2tgUqCHWXPJN-dJ-nuqpwo1R9GsFARa9J4Jzs&ts=1600812000&e=0",
            State: "Available"

            );

            //Act
            CoJourneyDbContextSUT.Users.Add(entity);
            await CoJourneyDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntities);
        }

#if false //nepouzito
        [Fact]
        public async Task GetAll_Ingredients_ContainsSeededWater()
        {
            //Act
            var entities = await CookBookDbContextSUT.Ingredients.ToArrayAsync();

            //Assert
            Assert.Contains(IngredientSeeds.Water, entities);
        }

        [Fact]
        public async Task GetById_Ingredient_WaterRetrieved()
        {
            //Act
            var entity = await CookBookDbContextSUT.Ingredients.SingleAsync(i => i.Id == IngredientSeeds.Water.Id);

            //Assert
            Assert.Equal(IngredientSeeds.Water, entity);
        }

        [Fact]
        public async Task Update_Ingredient_Persisted()
        {
            //Arrange
            var baseEntity = IngredientSeeds.WaterUpdate;
            var entity =
                baseEntity with
                {
                    Name = baseEntity + "Updated",
                    Description = baseEntity + "Updated",
                };

            //Act
            CookBookDbContextSUT.Ingredients.Update(entity);
            await CookBookDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Ingredients.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task Delete_Ingredient_WaterDeleted()
        {
            //Arrange
            var entityBase = IngredientSeeds.WaterDelete;

            //Act
            CookBookDbContextSUT.Ingredients.Remove(entityBase);
            await CookBookDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await CookBookDbContextSUT.Ingredients.AnyAsync(i => i.Id == entityBase.Id));
        }

        [Fact]
        public async Task DeleteById_Ingredient_WaterDeleted()
        {
            //Arrange
            var entityBase = IngredientSeeds.WaterDelete;

            //Act
            CookBookDbContextSUT.Remove(
                CookBookDbContextSUT.Ingredients.Single(i => i.Id == entityBase.Id));
            await CookBookDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await CookBookDbContextSUT.Ingredients.AnyAsync(i => i.Id == entityBase.Id));
        } 
#endif
    }
}
