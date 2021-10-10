using InstantEatService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public interface ICartService
    {
        Task<Cart> AddCart(DeliveryAddress address, int clientId, [FromBody] Client client);
        Task<bool> DeleteCart(int id);

        Task<List<FoodItem>> GetBisunessLunch();
        Task<Cart> GetCart(int id);
        Task<IEnumerable<Cart>> GetCarts();
        Task<bool> UpdateCart(Cart cart);
        Task<List<Cart>> GetCurrentCarts();

    }
}