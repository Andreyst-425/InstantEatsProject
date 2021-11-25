using System.Collections.Generic;
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
    public class ClientControllerTests
    {
        private async Task<IEnumerable<Client>> GetClients()
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
        
        private async Task<Client> GetClient()
        {
            var client = new Client
            {
                Id = 10,
                Name = "name",
                PhoneNumber = "1234"
            };
            return client;
        }
        
        [Test]
        public async Task GetAll_Clients()
        {
            var mockService = new Mock<IClientService>();
            var controller = new ClientController(mockService.Object);
            mockService.Setup(m => m.GetAllClients()).Returns(GetClients());
            var clients = await GetClients();
            var expected = new List<ClientDto>();
            foreach (var client in clients)
            {
                expected.Add(new ClientDto(client));
            }

            var actual = await controller.GetAll();
            
            Assert.AreEqual(expected.Count,actual.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.AreEqual(expected[i].Name,actual[i].Name);
                    Assert.AreEqual(expected[i].PhoneNumber,actual[i].PhoneNumber);
                }
            });
        }

        [Test]
        public async Task GetClientById_Client_ClientDto()
        {
            var mockService = new Mock<IClientService>();
            var controller = new ClientController(mockService.Object);
            var id = 10;
            mockService.Setup(m => m.GetClient(id)).Returns(GetClient());

            var expected = await GetClient();
            var expectedDto = new ClientDto(expected);

            var actual = await controller.GetClientById(id);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedDto.Name,actual.Name);
                Assert.AreEqual(expectedDto.PhoneNumber,actual.PhoneNumber);
            });
        }

        [Test]
        public async Task GetClientByPhoneNumber_Number_ClientDto()
        {
            var mockService = new Mock<IClientService>();
            var controller = new ClientController(mockService.Object);
            var phone = "1234";
            mockService.Setup(m => m.GetClientByPhoneNumber(phone)).Returns(GetClient());

            var expected = await GetClient();
            var expectedDto = new ClientDto(expected);

            var actual = await controller.GetClientByPhoneNumber(phone);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedDto.Name,actual.Name);
                Assert.AreEqual(expectedDto.PhoneNumber,actual.PhoneNumber);
            });
        }

        [Test]
        public async Task AddClient_ReturnsTrue()
        {
            var mockService = new Mock<IClientService>();
            var controller = new ClientController(mockService.Object);
            var phone = "1234";
            mockService.Setup(m => m.AddClient(phone)).Returns(GetTrue());
            
            var actual = await controller.AddClient();
            
            Assert.IsTrue(actual);
        }

        [Test]
        public async Task EditClientName_ReturnsTrue()
        {
            var mockService = new Mock<IClientService>();
            var controller = new ClientController(mockService.Object);
            var phone = "1234";
            var name = "null";
            mockService.Setup(m => m.EditClientName(phone,name)).Returns(GetTrue());

            var actual = await controller.EditClientName();
            
            Assert.IsTrue(actual);
        }

        [Test]
        public async Task UpdateClient_Id_CreateDto_ReturnsTrue()
        {
            var mockService = new Mock<IClientService>();
            var controller = new ClientController(mockService.Object);
            int id = 10;
            var createDto = new ClientCreateDto()
            {
                Name = "new",
                PhoneNumber = "newtoo"
            };
            mockService.Setup(m => m.UpdateClient(id,createDto)).Returns(GetTrue());
            
            var actual = await controller.UpdateClient(id, createDto);
            
            Assert.IsTrue(actual);
        }

        [Test]
        public async Task DeleteClient_Id_ReturnsTrue()
        {
            var mockService = new Mock<IClientService>();
            var controller = new ClientController(mockService.Object);
            int id = 10;
            mockService.Setup(m => m.DeleteClient(id)).Returns(GetTrue());
            
            var actual = await controller.DeleteClient(id);
            
            Assert.IsTrue(actual);
        }
        
    }
}