using InstantEatService.Models;
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
    class CartsInMSSQLRepositoryTests
    {
        private List<Cart> GetCarts()
        {
            DeliveryAddress address = new DeliveryAddress { 
                Id = 0,
                FlatNumber = 10,
                Floor = 2,
                HouseNumber = 18,
                Street = "street"
            };
            Client client = new Client
            {
                Id = 1,
                Name = "client",
                PhoneNumber = "12345"
            };
            return new List<Cart>()
            {
                new Cart() {
                    Id = 1,
                    AddressForDelivery = address,
                    ClientId=1,
                    IsCanceled=false,
                    OrderNumber=10,
                    Quantity=2,
                    TotalPrice=1000,
                    Client= client
                },
                new Cart() {
                    Id = 2,
                    AddressForDelivery = address,
                    ClientId=1,
                    IsCanceled=false,
                    OrderNumber=11,
                    Quantity=4,
                    TotalPrice=100,
                    Client= client
                },
                new Cart() {
                    Id = 3,
                    AddressForDelivery = address,
                    ClientId=1,
                    IsCanceled=false,
                    OrderNumber=12,
                    Quantity=90,
                    TotalPrice=109990,
                    Client= client
                },
            };
        }

        [Test]
        public async Task GetAllCarts_Carts()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var carts = GetCarts();
            db.Add(carts[0]);
            db.SaveChanges();
            var mockLogger = new Mock<ILogger<CartsInMSSQLRepository>>();
            var logger = mockLogger.Object;
            var repository = new CartsInMSSQLRepository(db, logger);

            //act
            List<Cart> actual = new List<Cart>();
            var items = await repository.GetAllCarts();
            foreach (var item in items)
            {
                actual.Add(item);
            }
            //assert
            Assert.IsTrue(actual.Count() == 1, "GetAll returned no items");
        }

        [Test]
        public async Task GetCart_Id_Cart()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var carts = GetCarts();
            db.Add(carts[0]);
            db.SaveChanges();
            var mockLogger = new Mock<ILogger<CartsInMSSQLRepository>>();
            var logger = mockLogger.Object;
            var repository = new CartsInMSSQLRepository(db, logger);
            var id = 1;
            var expected = carts[0];
            //act
            var actual = await repository.GetCart(id);
            //assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.IsCanceled, actual.IsCanceled);
                Assert.AreEqual(expected.OrderNumber, actual.OrderNumber);
                Assert.AreEqual(expected.Quantity, actual.Quantity);
                Assert.AreEqual(expected.TotalPrice, actual.TotalPrice);
                Assert.AreEqual(expected.ClientId, actual.ClientId);

                var expectedAddress = expected.AddressForDelivery;
                var actualAddress = actual.AddressForDelivery;

                Assert.AreEqual(expectedAddress.Id, actualAddress.Id);
                Assert.AreEqual(expectedAddress.FlatNumber, actualAddress.FlatNumber);
                Assert.AreEqual(expectedAddress.Floor, actualAddress.Floor);
                Assert.AreEqual(expectedAddress.HouseNumber, actualAddress.HouseNumber);
                Assert.AreEqual(expectedAddress.Street, actualAddress.Street);

                var expectedClient = expected.Client;
                var actualClient = actual.Client;

                Assert.AreEqual(expectedClient.Id, actualClient.Id);
                Assert.AreEqual(expectedClient.Name, actualClient.Name);
                Assert.AreEqual(expectedClient.PhoneNumber, actualClient.PhoneNumber);
            });
        }

        [Test]
        public async Task DeleteCart_ExistingCartId_ReturnsTrue()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var carts = GetCarts();
            db.Add(carts[0]);
            db.SaveChanges();
            var existingId = 1;
            var mockLogger = new Mock<ILogger<CartsInMSSQLRepository>>();
            var logger = mockLogger.Object;
            var repository = new CartsInMSSQLRepository(db, logger);

            //act
            var actual = await repository.DeleteCart(existingId);

            //assert
            Assert.IsTrue(actual);
        }

        [Test]
        public async Task DeleteCart_NotExistingCartId_ReturnsFalse()
        {
            var db = TestsRepositoryService.GetClearDataBase();
            var carts = GetCarts();
            db.Add(carts[0]);
            db.SaveChanges();
            var notExistingId = 5;
            var mockLogger = new Mock<ILogger<CartsInMSSQLRepository>>();
            var logger = mockLogger.Object;
            var repository = new CartsInMSSQLRepository(db, logger);

            //act
            var actual = await repository.DeleteCart(notExistingId);

            //assert
            Assert.IsFalse(actual);
        }


    }
}
