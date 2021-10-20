using InstantEatService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Repositories
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Category>> GetAllCategories();
    }
}