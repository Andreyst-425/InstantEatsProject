using InstantEatService.Models;
using InstantEatService.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstantEatService.Repositories;

namespace InstantEatService.Tests.Service
{
    [TestFixture]
    class CartServiceTests
    {
       
        private async Task<Cart> GetSingleCart()
        {
            var newCart = new Cart
            {
                Id = 10, IsCanceled = false, ClientId = 10, OrderNumber = 11, TotalPrice = 100, Quantity = 15
            };
            return newCart;
        }

        private DeliveryAddress GetAddressInfo()
        {
            var address = new DeliveryAddress
            {
                Id = 10,
                Floor = 7, 
                Street = "Street", 
                FlatNumber = 98, 
                HouseNumber = 98
            };
            return address;
        }

        private async Task<bool> GetTrue()
        {
            return true;
        }
        private Client GetClientInfo()
        {
            var client = new Client()
            {
                Id = 10,
                Name = "anem",
                PhoneNumber = "12678656"
            };
            return client;
        }
        private async Task<Cart> GetNewCart()
        {
            var address =  GetAddressInfo();
            var client = GetClientInfo();
            var newCart = new Cart
            {
                ClientId = client.Id,
                AddressForDelivery = address,
                Quantity = 0,
                TotalPrice = 0,
                FoodItems = null,
                OrderNumber = 0,
                IsCanceled = false
            };
            return newCart;
        }
        
        private async Task<IEnumerable<Cart>> GetCarts()
        {
            await Task.CompletedTask;
            IEnumerable<Cart> carts = new List<Cart>
            {
                new Cart {Id = 0, IsCanceled = false, ClientId = 0, OrderNumber = 11, TotalPrice = 100, Quantity = 15},
                new Cart {Id = 1, IsCanceled = false, ClientId = 1, OrderNumber = 12, TotalPrice = 120, Quantity = 35},
                new Cart {Id = 2, IsCanceled = false, ClientId = 2, OrderNumber = 80, TotalPrice = 10, Quantity = 90},
            };
            return carts;
        }

