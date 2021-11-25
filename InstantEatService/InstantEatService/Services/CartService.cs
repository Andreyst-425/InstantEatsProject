using InstantEatService.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstantEatService.Repositories;

namespace InstantEatService.Services
{
    public class CartService : ICartService
    {
        private readonly ICartsRepository _carts;
        private readonly ILogger<CartService> _logger;

        public void Logging(string methodName)
        {
            _logger.LogTrace($"{methodName}is worked out");
        }
        public void Logging(string methodName, string param)
        {
            _logger.LogTrace($"{methodName}({param})is worked out");
        }
        public void Logging(string methodName, string param1, string param2, string param3)
        {
            _logger.LogTrace($"{methodName}({param1},{param2},{param3})is worked out");
        }

        public CartService(ICartsRepository carts, ILogger<CartService> logger)
        {
            _carts = carts;
            _logger = logger;
        }

        public async Task<IEnumerable<Cart>> GetCarts()
        {
            Logging(nameof(GetCarts));
            return await _carts.GetAllCarts();
        }

        public async Task<Cart> GetCart(int id)
        {
            Logging(nameof(GetCart), nameof(id));
            return await _carts.GetCart(id);
        }

        public async Task<List<Cart>> GetCurrentCarts()
        {
            Logging(nameof(GetCurrentCarts));
            var carts = await _carts.GetAllCarts();
            return carts.Where(c=>c.IsCanceled == false).ToList();
        }

        public async Task<bool> AddCart(DeliveryAddress address, int clientId, Client client)
        {
            Logging(nameof(AddCart), nameof(address),nameof(clientId),nameof(client));
            
            var newCart = new Cart
            {
                ClientId = clientId,
                AddressForDelivery = address,
                Quantity = 0,
                TotalPrice = 0,
                FoodItems = null,
                OrderNumber = 0,
                IsCanceled = false
            };

            //newCart.Client = client; 
            await _carts.AddCart(newCart);
            return true;
            //return await _carts.AddCart(newCart);
        }

        public async Task<bool> DeleteCart(int id)
        {
            Logging(nameof(DeleteCart), nameof(id));
            return await _carts.DeleteCart(id);
        }

        public async Task<bool> UpdateCart(Cart cart)
        {
            Logging(nameof(UpdateCart), nameof(cart));
            return await _carts.UpdateCart(cart,cart.Id);
        }
    }
}
