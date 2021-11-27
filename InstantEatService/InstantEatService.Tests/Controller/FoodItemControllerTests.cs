using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstantEatService.Controllers;
using InstantEatService.Dto;
using InstantEatService.Models;
using InstantEatService.Services;
using Moq;
using NUnit.Framework;
//TODO: вынести все методы для "подготовки" к тестам в отдельные файлы, а то больно много места занимают
namespace InstantEatService.Tests.Controller
{
    [TestFixture]
    public class FoodItemControllerTests
    {
        private async Task<bool> GetTrue()
        {
            return true;
        }
        private async Task<List<FoodItem>> GetFoodItems()
        {
            var list = new List<FoodItem>()
            {
                new(){Id = 1,Name = "food",Price = 189,PictureUrl = "url",Description = "vkusno"},
                new(){Id = 2,Name = "fo0od",Price = 19,PictureUrl = "u444rl",Description = "vkusnooo"},
                new(){Id = 3,Name = "food",Price = 309,PictureUrl = "pic",Description = "vvvkusno"}
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

        private async Task<IEnumerable<FoodItemDto>> GetFoodItemsDtoInPriceLimits()
        {
            var foodItems = await GetFoodItemsInPriceLimits();
            var listDto = new List<FoodItemDto>();
            foreach (var foodItem in foodItems)
            {
                listDto.Add(new FoodItemDto(foodItem));
            }
            return listDto;
        }
        private async Task<FoodItem> GetSingleFoodItem()
        {
            return new FoodItem
            {
                Name = "name",
                Description = "descrikj",
                Price = 2883,
                PictureUrl = "url",
                Id = 10,
                Categories = new List<Category>
                {
                    new()
                    {
                        Name = "супы"
                    }
                }
            };
        }
        private async Task<List<FoodItem>> GetFoodItemsWithCategories()
        {
            var items = await GetFoodItems();
            foreach (var item in items)
            {
                item.Categories = new List<Category>()
                {
                    new()
                    {
                        Name = "soups"
                    }
                };
            }

            return items;
        }

        private async Task<List<FoodItemDto>> GetFoodItemsDtoWithCategories()
        {
            var foodItems = await GetFoodItemsWithCategories();
            var listDto = new List<FoodItemDto>();
            foreach (var foodItem in foodItems)
            {
                listDto.Add(new FoodItemDto(foodItem));
            }

            return listDto;
        }
        private async Task<List<FoodItemDto>> GetFoodItemsDto()
        {
            var foodItems = await GetFoodItems();
            var listDto = new List<FoodItemDto>();
            foreach (var foodItem in foodItems)
            {
                listDto.Add(new FoodItemDto(foodItem));
            }

            return listDto;
        }
        
        [Test(Description = "Метод GetAll должен вернуть все блюда")]
        public async Task GetAll_FoodItemsDtoList()
        {
            var mockService = new Mock<IFoodItemsService>();

            var controller = new FoodItemController(mockService.Object);
            mockService.Setup(m => m.GetAll()).Returns(GetFoodItems());

            var expected = await GetFoodItemsDto();

            var actual = await controller.GetAll();
            
            Assert.AreEqual(expected.Count,actual.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.AreEqual(expected[i].Name,actual[i].Name);
                    Assert.AreEqual(expected[i].Price,actual[i].Price);
                    Assert.AreEqual(expected[i].Description,actual[i].Description);
                    Assert.AreEqual(expected[i].PictureUrl,actual[i].PictureUrl);
                }
            });
        }

