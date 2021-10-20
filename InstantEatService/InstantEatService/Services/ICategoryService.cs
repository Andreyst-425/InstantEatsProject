using InstantEatService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public interface ICategoryService
    {
        /// <summary>
        /// Получить категории
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Category>> GetCategories();
    }
}