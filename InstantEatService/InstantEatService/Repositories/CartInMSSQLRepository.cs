using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Models
{
    public class CartInMSSQLRepository : IDisposable, ICarts
    {
        private readonly InstantEatDbContext _db;
        private readonly ILogger<CartInMSSQLRepository> _logger;

        public CartInMSSQLRepository(InstantEatDbContext db, ILogger<CartInMSSQLRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        private void Logging(string methodName)
        {
            var method = methodName;
            _logger.LogTrace($"{method}is worked out");
        }

        private void Logging(string methodName, string param)
        {
            var method = methodName;
            _logger.LogTrace($"{method}({param})is worked out");

        }

        public async Task<IEnumerable<Cart>> GetAllCarts()
        {
            Logging(nameof(GetAllCarts));

            await Task.CompletedTask;
            return _db.Carts;
        }

        public async Task<Cart> GetCart(int id)
        {
            Logging(nameof(GetCart),nameof(id));

            await Task.CompletedTask;
            return _db.Carts.FirstOrDefault(c => c.Id == id);
        }

        public async Task<bool> DeleteCart(int id)
        {
            Logging(nameof(DeleteCart), nameof(id));

            var cart = await GetCart(id);
            cart.IsCanceled = true;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RestoreCart(int id)
        {
            Logging(nameof(RestoreCart), nameof(id));

            var cart = await GetCart(id);

            _db.Remove(cart);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Cart> AddCart(Cart cart)
        {
            Logging(nameof(AddCart), nameof(cart));

            var newCart = new Cart
            {
                ClientId = cart.ClientId,
                AddressForDelivery = cart.AddressForDelivery,
                FoodItems = cart.FoodItems,
                OrderNumber = cart.OrderNumber,
                Quantity = cart.Quantity,
                TotalPrice = cart.TotalPrice,
                IsCanceled = cart.IsCanceled
            };

            _db.Carts.Add(newCart);
            await _db.SaveChangesAsync();
            return newCart;
        }
        
        public async Task<bool> UpdateCart(Cart cart)
        {
            Logging(nameof(UpdateCart), nameof(cart));

            var updatingCart = await GetCart(cart.Id);
            if (updatingCart == null)
                return false;
            _db.Carts.Update(cart);
            await _db.SaveChangesAsync();
            return true;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
