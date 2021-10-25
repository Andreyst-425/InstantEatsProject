using InstantEatService.Models;
using InstantEatService.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoriesRepository _category;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoriesRepository category, ILogger<CategoryService> logger)
        {
            _category = category;
            _logger = logger;
        }
        public void Logging(string methodName)
        {
            _logger.LogTrace($"{methodName}is worked out (CategoryService)");
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            Logging(nameof(GetCategories));
            return await _category.GetAllCategories();
        }
    }
}
