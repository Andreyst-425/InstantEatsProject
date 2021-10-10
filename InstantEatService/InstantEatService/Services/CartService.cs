using InstantEatService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public class CartService : ICartService
    {
        private readonly ICarts _carts;

        public CartService(ICarts carts)
        {
            _carts = carts;
        }

        public async Task<IEnumerable<Cart>> GetCarts()
        {
            return await _carts.GetAllCarts();
        }

        public async Task<Cart> GetCart(int id)
        {
            return await _carts.GetCart(id);
        }

        public async Task<List<Cart>> GetCurrentCarts()
        {
            var carts = await _carts.GetAllCarts();
            return carts.Where(c=>c.IsCanceled == false).ToList();
        }

        public async Task<Cart> AddCart(DeliveryAddress address, int clientId, [FromBody] Client client)
        {
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

            newCart.Client = client;

            return await _carts.AddCart(newCart);
        }

        public async Task<List<FoodItem>> GetBisunessLunch()
        {
            await Task.CompletedTask;

            return null;
        }
        public async Task<bool> DeleteCart(int id)
        {
            return await _carts.DeleteCart(id);
        }

        public async Task<bool> UpdateCart(Cart cart)
        {
            return await _carts.UpdateCart(cart);
        }
    }
}
