using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstantEatService.Controllers;
using InstantEatService.Dto;
using InstantEatService.Models;
using InstantEatService.Services;
using Moq;
using NUnit.Framework;

namespace InstantEatService.Tests.Controller
{
    [TestFixture]
    public class CategoryControllerTests
    {
        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = new List<Category>()
            {
                new() {Id = 1, Name = "111"},
                new() {Id = 2, Name = "111"}
            };
            return categories;
        }
        
        [Test]
        public async Task Get_Categories()
        {
            var mockService = new Mock<ICategoryService>();
            var mockContext = new Mock<InstantEatDbContext>();
            var controller = new CategoryController(mockContext.Object, mockService.Object);

            mockService.Setup(m => m.GetCategories()).Returns(GetCategories());
            var expectedIEnumerable = await GetCategories();
            var expectedList = expectedIEnumerable.ToList();
            var expected = new List<CategoryDto>();
            foreach (var category in expectedList)
            {
                expected.Add(new CategoryDto(category));
            }

            var actualIEnumerable = await controller.Get();

            var actual = actualIEnumerable.ToList();

            Assert.AreEqual(expected.Count,actual.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.AreEqual(expected[i].Name,actual[i].Name);
                }
            });

        }
    }
}