using InstantEatService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
    }
}