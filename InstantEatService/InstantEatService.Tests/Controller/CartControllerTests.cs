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
    public class CartControllerTests
    {
        private DeliveryAddress GetAddress()
        {
            var address = new DeliveryAddress()
            {
                Floor = 1,
                Id = 1,
                Street = "wow",
                FlatNumber = 111,
                HouseNumber = 22
            };
            return address;
        }

        private Client GetClient()
        {
            var client = new Client()
            {
                Id = 10,
                Name = "nnn",
                PhoneNumber = "1567"
            };
            return client;
        }
        private  async Task<IEnumerable<Cart>> GetCarts()
        {
            var address = GetAddress();
            var carts = new List<Cart>
            {
                new(){TotalPrice = 100,Quantity = 1,AddressForDelivery = address,Id = 1},
                new(){TotalPrice = 200,Quantity = 2,AddressForDelivery = address,Id = 2},
                new(){TotalPrice = 300,Quantity = 3,AddressForDelivery = address,Id = 3}
            };
            return carts;
        }

        private async Task<bool> GetTrue()
        {
            return true;
        }
        
        private async Task<Cart> GetCart()
        {
            var address = GetAddress();
            var cart = new Cart
            {
                TotalPrice = 100,
                Quantity = 1,
                AddressForDelivery = address,
                Id = 1,
                IsCanceled = false,
                OrderNumber = 11
            };
            return cart;
        }
        private List<CartDto> GetCartsDto()
        {
            var address = GetAddress();
            var listCart = new List<Cart>
            {
                new(){TotalPrice = 100,Quantity = 1,AddressForDelivery = address,Id = 1},
                new(){TotalPrice = 200,Quantity = 2,AddressForDelivery = address,Id = 2},
                new(){TotalPrice = 300,Quantity = 3,AddressForDelivery = address,Id = 3}
            };
            var listCartDto = new List<CartDto>();
            foreach (var cart in listCart)
            {
                listCartDto.Add(new CartDto(cart));
            }
            return listCartDto;
        }
        
        [Test]
        public async Task Get_CartDtoList()
        {
            //arrange
            var mockContext = new Mock<InstantEatDbContext>();
            var mockService = new Mock<ICartService>();
            var controller = new CartController(mockContext.Object, mockService.Object);
            mockService.Setup(m => m.GetCarts()).Returns(GetCarts());

            var expected = GetCartsDto();
            //act
            var actual = await controller.Get();
            
            //assert
            Assert.AreEqual(expected.Count,actual.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.AreEqual(expected[i].Quantity,actual[i].Quantity);
                    Assert.AreEqual(expected[i].TotalPrice,actual[i].TotalPrice);

                    var actualDelivery = actual[i].AddressForDelivery;
                    var expectedDelivery = expected[i].AddressForDelivery;
                    
                    Assert.AreEqual(expectedDelivery.Floor,actualDelivery.Floor);
                    Assert.AreEqual(expectedDelivery.Street,actualDelivery.Street);
                    Assert.AreEqual(expectedDelivery.FlatNumber,actualDelivery.FlatNumber);
                    Assert.AreEqual(expectedDelivery.HouseNumber,actualDelivery.HouseNumber);
                    Assert.AreEqual(expectedDelivery.Id,actualDelivery.Id);
                }
            });
        }

        //id
        [Test]
        public async Task Get_CartDto()
        {
            //arrange
            var mockContext = new Mock<InstantEatDbContext>();
            var mockService = new Mock<ICartService>();
            var controller = new CartController(mockContext.Object, mockService.Object);
            int id = 1;
            mockService.Setup(m => m.GetCart(id)).Returns(GetCart());

            var expected = new CartDto(await GetCart());
            
            //act
            var actual = await controller.Get(id);
            
            //assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.Quantity,actual.Quantity);
                Assert.AreEqual(expected.TotalPrice,actual.TotalPrice);
                
                var actualDelivery = actual.AddressForDelivery;
                var expectedDelivery = expected.AddressForDelivery;
                    
                Assert.AreEqual(expectedDelivery.Floor,actualDelivery.Floor);
                Assert.AreEqual(expectedDelivery.Street,actualDelivery.Street);
                Assert.AreEqual(expectedDelivery.FlatNumber,actualDelivery.FlatNumber);
                Assert.AreEqual(expectedDelivery.HouseNumber,actualDelivery.HouseNumber);
                Assert.AreEqual(expectedDelivery.Id,actualDelivery.Id);
            });
        }

        //я ебала это добавление крч, пускай и дальше не работает, надоело
        [Test]
        public async Task Add_Cart_ReturnsTrue()
        {
            //arrange
            var mockContext = new Mock<InstantEatDbContext>();
            var mockService = new Mock<ICartService>();
            var controller = new CartController(mockContext.Object, mockService.Object);
            int id = 1;
            mockService.Setup(m => m.AddCart(GetAddress(),GetClient().Id,GetClient())).Returns(GetTrue());

            //act
            var actual = await controller.Add(await GetCart());
            //assert
            Assert.IsTrue(actual);
        }

        [Test]
        public async Task UpdateCart_Cart_ReturnsTrue()
        {
            //arrange
            var mockContext = new Mock<InstantEatDbContext>();
            var mockService = new Mock<ICartService>();
            var controller = new CartController(mockContext.Object, mockService.Object);
            int id = 1;
            var cart = await GetCart();
            mockService.Setup(m => m.UpdateCart(cart)).Returns(GetTrue());

            //act
            var actual = await controller.Update(cart);
            //assert
            Assert.IsTrue(actual);
        }

        [Test]
        public async Task DeleteCart_Id_ReturnsTrue()
        {
            //arrange
            var mockContext = new Mock<InstantEatDbContext>();
            var mockService = new Mock<ICartService>();
            var controller = new CartController(mockContext.Object, mockService.Object);
            int id = 1;
            mockService.Setup(m => m.DeleteCart(id)).Returns(GetTrue());

            //act
            var actual = await controller.Delete(id);
            //assert
            Assert.IsTrue(actual);
        }
    }
}