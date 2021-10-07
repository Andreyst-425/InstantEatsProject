using InstantEatService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Repositories
{
    public class CategoryInMSSQLRepository : ICategoryRepository
    {
        private readonly InstantEatDbContext _db;
        private readonly ILogger<CategoryInMSSQLRepository> _logger;

        public CategoryInMSSQLRepository(InstantEatDbContext db, ILogger<CategoryInMSSQLRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        private void Logging(string methodName)
        {
            _logger.LogTrace($"{methodName}is worked out");
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            await Task.CompletedTask;
            Logging(nameof(GetAllCategories));

            return _db.Categories;
        }

    }
}
