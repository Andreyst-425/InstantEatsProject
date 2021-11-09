using InstantEatService.Dto;
using InstantEatService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly InstantEatDbContext _db;
        private readonly ICategoryService _categories;

        public CategoryController(InstantEatDbContext db, ICategoryService categories)
        {
            _db = db;
            _categories = categories;
        }

        /// <summary>
        /// Получение всех объектов Category
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<CategoryDto>> Get()
        {
            var categories = await _categories.GetCategories();
            return categories.Select(c => new CategoryDto(c));
        }
    }
}
