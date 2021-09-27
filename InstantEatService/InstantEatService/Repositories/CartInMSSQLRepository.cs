using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Models
{
    public class CartInMSSQLRepository : ICarts
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

        public async Task<Cart> GetCart(Guid id)
        {
            Logging(nameof(GetCart),nameof(id));

            await Task.CompletedTask;
            return _db.Carts.FirstOrDefault(c => c.Id == id);
        }

        public async Task<bool> DeleteCart(Guid id)
        {
            Logging(nameof(DeleteCart), nameof(id));

            var cart = await GetCart(id);
            cart.IsDeleted = true;
            cart.IsCanceled = true;
            await _db.SaveChangesAsync();
            await _db.DisposeAsync();
            return true;
        }

        public async Task<bool> RestoreCart(Guid id)
        {
            Logging(nameof(RestoreCart), nameof(id));

            var cart = await GetCart(id);
            cart.IsDeleted = false;
            await _db.SaveChangesAsync();
            await _db.DisposeAsync();
            return true;
        }

        public async Task<Cart> AddCart(Cart cart)
        {
            Logging(nameof(AddCart), nameof(cart));

            var newCart = new Cart
            {
                IsDeleted = cart.IsDeleted,
                ClientId = cart.ClientId,
                DeliveryAdress = cart.DeliveryAdress,
                FoodItems = cart.FoodItems,
                OrderNumber = cart.OrderNumber,
                Quantity = cart.Quantity,
                TotalPrice = cart.TotalPrice,
                IsCanceled = cart.IsCanceled
            };

            _db.Carts.Add(newCart);

            await _db.SaveChangesAsync();
            await _db.DisposeAsync();

            return newCart;
        }
        
        public async Task<bool> UpdateCart(Cart cart)
        {
            Logging(nameof(UpdateCart), nameof(cart));

            var updatingCart = await GetCart(cart.Id);
            if (updatingCart == null || updatingCart.IsDeleted)
                return false;
            _db.Carts.Update(cart);
            await _db.SaveChangesAsync();
            await _db.DisposeAsync();
            return true;
        }

    }
}
