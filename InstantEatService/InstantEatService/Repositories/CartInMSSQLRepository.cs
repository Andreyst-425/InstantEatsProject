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


        public async Task<IEnumerable<Cart>> GetAllCarts()
        {
            await Task.CompletedTask;
            return _db.Carts;
        }

        public async Task<Cart> GetCart(Guid id)
        {
            await Task.CompletedTask;
            return _db.Carts.FirstOrDefault(c => c.Id == id);
        }

        public async Task<bool> DeleteCart(Guid id)
        {
            var cart = await GetCart(id);
            cart.IsDeleted = true;
            await _db.SaveChangesAsync();
            await _db.DisposeAsync();
            return true;
        }

        public async Task<bool> RestoreCart(Guid id)
        {
            var cart = await GetCart(id);
            cart.IsDeleted = false;
            await _db.SaveChangesAsync();
            await _db.DisposeAsync();
            return true;
        }

        public async Task<Cart> AddCart(Cart cart)
        {
            var newCart = new Cart
            {
                IsDeleted = cart.IsDeleted,
                ClientId = cart.ClientId,
                DeliveryAdress = cart.DeliveryAdress,
                FoodItems = cart.FoodItems,
                OrderNumber = cart.OrderNumber,
                PaymentType = cart.PaymentType,
                Quantity = cart.Quantity,
                TotalPrice = cart.TotalPrice
            };

            _db.Carts.Add(newCart);

            await _db.SaveChangesAsync();
            await _db.DisposeAsync();

            return newCart;
        }

        public async Task<bool> UpdateCart(Cart cart)
        {
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
