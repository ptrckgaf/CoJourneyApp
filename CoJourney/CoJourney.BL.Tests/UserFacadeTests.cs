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
        public async Task Create_UserWithAllDetails()
        {
            //Arrange
            var user = new UsersDetailModel
            (
                Name:"Ivan",
                Surname:"Felos",
                State:"Jsem pripraven na nova dobrodruzstvi"
            );

            //Act
            user = await _facadeSUT.SaveAsync(user);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var userFromDb = await dbxAssert.Users.SingleAsync(i => i.Id == user.Id);
            DeepAssert.Equal(user, Mapper.Map<UsersDetailModel>(userFromDb));
        }
        /*
                [Fact]
                public async Task Create_WithNonExistingIngredient_Throws()
                {
                    //Arrange
                    var model = new RecipeDetailModel
                    (
                        Name: "Recipe 2",
                        Description: "Testing recipe 2",
                        Duration: TimeSpan.FromHours(2),
                        FoodType: FoodType.Dessert
                    )
                    {
                        Ingredients = {
                            new IngredientAmountDetailModel(
                                IngredientId:Guid.Empty,
                                IngredientName: "Ingredient 1",
                                IngredientDescription: "Testing Ingredient",
                                Amount:0,
                                Unit: Unit.None
                                )
                        }
                    };

                    //Act & Assert
                    try
                    {
                        await _facadeSUT.SaveAsync(model); //In-memory pass without exception
                    }
                    catch(DbUpdateException){} //SqlServer throws on FK
                }

                [Fact]
                public async Task Create_WithIngredient_DoesNotThrowAndEqualsCreated()
                {
                    //Arrange
                    var model = new RecipeDetailModel
                    (
                        Name: "Recipe 2",
                        Description: "Testing recipe 2",
                        Duration: TimeSpan.FromHours(2),
                        FoodType: FoodType.Dessert
                    )
                    {

                        ImageUrl = "https://d2v9mhsiek5lbq.cloudfront.net/eyJidWNrZXQiOiJsb21hLW1lZGlhLXVrIiwia2V5IjoiZm9vZG5ldHdvcmstaW1hZ2UtOGI5ZWM4YTAtODc1OC00MDcyLTg2YTItMzMzYTA4NTY5NTkwLmpwZyIsImVkaXRzIjp7InJlc2l6ZSI6eyJmaXQiOiJjb3ZlciIsIndpZHRoIjo3NTAsImhlaWdodCI6NDIyfX19",
                        Ingredients = {
                            new IngredientAmountDetailModel
                            (
                                IngredientName: IngredientSeeds.IngredientEntity1.Name,
                                IngredientDescription: IngredientSeeds.IngredientEntity1.Description,
                                IngredientId:IngredientSeeds.IngredientEntity1.Id,

                                Amount: 5,
                                Unit: Unit.L
                            )
                            {
                                IngredientImageUrl= IngredientSeeds.IngredientEntity1.ImageUrl,
                            }
                        }
                    };

                    //Act
                    var returnedModel = await _facadeSUT.SaveAsync(model);

                    //Assert
                    FixIds(model, returnedModel);
                    DeepAssert.Equal(model,returnedModel);
                }

                [Fact]
                public async Task Create_WithExistingAndNotExistingIngredient_Throws()
                {
                    //Arrange
                    var model = new RecipeDetailModel
                    (
                        Name: "Recipe 2",
                        Description: "Testing recipe 2",
                        Duration: TimeSpan.FromHours(2),
                        FoodType: FoodType.Dessert
                    )
                    {
                        Ingredients = {
                            new IngredientAmountDetailModel(
                                IngredientId:Guid.Empty,
                                IngredientName: "Ingredient 1",
                                IngredientDescription: "Testing Ingredient",
                                Amount:0,
                                Unit: Unit.None
                            ),
                            Mapper.Map<IngredientAmountDetailModel>(IngredientAmountSeeds.IngredientAmountEntity1)
                        }
                    };

                    //Act & Assert
                    try
                    {
                        await _facadeSUT.SaveAsync(model);
                        Assert.True(false, "Assert Fail");
                    }
                    catch(DbUpdateException){} //SqlServer
                    catch(ArgumentException){} //In-memory
                }

                [Fact]
                public async Task GetById_FromSeeded_DoesNotThrowAndEqualsSeeded()
                {
                    //Arrange
                    var detailModel = Mapper.Map<RecipeDetailModel>(RecipeSeeds.RecipeEntity);

                    //Act
                    var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);

                    //Assert
                    DeepAssert.Equal(detailModel, returnedModel);
                }

                [Fact]
                public async Task GetAll_FromSeeded_DoesNotThrowAndContainsSeeded()
                {
                    //Arrange
                    var listModel = Mapper.Map<RecipeListModel>(RecipeSeeds.RecipeEntity);

                    //Act
                    var returnedModel = await _facadeSUT.GetAsync();

                    //Assert
                    Assert.Contains(listModel, returnedModel);
                }

                [Fact]
                public async Task Delete_FromSeeded_DoesNotThrow()
                {
                    //Arrange
                    var detailModel = Mapper.Map<RecipeDetailModel>(RecipeSeeds.RecipeEntity);

                    //Act & Assert
                    await _facadeSUT.DeleteAsync(detailModel);
                }

                [Fact]
                public async Task Update_FromSeeded_DoesNotThrow()
                {
                    //Arrange
                    var detailModel = Mapper.Map<RecipeDetailModel>(RecipeSeeds.RecipeEntity);
                    detailModel.Name = "Changed recipe name";

                    //Act & Assert
                    await _facadeSUT.SaveAsync(detailModel);
                }

                [Fact]
                public async Task Update_Name_FromSeeded_CheckUpdated()
                {
                    //Arrange
                    var detailModel = Mapper.Map<RecipeDetailModel>(RecipeSeeds.RecipeEntity);
                    detailModel.Name = "Changed recipe name 1";

                    //Act
                    await _facadeSUT.SaveAsync(detailModel);

                    //Assert
                    var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);
                    DeepAssert.Equal(detailModel, returnedModel);
                }

                [Fact]
                public async Task Update_RemoveIngredients_FromSeeded_CheckUpdated()
                {
                    //Arrange
                    var detailModel = Mapper.Map<RecipeDetailModel>(RecipeSeeds.RecipeEntity);
                    detailModel.Ingredients.Clear();

                    //Act
                    await _facadeSUT.SaveAsync(detailModel);

                    //Assert
                    var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);
                    DeepAssert.Equal(detailModel, returnedModel);
                }

                [Fact]
                public async Task Update_RemoveOneOfIngredients_FromSeeded_CheckUpdated()
                {
                    //Arrange
                    var detailModel = Mapper.Map<RecipeDetailModel>(RecipeSeeds.RecipeEntity);
                    detailModel.Ingredients.Remove(detailModel.Ingredients.First());

                    //Act
                    await _facadeSUT.SaveAsync(detailModel);

                    //Assert
                    var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);
                    DeepAssert.Equal(detailModel, returnedModel);
                }

                [Fact]
                public async Task DeleteById_FromSeeded_DoesNotThrow()
                {
                    //Arrange & Act & Assert
                    await _facadeSUT.DeleteAsync(RecipeSeeds.RecipeEntity.Id);
                }

                private static void FixIds(RecipeDetailModel expectedModel, RecipeDetailModel returnedModel)
                {
                    returnedModel.Id = expectedModel.Id;

                    foreach (var ingredientAmountModel in returnedModel.Ingredients)
                    {
                        var ingredientAmountDetailModel = expectedModel.Ingredients.FirstOrDefault(i => 
                            i.IngredientName == ingredientAmountModel.IngredientName 
                            && i.IngredientDescription == ingredientAmountModel.IngredientDescription
                            && i.IngredientImageUrl == ingredientAmountModel.IngredientImageUrl
                            && Math.Abs(i.Amount - ingredientAmountModel.Amount) <= 0
                            && i.Unit == ingredientAmountModel.Unit);

                        if (ingredientAmountDetailModel != null)
                        {
                            ingredientAmountModel.Id = ingredientAmountDetailModel.Id;
                            ingredientAmountModel.IngredientId = ingredientAmountDetailModel.IngredientId;
                        }
                    }
                }*/
    } 
}
