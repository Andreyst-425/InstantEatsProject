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
    //TODO: добавить описания для тестов 
    [TestFixture]
    public class ClientServiceTests
    {
        private async Task<Client> GetClient()
        {
            return new Client()
            {
                PhoneNumber = "1234567",
                Name = "new name",
                Id = 1 
            };
        }
        
        private async Task<List<Client>> GetClients()
        {
            var list = new List<Client>
            {
                new()
                {
                    PhoneNumber = "1",
                    Name = "1",
                    Id = 1,
                },
                new()
                {
                    PhoneNumber = "2",
                    Name = "2",
                    Id = 2,
                },
            };
            return list;
        }
        
        private async Task<bool> GetTrue()
        {
            return true;
        }
        
        private async Task<Client> GetNull()
        {
            return null;
        }
        
        [Test]
        public async Task AddClient_NotExistingPhoneNumber_ReturnsTrue()
        {
            //arrange
            var mockRepository = new Mock<IClientsRepository>();
            var mockLogger = new Mock<ILogger<ClientService>>();
            var logger = mockLogger.Object;
            var phone = "1234567";
            var createDto = new ClientCreateDto()
            {
                PhoneNumber = phone,
                Name = null,
            };
            mockRepository.Setup(m => m.CreateClient(createDto)).Returns(GetClient());
            mockRepository.Setup(m => m.GetClientByPhoneNumber(phone)).Returns(GetNull());
            var clientService = new ClientService(mockRepository.Object, logger);
            
            //act
            var actual = await clientService.AddClient(phone);
            
            //assert
            Assert.IsTrue(actual);
        }
        
        [Test]
        public async Task AddClient_ExistingPhoneNumber_ReturnsFalse()
        {
            //arrange
            var mockRepository = new Mock<IClientsRepository>();
            var mockLogger = new Mock<ILogger<ClientService>>();
            var logger = mockLogger.Object;
            var phone = "1234567";
            var createDto = new ClientCreateDto()
            {
                PhoneNumber = phone,
                Name = null,
            };
            mockRepository.Setup(m => m.CreateClient(createDto)).Returns(GetClient());
            mockRepository.Setup(m => m.GetClientByPhoneNumber(phone)).Returns(GetClient());
            var clientService = new ClientService(mockRepository.Object, logger);
            
            //act
            var actual = await clientService.AddClient(phone);
            
            //assert
            Assert.IsFalse(actual);
        }
        
        [Test]
        public async Task EditClientName_PhoneNumber_Name_ReturnsTrue()
        {
            //arrange
            var mockRepository = new Mock<IClientsRepository>();
            var mockLogger = new Mock<ILogger<ClientService>>();
            var logger = mockLogger.Object;
            var phone = "1234567";
            var name = "new name";

            mockRepository.Setup(m => m.UpdateClientName(phone,name)).Returns(GetTrue());
            var clientService = new ClientService(mockRepository.Object, logger);
            
            //act
            var actual = await clientService.UpdateClientName(phone, name);
            //assert
            Assert.IsTrue(actual);
        }

        [Test]
        public async Task GetAllClients_Clients()
        {
            //arrange
            var mockRepository = new Mock<IClientsRepository>();
            var mockLogger = new Mock<ILogger<ClientService>>();
            var logger = mockLogger.Object;
            mockRepository.Setup(m => m.GetAllClients()).Returns(GetClients());
           var clientService = new ClientService(mockRepository.Object, logger);
           var expected = await GetClients();

            //act
            var actualIEnumerable = await clientService.GetAllClients();
            var actual = actualIEnumerable.ToList();
            //assert

            Assert.AreEqual(expected.Count(),actual.Count());
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expected.Count(); i++)
                {
                    Assert.AreEqual(expected[i].Id,actual[i].Id);
                    Assert.AreEqual(expected[i].Name,actual[i].Name);
                    Assert.AreEqual(expected[i].PhoneNumber,actual[i].PhoneNumber);
                }
            });
        }

        [Test]
        public async Task GetClient_Id_Client()
        {
            //arrange
            var mockRepository = new Mock<IClientsRepository>();
            var mockLogger = new Mock<ILogger<ClientService>>();
            var logger = mockLogger.Object;
            var id = 1;
            mockRepository.Setup(m => m.GetClient(id)).Returns(GetClient());
            var clientService = new ClientService(mockRepository.Object, logger);
            var expected = await GetClient();

            //act
            var actual = await clientService.GetClient(id);
            
            //assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.Id,actual.Id);
                Assert.AreEqual(expected.Name,actual.Name);
                Assert.AreEqual(expected.PhoneNumber,actual.PhoneNumber);
            });
        }

        [Test]
        public async Task GetClientByPhoneNumber_Number_Client()
        {
            //arrange
            var mockRepository = new Mock<IClientsRepository>();
            var mockLogger = new Mock<ILogger<ClientService>>();
            var logger = mockLogger.Object;
            var phone = "1234567";
            var expected = await GetClient();
            mockRepository.Setup(m => m.GetClientByPhoneNumber(phone)).Returns(GetClient());
            var clientService = new ClientService(mockRepository.Object, logger);

            //act
            var actual = await clientService.GetClientByPhoneNumber(phone);
            
            //assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.Name,actual.Name);
                Assert.AreEqual(expected.Id,actual.Id);
                Assert.AreEqual(expected.PhoneNumber,actual.PhoneNumber);
            });
        }

        [Test]
        public async Task DeleteClient_Id_ReturnsTrue()
        {
            //arrange
            var mockRepository = new Mock<IClientsRepository>();
            var mockLogger = new Mock<ILogger<ClientService>>();
            var logger = mockLogger.Object;
            var id = 1;
            mockRepository.Setup(m => m.DeleteClient(id)).Returns(GetTrue());
            var clientService = new ClientService(mockRepository.Object, logger);
            
            //act
            var actual = await clientService.DeleteClient(id);
            
            //assert
            Assert.IsTrue(actual);
        }

        [Test]
        public async Task UpdateClient_Id_CreateDto_ReturnsTrue()
        {
            //arrange
            var mockRepository = new Mock<IClientsRepository>();
            var mockLogger = new Mock<ILogger<ClientService>>();
            var logger = mockLogger.Object;
            var phone = "1234567";
            var name = "new name";
            var id = 1;
            var createDto = new ClientCreateDto()
            {
                Name = name,
                PhoneNumber = phone
            };

            mockRepository.Setup(m => m.UpdateClient(id,createDto)).Returns(GetTrue());
            var clientService = new ClientService(mockRepository.Object, logger);
            
            //act
            var actual = await clientService.UpdateClient(id,createDto);
            //assert
            Assert.IsTrue(actual);
        }
 
    }
}