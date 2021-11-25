using System.Threading.Tasks;
using InstantEatService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace InstantEatService.Tests.Service
{
    [TestFixture]
    public class InstantEatJsonDataServiceTests
    {
        [Test]
        public async Task PostFoodItems_ReturnsTrue()
        {
            //arrange
            var mockLogger = new Mock<ILogger<InstantEatJsonDataService>>();
            var mockDbContext = new Mock<InstantEatDbContext>();
            var mockConfig = new Mock<IConfiguration>();

            var ijsonService =
                new InstantEatJsonDataService(mockDbContext.Object, mockConfig.Object, mockLogger.Object);
            
        }
        
    }
}