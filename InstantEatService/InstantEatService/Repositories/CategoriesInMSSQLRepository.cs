using InstantEatService.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Repositories
{
    public class CategoriesInMSSQLRepository : ICategoriesRepository
    {
        private readonly InstantEatDbContext _db;
        private readonly ILogger<CategoriesInMSSQLRepository> _logger;

        public CategoriesInMSSQLRepository(InstantEatDbContext db, ILogger<CategoriesInMSSQLRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        private void Logging(string methodName)
        {
            _logger.LogTrace($"{methodName}is worked out (CartsInMSSQLRepository)");
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            await Task.CompletedTask;
            Logging(nameof(GetAllCategories));
            return _db.Categories;
        }

    }
}
