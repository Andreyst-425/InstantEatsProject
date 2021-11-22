using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstantEatService.Dto;
using InstantEatService.Models;
using InstantEatService.Repositories;
using InstantEatService.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace InstantEatService.Tests.Service
{
    [TestFixture]
    public class FoodItemsServiceTests
    {
        private async Task<bool> GetTrue()
        {
            return true;
        }
        private async Task<FoodItem> GetSingleFoodItem()
        {
            return new FoodItem
            {
                Id = 10,
                Name = "name",
                Categories = new List<Category>
                {
                    new()
                    {
                        Name = "супы"
                    }
                },
                Price = 200,
                PictureUrl = "url",
                Description = "descr"
            };
        }
        private async Task<IEnumerable<FoodItem>> GetFoodItems()
        {
            var list = new List<FoodItem>
            {
                new FoodItem
                {
                    Description = "1112322",
                    Id = 3, 
                    Name = "na32432me",
                    Price = 100,
                    Categories = new List<Category>
                    {
                        new()
                        {
                            Name = "Супы"
                        }
                    },
                    PictureUrl = "sijigi"
                },
                new FoodItem
                {
                    Description = "12342321",
                    Id = 4, 
                    Name = "ne",
                    Price = 10,
                    Categories = new List<Category>
                    {
                        new()
                        {
                            Name = "Салаты"
                        }
                    },
                    PictureUrl = "sihjhdsgi"
                },
                new FoodItem
                {
                    Description = "111",
                    Id = 1, 
                    Name = "name",
                    Price = 51,
                    Categories = new List<Category>
                    {
                        new()
                        {
                            Name = "Супы"
                        }
                    },
                    PictureUrl = "sijigi"
                },
                new FoodItem
                {
                    Description = "121",
                    Id = 2, 
                    Name = "ne",
                    Price = 55,
                    Categories = new List<Category>
                    {
                        new()
                        {
                            Name = "Бургеры"
                        }
                    },
                    PictureUrl = "sihjhdsgi"
                },
            };
            return list;
        }
        private async Task<IEnumerable<FoodItem>> GetFoodItemsWithCategory()
        {
            var list = new List<FoodItem>
            {
                new()
                {
                    Description = "111",
                    Id = 1, 
                    Name = "name",
                    Price = 51,
                    Categories = new List<Category>
                    {
                        new()
                        {
                            Name = "Супы"
                        }
                    },
                    PictureUrl = "sijigi"
                },
                new()
                {
                    Description = "121",
                    Id = 2, 
                    Name = "ne",
                    Price = 55,
                    Categories = new List<Category>
                    {
                        new()
                        {
                            Name = "Супы"
                        }
                    },
                    PictureUrl = "sihjhdsgi"
                },
            };
            return list;
        }
        private async Task<IEnumerable<FoodItem>> GetFoodItemsInPriceLimits()
        {
            var list = new List<FoodItem>
            {
                new()
                {
                    Description = "111",
                    Id = 1, 
                    Name = "name",
                    Price = 51,
                    Categories = new List<Category>
                    {
                        new()
                        {
                            Name = "супы"
                        }
                    },
                    PictureUrl = "sijigi"
                },
                new()
                {
                    Description = "121",
                    Id = 2, 
                    Name = "ne",
                    Price = 55,
                    Categories = new List<Category>
                    {
                        new()
                        {
                            Name = "супы"
                        }
                    },
                    PictureUrl = "sihjhdsgi"
                },
            };
            return list;
        }
        
        [Test]
        public async Task GetAll_FoodItems()
        {
            //arrange
            var mockRepository = new Mock<IFoodItemsRepository>();
            var mockLogger = new Mock<ILogger<FoodItemsService>>();
            var logger = mockLogger.Object;
            mockRepository.Setup(m => m.GetAllFoodItems()).Returns(GetFoodItems());
            var foodItemsService = new FoodItemsService(mockRepository.Object, logger);
            var expected = await GetFoodItems();
            var expectedList = expected.ToList();
            //act
            var actual = await foodItemsService.GetAll();
            
            //assert
            Assert.AreEqual(expected.Count(),actual.Count());
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expected.Count(); i++)
                {
                    Assert.AreEqual(expectedList[i].Name,actual[i].Name);
                    Assert.AreEqual(expectedList[i].Id,actual[i].Id);
                    Assert.AreEqual(expectedList[i].PictureUrl,actual[i].PictureUrl);
                    Assert.AreEqual(expectedList[i].Price,actual[i].Price);
                }
            });
        }

        [Test]
        public async Task GetAllWithCategories_FoodItems()
        {
            //arrange
            var mockRepository = new Mock<IFoodItemsRepository>();
            var mockLogger = new Mock<ILogger<FoodItemsService>>();
            var logger = mockLogger.Object;
            mockRepository.Setup(m => m.GetAllFoodItemsWithCategories()).Returns(GetFoodItems());
            var foodItemsService = new FoodItemsService(mockRepository.Object, logger);
            var expected = await GetFoodItems();
            var expectedList = expected.ToList();
            
            //act
            var actualList = await foodItemsService.GetAllWithCategories();
            
            //assert
            Assert.AreEqual(expectedList.Count(),actualList.Count());
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expectedList.Count();i++)
                {
                    Assert.AreEqual(expectedList[i].Name, actualList[i].Name);
                    Assert.AreEqual(expectedList[i].Id,actualList[i].Id);
                    Assert.AreEqual(expectedList[i].PictureUrl,actualList[i].PictureUrl);
                    Assert.AreEqual(expectedList[i].Description,actualList[i].Description);

                    var expectedCategories = expectedList[i].Categories.ToList();
                    var actualCategories = actualList[i].Categories.ToList();

                    for (int j = 0; j < expectedCategories.Count(); j++)
                    {
                        Assert.AreEqual(expectedCategories[j].Name, actualCategories[j].Name);
                    }
                }
            });
            
        }
        
        [Test]
        public async Task GetFoodItemById_Id_FoodItem()
        {
            //arrange
            var mockRepository = new Mock<IFoodItemsRepository>();
            var mockLogger = new Mock<ILogger<FoodItemsService>>();
            var logger = mockLogger.Object;
            var id = 10;
            mockRepository.Setup(m => m.GetFoodItem(id)).Returns(GetSingleFoodItem());
            var foodItemsService = new FoodItemsService(mockRepository.Object, logger);
            var expected = await GetSingleFoodItem();
            
            //act
            var actual = await foodItemsService.GetFoodItemById(id);
            
            //assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.Name,actual.Name);
                Assert.AreEqual(expected.Price,actual.Price);
                Assert.AreEqual(expected.PictureUrl,actual.PictureUrl);
                Assert.AreEqual(expected.Description,actual.Description);
                Assert.AreEqual(expected.Id,actual.Id);
            });
        }
        
        [Test]
        public async Task CreateFoodItem_CreateDto_FoodItem()
        {
            //arrange
            var mockRepository = new Mock<IFoodItemsRepository>();
            var mockLogger = new Mock<ILogger<FoodItemsService>>();
            var logger = mockLogger.Object;
            var expected = await GetSingleFoodItem();
            var createDto = new FoodItemCreateDto()
            {
                Name = expected.Name,
                Description = expected.Description,
                Price = expected.Price,
                PictureUrl = expected.PictureUrl
            };
            mockRepository.Setup(m => m.CreateFoodItem(createDto)).Returns(GetSingleFoodItem());
            var foodItemsService = new FoodItemsService(mockRepository.Object, logger);
            
            //act
            var actual = await foodItemsService.CreateFoodItem(createDto);
            //assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.Name,actual.Name);
                Assert.AreEqual(expected.Price,actual.Price);
                Assert.AreEqual(expected.PictureUrl,actual.PictureUrl);
                Assert.AreEqual(expected.Description,actual.Description);
            });
        }

        [Test]
        public async Task UpdateFoodItem_FoodItemInfo_ReturnsTrue()
        {
            //arrange
            var mockRepository = new Mock<IFoodItemsRepository>();
            var mockLogger = new Mock<ILogger<FoodItemsService>>();
            var logger = mockLogger.Object;
            var id = 1;
            var expected = await GetSingleFoodItem();
            var createDto = new FoodItemCreateDto()
            {
                Name = expected.Name,
                Description = expected.Description,
                Price = expected.Price,
                PictureUrl = expected.PictureUrl
            };
            
            mockRepository.Setup(m => m.UpdateFoodItem(id,createDto)).Returns(GetTrue());
            var foodItemsService = new FoodItemsService(mockRepository.Object, logger);
            
            //act
            var actual = await foodItemsService.UpdateFoodItem(id, createDto);
            
            //assert
            Assert.IsTrue(actual);
        }

        [Test]
        public async Task DeleteFoodItem_Id_ReturnsTrue()
        {
            //arrange
            var mockRepository = new Mock<IFoodItemsRepository>();
            var mockLogger = new Mock<ILogger<FoodItemsService>>();
            var logger = mockLogger.Object;
            var id = 1;
            
            mockRepository.Setup(m => m.DeleteFoodItem(id)).Returns(GetTrue());
            var foodItemsService = new FoodItemsService(mockRepository.Object, logger);

            //act
            var actual = await foodItemsService.DeleteFoodItem(id);
            
            //assert
            Assert.IsTrue(actual);
        }

        [Test]
        public async Task FilterByPrice_PriceLimits_FoodItems()
        {
            //arrange
            var mockRepository = new Mock<IFoodItemsRepository>();
            var mockLogger = new Mock<ILogger<FoodItemsService>>();
            var logger = mockLogger.Object;
            var expected = await GetFoodItemsInPriceLimits();
            var expectedList = expected.ToList();
            const int min = 50;
            const int max = 60;
            
            mockRepository.Setup(m => m.GetAllFoodItems()).Returns(GetFoodItems());
            var foodItemsService = new FoodItemsService(mockRepository.Object, logger);

            //act
            var actual = await foodItemsService.FilterByPrice(min, max);
            var actualList = actual.ToList();
            //assert
            Assert.AreEqual(expectedList.Count(),actualList.Count());
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expectedList.Count(); i++)
                {
                    Assert.AreEqual(expectedList[i].Id,actualList[i].Id);
                    Assert.AreEqual(expectedList[i].Name,actualList[i].Name);
                    Assert.AreEqual(expectedList[i].Description,actualList[i].Description);
                    Assert.AreEqual(expectedList[i].Price,actualList[i].Price);
                    
                }
            });
        }

        [Test]
        public async Task GetFoodItemsByCategory_Category_FoodItems()
        {
            //arrange
            var mockRepository = new Mock<IFoodItemsRepository>();
            var mockLogger = new Mock<ILogger<FoodItemsService>>();
            var logger = mockLogger.Object;
            mockRepository.Setup(m => m.GetAllFoodItemsWithCategories()).Returns(GetFoodItemsWithCategory());
            var foodItemsService = new FoodItemsService(mockRepository.Object, logger);
            var expected = await GetFoodItemsWithCategory();
            var expectedList = expected.ToList();
            const string category = "Супы";
            //act
            var actual = await foodItemsService.GetFoodItemsByCategory(category);
            var actualList = actual.ToList();
            //assert
            Assert.AreEqual(expectedList.Count(), actualList.Count());
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expectedList.Count(); i++)
                {
                    Assert.AreEqual(expectedList[i].Price,actualList[i].Price);
                    Assert.AreEqual(expectedList[i].Name,actualList[i].Name);
                    Assert.AreEqual(expectedList[i].PictureUrl,actualList[i].PictureUrl);
                    Assert.AreEqual(expectedList[i].Description,actualList[i].Description);

                    var expectedCategories = expectedList[i].Categories.ToList();
                    var actualCategories = actualList[i].Categories.ToList();

                    for (int j = 0; j < expectedCategories.Count(); j++)
                    {
                        Assert.AreEqual(expectedCategories[j].Name,actualCategories[j].Name);
                    }
                }
            });
        }
    }
}