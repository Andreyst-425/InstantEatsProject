using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Models
{
    public interface ICarts
    {
        Task<Cart> AddCart(Cart cart);
        Task<bool> DeleteCart(int id);
        Task<IEnumerable<Cart>> GetAllCarts();

        Task<Cart> GetCart(int id);
        Task<bool> RestoreCart(int id);
        Task<bool> UpdateCart(Cart cart);
    }
}