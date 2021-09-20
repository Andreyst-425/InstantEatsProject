using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Models
{
    public interface ICarts
    {
        Task<Cart> AddCart(Cart cart);
        Task<bool> DeleteCart(Guid id);
        Task<IEnumerable<Cart>> GetAllCarts();
        Task<Cart> GetCart(Guid id);
        Task<bool> RestoreCart(Guid id);
        Task<bool> UpdateCart(Cart cart);
    }
}