        [Test(Description = "Метод GetFoodItemByCategory должен вернуть все блюда с их категориями")]
        public async Task GetFoodItemByCategory_CategoryName_FoodItemsDtoList()
        {
            var mockService = new Mock<IFoodItemsService>();
            var controller = new FoodItemController(mockService.Object);
            mockService.Setup(m => m.GetAllWithCategories()).Returns(GetFoodItemsWithCategories());
            var category = "soups";
            var expected = await GetFoodItemsDtoWithCategories();

            var actual = await controller.GetFoodItemByCategory(category);
            
            Assert.AreEqual(expected.Count,actual.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.AreEqual(expected[i].Name,actual[i].Name);
                    Assert.AreEqual(expected[i].Description,actual[i].Description);
                    Assert.AreEqual(expected[i].Price,actual[i].Price);
                    Assert.AreEqual(expected[i].PictureUrl,actual[i].PictureUrl);
                }
            });
        }

        [Test(Description = "Метод GetFoodItemById должен вернуть блюдо по его идентификатору")]
        public async Task GetById_Id_FoodItemDto()
        {
            var mockService = new Mock<IFoodItemsService>();

            var controller = new FoodItemController(mockService.Object);
            var id = 10;
            mockService.Setup(m => m.GetFoodItemById(id)).Returns(GetSingleFoodItem());

            var expected = await GetSingleFoodItem();
            var expectedDto = new FoodItemDto(expected);

            var actual = await controller.GetById(id);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedDto.Name,actual.Name);
                Assert.AreEqual(expectedDto.Description,actual.Description);
                Assert.AreEqual(expectedDto.Price,actual.Price);
                Assert.AreEqual(expectedDto.PictureUrl,actual.PictureUrl);
            });
        }

        [Test(Description = "Метод Add должен добавить блюдо")]
        public async Task Add_CreateDto_FoodItem()
        {
            var mockService = new Mock<IFoodItemsService>();

            var controller = new FoodItemController(mockService.Object);
            var id = 10;
            var createDto = new FoodItemCreateDto()
            {
                Name = "name",
                Description = "descrikj",
                Price = 2883,
                PictureUrl = "url"
            };
            mockService.Setup(m => m.CreateFoodItem(createDto)).Returns(GetSingleFoodItem());

            var expected = new FoodItemDto(await GetSingleFoodItem());
            var actual = await controller.Add(createDto);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.Description,actual.Description);
                Assert.AreEqual(expected.Name,actual.Name);
                Assert.AreEqual(expected.Price,actual.Price);
                Assert.AreEqual(expected.PictureUrl,actual.PictureUrl);
            });
            
        }

        [Test(Description = "Метод Update должен обновить данные блюда")]
        public async Task Update_Id_CreateDto_ReturnsTrue()
        {
            var mockService = new Mock<IFoodItemsService>();

            var controller = new FoodItemController(mockService.Object);
            var id = 10;
            var createDto = new FoodItemCreateDto()
            {
                Name = "name",
                Description = "descrikj",
                Price = 2883,
                PictureUrl = "url"
            };
            mockService.Setup(m => m.UpdateFoodItem(id,createDto)).Returns(GetTrue());

            var actual = await controller.Update(id,createDto);
            
            Assert.IsTrue(actual);
            
        }

        [Test(Description = "Метод Delete должен удалить блюдо и вернуть true")]
        public async Task Delete_Id_ReturnTrue()
        {
            var mockService = new Mock<IFoodItemsService>();
            var controller = new FoodItemController(mockService.Object);
            var id = 10;
            mockService.Setup(m => m.DeleteFoodItem(id)).Returns(GetTrue());

            var actual = await controller.Delete(id);
            
            Assert.IsTrue(actual);
        }

        [Test(Description = "Метод FilterByPrice должен вернуть все блюда в заданных ценовых пределах")]
        public async Task FilterByPrice_Min_Max_FoodItems()
        {
            var mockService = new Mock<IFoodItemsService>();
            var min = 50;
            var max = 60;
            var controller = new FoodItemController(mockService.Object);
            mockService.Setup(m => m.FilterByPrice(min,max)).Returns(GetFoodItemsInPriceLimits());

            var expectedIEnumerable = await GetFoodItemsDtoInPriceLimits();
            var expected = expectedIEnumerable.ToList();

            var actualIEnumerable = await controller.FilterByPrice(min,max);
            var actual = actualIEnumerable.ToList();
            
            Assert.AreEqual(expected.Count,actual.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.AreEqual(expected[i].Name,actual[i].Name);
                    Assert.AreEqual(expected[i].Price,actual[i].Price);
                    Assert.AreEqual(expected[i].Description,actual[i].Description);
                    Assert.AreEqual(expected[i].PictureUrl,actual[i].PictureUrl);
                }
            });
        }
    }
}



