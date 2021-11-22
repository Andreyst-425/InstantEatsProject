using InstantEatService.Models;
using InstantEatService.Tests.TestServices;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstantEatService.Repositories;

namespace InstantEatService.Tests.Repository
{
    [TestFixture]
    class CartsInMSSQLRepositoryTests
    {
        private DeliveryAddress GetDeliveryAddress()
        {
            DeliveryAddress address = new DeliveryAddress
            {
                Id = 0,
                FlatNumber = 10,
                Floor = 2,
                HouseNumber = 18,
                Street = "street"
            };
            return address;
        }
        private Client GetClientInfo()
        {
            Client client = new Client
            {
                Id = 1,
                Name = "client",
                PhoneNumber = "12345"
            };
            return client;
        }
        private List<Cart> GetCarts()
        {
            var address = GetDeliveryAddress();
            var client = GetClientInfo();
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

        private Cart GetCartInfo()
        {
            var address = GetDeliveryAddress();
            var client = GetClientInfo();
            var cart = new Cart
            {
                AddressForDelivery = address,
                Client = client,
                OrderNumber = 100,
                TotalPrice = 10,
                Quantity = 1,
                ClientId = 1,
                FoodItems = null,
                IsCanceled = false
            };
            return cart;
        }

        private List<Cart> GetCanceledCarts()
        {
            var carts = GetCarts();
            foreach (var cart in carts)
                cart.IsCanceled = true;
            return carts;
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
            //arrange
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

        [Test]
        public async Task RestoreCart_ExistingCartId_ReturnsTrue()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var carts = GetCanceledCarts();
            db.Add(carts[0]);
            db.SaveChanges();
            var existingId = 1;
            var mockLogger = new Mock<ILogger<CartsInMSSQLRepository>>();
            var logger = mockLogger.Object;
            var repository = new CartsInMSSQLRepository(db, logger);

            //act
            var actual = await repository.RestoreCart(existingId);

            //assert
            Assert.IsTrue(actual);
        }

        [Test]
        public async Task RestoreCart_NotExistingCartId_ReturnsFalse()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var carts = GetCanceledCarts();
            db.Add(carts[0]);
            db.SaveChanges();
            var notExistingId = 5;
            var mockLogger = new Mock<ILogger<CartsInMSSQLRepository>>();
            var logger = mockLogger.Object;
            var repository = new CartsInMSSQLRepository(db, logger);

            //act
            var actual = await repository.RestoreCart(notExistingId);

            //assert
            Assert.IsFalse(actual);
        }

        [Test]
        public async Task AddCart_CartData_ReturnsCart()
        {
            //assert
            var db = TestsRepositoryService.GetClearDataBase();
            var carts = GetCarts();
            var mockLogger = new Mock<ILogger<CartsInMSSQLRepository>>();
            var logger = mockLogger.Object;
            var repository = new CartsInMSSQLRepository(db, logger);

            var cart = GetCartInfo();
            var expected = cart;

            //act
            var actual = await repository.AddCart(cart);

            //assert
            Assert.Multiple(() => {
                Assert.AreEqual(expected.IsCanceled, actual.IsCanceled);
                Assert.AreEqual(expected.OrderNumber, actual.OrderNumber);
                Assert.AreEqual(expected.Quantity, actual.Quantity);
                Assert.AreEqual(expected.TotalPrice, actual.TotalPrice);
                Assert.AreEqual(expected.ClientId, actual.ClientId);

                var expectedAddress = expected.AddressForDelivery;
                var actualAddress = actual.AddressForDelivery;

                Assert.AreEqual(expectedAddress.FlatNumber, actualAddress.FlatNumber);
                Assert.AreEqual(expectedAddress.Floor, actualAddress.Floor);
                Assert.AreEqual(expectedAddress.HouseNumber, actualAddress.HouseNumber);
                Assert.AreEqual(expectedAddress.Street, actualAddress.Street);
            });
        }

        [Test]
        public async Task UpdateCart_ExistingCartData_ReturnsTrue()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var cart = GetCartInfo();
            db.Add(cart);
            db.SaveChanges();
            var address = GetDeliveryAddress();
            var client = GetClientInfo();
            var existingId = cart.Id;
            var updatingCart = cart;
            updatingCart.OrderNumber = 12;
            var mockLogger = new Mock<ILogger<CartsInMSSQLRepository>>();
            var logger = mockLogger.Object;
            var repository = new CartsInMSSQLRepository(db, logger);

            //act
            var actual = await repository.UpdateCart(updatingCart,existingId);

            //assert
            Assert.IsTrue(actual);
        }

        [Test]
        public async Task UpdateCart_NotExistingCartData_ReturnsFalse()
        {
            //arrange
            var db = TestsRepositoryService.GetClearDataBase();
            var cart = GetCartInfo();
            db.Add(cart);
            db.SaveChanges();
            var address = GetDeliveryAddress();
            var client = GetClientInfo();
            var existingId = 100;
            var updatingCart = cart;
            updatingCart.OrderNumber = 12;
            var mockLogger = new Mock<ILogger<CartsInMSSQLRepository>>();
            var logger = mockLogger.Object;
            var repository = new CartsInMSSQLRepository(db, logger);

            //act
            var actual = await repository.UpdateCart(updatingCart, existingId);

            //assert
            Assert.IsFalse(actual);
        }
    }
}
