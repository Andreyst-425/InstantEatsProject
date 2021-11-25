
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstantEatService.Models;
using InstantEatService.Repositories;
using InstantEatService.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace InstantEatService.Tests
{
    [TestFixture]
    class CategoryServiceTests
    {
        private async Task<IEnumerable<Category>> GetCategories()
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
        public async Task GetCategories_Categories()
        {
            //arrange
            var mockRepository = new Mock<ICategoriesRepository>();
            var mockLogger = new Mock<ILogger<CategoryService>>();
            var logger = mockLogger.Object;
            mockRepository.Setup(m => m.GetAllCategories()).Returns(GetCategories());

            var categoryService = new CategoryService(mockRepository.Object, logger);
            var expected = await GetCategories();
            var expectedList  = expected.ToList();
            
            //act
            var actual = await categoryService.GetCategories();
            var actualList = actual.ToList();
            
            //assert
            Assert.AreEqual(expectedList.Count(), actualList.Count());
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expectedList.Count(); i++)
                {
                    Assert.AreEqual(expectedList[i].Name,actualList[i].Name);
                }
            });
        }
    }
    
}