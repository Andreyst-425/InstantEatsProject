using InstantEatService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public interface ICartService
    {
        Task<Cart> AddCart(string address, PaymentType paymentType, Guid clientId, [FromBody] Client client);
        Task<bool> DeleteCart(Guid id);
        Task<Cart> GetCart(Guid id);
        Task<IEnumerable<Cart>> GetCarts();
        Task<bool> UpdateCart(Cart cart);
    }
}