        [Test]
        public async Task GetCarts_Carts()
        {
            //arrange
            var mockRepository = new Mock<ICartsRepository>();
            var mockLogger = new Mock<ILogger<CartService>>();
            var logger = mockLogger.Object;
            mockRepository.Setup(m => m.GetAllCarts()).Returns(GetCarts());

            var cartService = new CartService(mockRepository.Object, logger);
            var expected = await GetCarts();
            //act
            var actual = await cartService.GetCarts();
            //assert
            Assert.AreEqual(expected.Count(), actual.Count());
            var expectedList = expected.ToList();
            var actualList = actual.ToList();
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expected.Count(); i++)
                {
                    Assert.AreEqual(expectedList[i].Id, actualList[i].Id);
                    Assert.AreEqual(expectedList[i].IsCanceled, actualList[i].IsCanceled);
                    Assert.AreEqual(expectedList[i].OrderNumber, actualList[i].OrderNumber);
                    Assert.AreEqual(expectedList[i].Quantity, actualList[i].Quantity);
                    Assert.AreEqual(expectedList[i].TotalPrice, actualList[i].TotalPrice);
                    Assert.AreEqual(expectedList[i].ClientId, actualList[i].ClientId);
                }
            });
        }

        [Test]
        public async Task GetCart_ExistingId_Cart()
        {
            //arrange
            var mockRepository = new Mock<ICartsRepository>();
            var mockLogger = new Mock<ILogger<CartService>>();
            var logger = mockLogger.Object;
            var existingId = 10;
            var cart = GetSingleCart();
            mockRepository.Setup(m => m.GetCart(existingId)).Returns(cart);

            var cartService = new CartService(mockRepository.Object, logger);
            var expected = await GetSingleCart();

            //act
            var actual = await cartService.GetCart(existingId);

            //assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.IsCanceled, actual.IsCanceled);
                Assert.AreEqual(expected.OrderNumber, actual.OrderNumber);
                Assert.AreEqual(expected.Quantity, actual.Quantity);
                Assert.AreEqual(expected.TotalPrice, actual.TotalPrice);
                Assert.AreEqual(expected.ClientId, actual.ClientId);
            });
        }

        [Test]
        public async Task GetCurrentCarts_Carts()
        {
            //arrange
            var mockRepository = new Mock<ICartsRepository>();
            var mockLogger = new Mock<ILogger<CartService>>();
            var logger = mockLogger.Object;
            mockRepository.Setup(m => m.GetAllCarts()).Returns(GetCarts());
            var cartService = new CartService(mockRepository.Object, logger);
            var expected = await GetCarts();
            var expectedList = expected
                .Where(c => c.IsCanceled == false)
                .ToList();

            //act
            var actual = await cartService.GetCurrentCarts();
            
            //assert
            Assert.AreEqual(expected.Count(), actual.Count());
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expected.Count(); i++)
                {
                    Assert.AreEqual(expectedList[i].Id, actual[i].Id);
                    Assert.AreEqual(expectedList[i].IsCanceled, actual[i].IsCanceled);
                    Assert.AreEqual(expectedList[i].OrderNumber, actual[i].OrderNumber);
                    Assert.AreEqual(expectedList[i].Quantity, actual[i].Quantity);
                    Assert.AreEqual(expectedList[i].TotalPrice, actual[i].TotalPrice);
                    Assert.AreEqual(expectedList[i].ClientId, actual[i].ClientId);
                }
            });
        }

        [Test]
        public async Task AddCart_CartInfo_NewCart()
        {
            //arrange
            var mockRepository = new Mock<ICartsRepository>();
            var mockLogger = new Mock<ILogger<CartService>>();
            var logger = mockLogger.Object;
            var expected = await GetNewCart();
            var expectedTask = GetNewCart();
            var address = GetAddressInfo();
            var client = GetClientInfo();
            mockRepository.Setup(m => m.AddCart(expected)).Returns(expectedTask);
            var cartService = new CartService(mockRepository.Object, logger);
            //act
            var actual = await cartService.AddCart(address, client.Id, client);
            //arrange
            Assert.IsNotNull(actual);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.ClientId, actual.ClientId);

                var expectedClient = expected.Client;
                var actualClient = actual.Client;
                Assert.AreEqual(expectedClient.Name,actualClient.Name);
                Assert.AreEqual(expectedClient.PhoneNumber,actualClient.PhoneNumber);
                Assert.AreEqual(expectedClient.Id,actualClient.Id);

                var expectedAddress = expected.AddressForDelivery;
                var actualAddress = actual.AddressForDelivery;
                Assert.AreEqual(expectedAddress.Floor,actualAddress.Floor);
                Assert.AreEqual(expectedAddress.Street,actualAddress.Street);
                Assert.AreEqual(expectedAddress.FlatNumber,actualAddress.FlatNumber);
                Assert.AreEqual(expectedAddress.HouseNumber,actualAddress.HouseNumber);
                Assert.AreEqual(expectedAddress.Id,actualAddress.Id); 
            });
        }

        [Test]
        public async Task DeleteCart_CartId_ReturnsTrue()
        {
            //arrange
            var mockRepository = new Mock<ICartsRepository>();
            var mockLogger = new Mock<ILogger<CartService>>();
            var logger = mockLogger.Object;
            var id = 10;
            mockRepository.Setup(m => m.DeleteCart(id)).Returns(GetTrue());
            var cartService = new CartService(mockRepository.Object, logger);
            
            //act
            var actual = await cartService.DeleteCart(id);
            
            //arrange
            Assert.IsTrue(actual);
        }

        [Test]
        public async Task UpdateCart_CartInfo_ReturnsTrue()
        {
            //arrange
            var mockRepository = new Mock<ICartsRepository>();
            var mockLogger = new Mock<ILogger<CartService>>();
            var logger = mockLogger.Object;
            var existingId = 10;
            var cart = await GetSingleCart();
            mockRepository.Setup(m => m.UpdateCart(cart,cart.Id)).Returns(GetTrue());

            var cartService = new CartService(mockRepository.Object, logger);
            
            //act
            var actual = await cartService.UpdateCart(cart);
            
            //assert
            Assert.IsTrue(actual);
        }
    }
}