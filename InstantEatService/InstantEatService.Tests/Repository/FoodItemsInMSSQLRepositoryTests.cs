using InstantEatService.Dto;
using InstantEatService.Models;
using InstantEatService.Repositories;
using InstantEatService.Tests.TestServices;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantEatService.Tests.Repository
{
    [TestFixture]
    class FoodItemsInMSSQLRepositoryTests
    {

        private List<FoodItem> GetFoodItems()
        {
            return new List<FoodItem> {
                new FoodItem(){
                    Id = 1,
                    Description = "desc 1",
                    Name = "dishh",
                    Price = 10,
                    PictureUrl = "picture.com",
                },
                new FoodItem(){
                    Id = 2,
                    Description = "desc 2",
                    Name = "dish",
                    Price = 102,
                    PictureUrl = "pictuRe.com",
                }
            };
        }

        [Test]
        public async Task GetAllFoodItems_FoodItems()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var foodItems = GetFoodItems();
            foreach(var item in foodItems)
            {
                db.Add(item);
            }
            db.SaveChanges();
            var mockLogger = new Mock<ILogger<FoodItemsInMsSqlRepository>>();
            var logger = mockLogger.Object;
            var repository = new FoodItemsInMsSqlRepository(db, logger);

            //act
            var actualIEnumerable = await repository.GetAllFoodItems();
            var actual = new List<FoodItem>();
            foreach (var item in actualIEnumerable)
            {
                actual.Add(item);
            }

            //assert
            Assert.Multiple(() => {
                Assert.AreEqual(foodItems.Count, actual.Count);
                for (int i = 0;i< foodItems.Count;i++)
                {
                    Assert.AreEqual(foodItems[i].Description,actual[i].Description);
                    Assert.AreEqual(foodItems[i].Id, actual[i].Id);
                    Assert.AreEqual(foodItems[i].Name, actual[i].Name);
                    Assert.AreEqual(foodItems[i].PictureUrl, actual[i].PictureUrl);
                    Assert.AreEqual(foodItems[i].Price, actual[i].Price);
                }
            });
        }

        //TODO: разобраться с иключениями
        [Test]
        public async Task GetAllFoodItems_ThrowsException()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var mockLogger = new Mock<ILogger<FoodItemsInMsSqlRepository>>();
            var logger = mockLogger.Object;
            var repository = new FoodItemsInMsSqlRepository(db, logger);

            //assert
            var ex = Assert.Throws<NullReferenceException>(()=> repository.GetAllFoodItems());
        }

        [Test]
        public async Task GetAllFoodItemsWithCategories_FoodItemsAndTheirCategories()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var foodItems = GetFoodItems();
            foreach (var item in foodItems)
            {
                db.Add(item);
            }
            db.SaveChanges();
            var mockLogger = new Mock<ILogger<FoodItemsInMsSqlRepository>>();
            var logger = mockLogger.Object;
            var repository = new FoodItemsInMsSqlRepository(db, logger);

        }

        [Test]
        public async Task GetFoodItem_ExistingId_FoodItem()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var foodItems = GetFoodItems();
            foreach (var item in foodItems)
            {
                db.Add(item);
            }
            db.SaveChanges();
            var mockLogger = new Mock<ILogger<FoodItemsInMsSqlRepository>>();
            var logger = mockLogger.Object;
            var repository = new FoodItemsInMsSqlRepository(db, logger);
            var existingId = 1;
            var expected = foodItems[0];

            //act
            var actual = await repository.GetFoodItem(existingId);

            //assert
            Assert.Multiple(() => {
                Assert.AreEqual(expected.Description, actual.Description);
                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.Name, actual.Name);
                Assert.AreEqual(expected.PictureUrl, actual.PictureUrl);
                Assert.AreEqual(expected.Price, actual.Price);
            });
        }

        //TODO: разобраться с иключениями
        [Test]
        public async Task GetFoodItem_NullId_ThrowsException()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var foodItems = GetFoodItems();
            foreach (var item in foodItems)
            {
                db.Add(item);
            }
            db.SaveChanges();
            var mockLogger = new Mock<ILogger<FoodItemsInMsSqlRepository>>();
            var logger = mockLogger.Object;
            var repository = new FoodItemsInMsSqlRepository(db, logger);
            var nullId = 0;

            //act
            //var actual = await repository.GetFoodItem(nullId);

            var ex = Assert.Throws<NullReferenceException>(()=> repository.GetFoodItem(nullId));
        }

        [Test]
        public async Task CreateFoodItem_FoodItemCreateDto_newFoodItem()
        {
            var db = TestsRepositoryService.GetClearDataBase();
            var foodItems = GetFoodItems();
            foreach (var item in foodItems)
            {
                db.Add(item);
            }
            db.SaveChanges();
            var mockLogger = new Mock<ILogger<FoodItemsInMsSqlRepository>>();
            var logger = mockLogger.Object;
            var repository = new FoodItemsInMsSqlRepository(db, logger);

            var createDto = new FoodItemCreateDto
            {
                Price = 999,
                PictureUrl = "picture",
                Name = "createDto",
                Description = "ochen' vkusno chestno"
            };
            var expected = createDto.ToEntity();

            //act
            var actual = await repository.CreateFoodItem(createDto);

            //assert

            Assert.Multiple(()=>
            {
                Assert.AreEqual(expected.Description, actual.Description);
                Assert.AreEqual(expected.Name, actual.Name);
                Assert.AreEqual(expected.PictureUrl, actual.PictureUrl);
                Assert.AreEqual(expected.Price, actual.Price);
            });
        }

        [Test]
        public async Task CreateFoodItem_NullCreateDto_ThrowsException()
        {

        }
        
        [Test]
        public async Task UpdateFoodItem_Id_FoodItemCreateDto_ReturnsTrue()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var foodItems = GetFoodItems();
            foreach (var item in foodItems)
            {
                db.Add(item);
            }
            db.SaveChanges();
            var mockLogger = new Mock<ILogger<FoodItemsInMsSqlRepository>>();
            var logger = mockLogger.Object;
            var repository = new FoodItemsInMsSqlRepository(db, logger);

            var foodItem = foodItems[0];
            var id = foodItems[0].Id;
            var updatingCreateDto = new FoodItemCreateDto { 
                Price = foodItem.Price,
                PictureUrl = foodItem.PictureUrl,
                Name = foodItem.Name,
                Description = foodItem.Description
            };
            //act
            Assert.IsTrue(await repository.UpdateFoodItem(id, updatingCreateDto));
        }
        [Test]
        public async Task UpdateFoodItem_NotExistingId_NullFoodItemCreateDto_ReturnsFalse()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var foodItems = GetFoodItems();
            foreach (var item in foodItems)
            {
                db.Add(item);
            }
            db.SaveChanges();
            var mockLogger = new Mock<ILogger<FoodItemsInMsSqlRepository>>();
            var logger = mockLogger.Object;
            var repository = new FoodItemsInMsSqlRepository(db, logger);

            var foodItem = foodItems[0];
            var id = 14;
            var updatingCreateDto = new FoodItemCreateDto
            {
                Price = foodItem.Price,
                PictureUrl = foodItem.PictureUrl,
                Name = foodItem.Name,
                Description = foodItem.Description
            };
            //act
            Assert.IsFalse(await repository.UpdateFoodItem(id, updatingCreateDto));
        }

        //TODO: разобраться с иключениями
        [Test]
        public async Task UpdateFoodItem_NullId_FoodItemCreateDto_ThrowsException()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var foodItems = GetFoodItems();
            foreach (var item in foodItems)
            {
                db.Add(item);
            }
            db.SaveChanges();
            var mockLogger = new Mock<ILogger<FoodItemsInMsSqlRepository>>();
            var logger = mockLogger.Object;
            var repository = new FoodItemsInMsSqlRepository(db, logger);

            var foodItem = foodItems[0];
            var id = 0;
            var updatingCreateDto = new FoodItemCreateDto
            {
                Price = foodItem.Price,
                PictureUrl = foodItem.PictureUrl,
                Name = foodItem.Name,
                Description = foodItem.Description
            };
            //act
            Assert.Throws<NullReferenceException>(()=> repository.UpdateFoodItem(id, updatingCreateDto));
        }

        //TODO: разобраться с иключениями
        [Test]
        public async Task UpdateFoodItem_Id_NullFoodItemCreateDto_ThrowsException()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var foodItems = GetFoodItems();
            foreach (var item in foodItems)
            {
                db.Add(item);
            }
            db.SaveChanges();
            var mockLogger = new Mock<ILogger<FoodItemsInMsSqlRepository>>();
            var logger = mockLogger.Object;
            var repository = new FoodItemsInMsSqlRepository(db, logger);

            var foodItem = foodItems[0];
            var id = foodItems[0].Id;
            var updatingCreateDto = new FoodItemCreateDto();
            updatingCreateDto = null;
            //act
            Assert.Throws<NullReferenceException>(() => repository.UpdateFoodItem(id, updatingCreateDto));
        }

        [Test]
        public async Task DeleteFoodItem_ExistingId_ReturnsTrue()
        {
            var db = TestsRepositoryService.GetClearDataBase();
            var foodItems = GetFoodItems();
            foreach (var item in foodItems)
            {
                db.Add(item);
            }
            db.SaveChanges();
            var mockLogger = new Mock<ILogger<FoodItemsInMsSqlRepository>>();
            var logger = mockLogger.Object;
            var repository = new FoodItemsInMsSqlRepository(db, logger);
            var existingId = 1;

            Assert.IsTrue(await repository.DeleteFoodItem(existingId));
        }

        [Test]
        public async Task DeleteFoodItem_NotExistingId_ReturnsFalse()
        {
            var db = TestsRepositoryService.GetClearDataBase();
            var foodItems = GetFoodItems();
            foreach (var item in foodItems)
            {
                db.Add(item);
            }
            db.SaveChanges();
            var mockLogger = new Mock<ILogger<FoodItemsInMsSqlRepository>>();
            var logger = mockLogger.Object;
            var repository = new FoodItemsInMsSqlRepository(db, logger);
            var notExistingId = 111;

            Assert.IsFalse(await repository.DeleteFoodItem(notExistingId));
        }

        //TODO: разобраться с иключениями
        [Test]
        public async Task DeleteFoodItem_NullId_ThrowException()
        {
            var db = TestsRepositoryService.GetClearDataBase();
            var foodItems = GetFoodItems();
            foreach (var item in foodItems)
            {
                db.Add(item);
            }
            db.SaveChanges();
            var mockLogger = new Mock<ILogger<FoodItemsInMsSqlRepository>>();
            var logger = mockLogger.Object;
            var repository = new FoodItemsInMsSqlRepository(db, logger);
            var nullId = 0;

            Assert.Throws<NullReferenceException>(()=> repository.DeleteFoodItem(nullId));
        }

    }
}
