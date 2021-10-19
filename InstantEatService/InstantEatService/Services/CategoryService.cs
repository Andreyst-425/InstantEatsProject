using InstantEatService.Models;
using InstantEatService.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _category;

        public CategoryService(ICategoryRepository category)
        {
            _category = category;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _category.GetAllCategories();
        }
    }
}
