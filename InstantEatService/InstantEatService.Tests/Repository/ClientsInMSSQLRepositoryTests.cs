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
    class ClientsInMSSQLRepositoryTests
    {
        private List<Client> GetClients()
        {
            var clients = new List<Client>
            {
                new Client{Name = "c1",PhoneNumber = "123"},
                new Client{Name = "c2",PhoneNumber = "456"},
                new Client{Name = "c3",PhoneNumber = "789"},
            };
            return clients;
        }

        [Test]
        public async Task GetAllClients_Clients()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var clients = GetClients();
            foreach (var client in clients)
            {
                db.Add(client);
            }
            db.SaveChanges();
            var expected = clients;
            var mockLogger = new Mock<ILogger<ClientsInMsSqlRepository>>();
            var logger = mockLogger.Object;
            var repository = new ClientsInMsSqlRepository(db, logger);

            //act
            var actual = await repository.GetAllClients();

            //assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.Count(), actual.Count());
                for (int i = 0; i < expected.Count(); i++)
                {
                    Assert.AreEqual(expected[i].Id, actual[i].Id);
                    Assert.AreEqual(expected[i].Name, actual[i].Name);
                    Assert.AreEqual(expected[i].PhoneNumber, actual[i].PhoneNumber);
                }
            });
        }

        [Test]
        public async Task GetClient_Id_Client()
        {
            
        }
    }
}
