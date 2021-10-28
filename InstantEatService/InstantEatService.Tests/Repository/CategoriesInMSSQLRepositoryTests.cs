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
    class CategoriesInMSSQLRepositoryTests
    {
        private List<Category> GetCategories()
        {
            var items = new List<Category>
            {
                new Category { Name = "category1",Id = 1},
                new Category { Name = "category2",Id = 2},
                new Category { Name = "category3",Id = 3},
            };
            return items;
        }
        [Test]
        public async Task GetAllCategories_Categories()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var categories = GetCategories();
            foreach(var category in categories)
            {
                db.Add(category);
            }
            db.SaveChanges();
            var mockLogger = new Mock<ILogger<CategoriesInMSSQLRepository>>();
            var logger = mockLogger.Object;
            var repository = new CategoriesInMSSQLRepository(db,logger);
            var expected = categories;
            //act
            var actual = new List<Category>();
            var items = await repository.GetAllCategories();
            foreach (var item in items)
            {
                actual.Add(item);
            }

            //assert
            Assert.Multiple(() => {
                Assert.IsTrue(items.Count() > 0, "GetAll returned no items");

                Assert.AreEqual(expected[0].Id, actual[0].Id);
                Assert.AreEqual(expected[0].Name, actual[0].Name);
                Assert.AreEqual(expected[1].Id, actual[1].Id);
                Assert.AreEqual(expected[1].Name, actual[1].Name);
                Assert.AreEqual(expected[2].Id, actual[2].Id);
                Assert.AreEqual(expected[2].Name, actual[2].Name);
            });
            
        }
    }
}
