using System.Threading.Tasks;
using InstantEatService.Controllers;
using InstantEatService.Services;
using Moq;
using NUnit.Framework;

namespace InstantEatService.Tests.Controller
{
    [TestFixture]
    public class InstantEatJsonControllerTests
    {
        
        private async Task<bool> GetTrue()
        {
            return true;
        }
        
        [Test]
        public async Task PostFoodItems()
        {
            var mockService = new Mock<IJsonDataService>();
            var controller = new InstantEatJsonController(mockService.Object);
            mockService.Setup(m => m.PostFoodItems()).Returns(GetTrue());

            var actual = await controller.PostFoodItems();
            
            Assert.IsTrue(actual);
        }
        
        [Test]
        public async Task PostCategories()
        {
            var mockService = new Mock<IJsonDataService>();
            var controller = new InstantEatJsonController(mockService.Object);
            mockService.Setup(m => m.PostCategories()).Returns(GetTrue());

            var actual = await controller.PostCategories();
            
            Assert.IsTrue(actual);
        }
    }
}