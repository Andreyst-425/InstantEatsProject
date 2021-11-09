using InstantEatService.Dto;
using InstantEatService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodItemController : ControllerBase
    {
        private readonly IFoodItemsService _foodService;

        public FoodItemController(IFoodItemsService foodService)
        {
            _foodService = foodService;
        }

        /// <summary>
        /// Получить список всех блюд
        /// </summary>
        /// <returns></returns>
        [HttpGet("foodItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<FoodItemDto>> GetAll()
        {
            var foodItems = await _foodService.GetAll();
            return foodItems.Select(f => new FoodItemDto(f)).ToList();
        }

        /// <summary>
        /// Получить все блюда из категории "Супы"
        /// </summary>
        /// <returns></returns>
        [HttpGet("foodItems/category/soup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<FoodItemDto>> GetSoups()
        {
            var soups = await _foodService.GetAllSoups();
            return soups.Select(f => new FoodItemDto(f)).ToList();
        }

        /// <summary>
        /// Получить все блюда из категории "Салаты"
        /// </summary>
        /// <returns></returns>
        [HttpGet("foodItems/category/salade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<FoodItemDto>> GetSalades()
        {
            var salades = await _foodService.GetAllSalades();
            return salades.Select(f => new FoodItemDto(f)).ToList();
        }

        /// <summary>
        /// Получить все блюда из категории "Бургеры"
        /// </summary>
        /// <returns></returns>
        [HttpGet("foodItems/category/burger")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<FoodItemDto>> GetBurgers()
        {
            var burgers = await _foodService.GetAllBurgers();
            return burgers.Select(f => new FoodItemDto(f)).ToList();
        }

        /// <summary>
        /// Получить блюдо по id
        /// </summary>
        /// <param name="id"> идентификатор блюда </param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodItemDto>> GetById(int id)
        {
            var foodItem = await _foodService.GetFoodItemById(id);
            if (foodItem == null) return NotFound();
            return Ok(new FoodItemDto(foodItem));
        }

        /// <summary>
        /// Создать новое блюдо
        /// </summary>
        /// <param name="newItemInfo"> информация о блюде </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<FoodItemDto>> Add([FromBody] FoodItemCreateDto newItemInfo)
        {
            var foodItem = await _foodService.CreateFoodItem(newItemInfo);
            return Ok(new FoodItemDto(foodItem));
        }

        /// <summary>
        /// Обновить блюдо 
        /// </summary>
        /// <param name="id"> идентификатор блюда </param>
        /// <param name="foodItemCreateDto"> новая информация о блюде </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] FoodItemCreateDto foodItemCreateDto )
        {
            var isUpdated = await _foodService.UpdateFoodItem(id, foodItemCreateDto);
            return isUpdated ? Ok() : NotFound();
        }

        /// <summary>
        /// Удалить блюдо по id 
        /// </summary>
        /// <param name="id"> идентификатор блюда </param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var isDeleted = await _foodService.DeleteFoodItem(id);
            return isDeleted ? Ok() : NotFound();
        }

        /// <summary>
        /// Фильтровать по цене
        /// </summary>
        /// <param name="min"> минимальная цена </param>
        /// <param name="max"> максимальная цена </param>
        /// <returns></returns>
        [HttpGet("foodItems/filteredByPrice")]
        public async Task<IEnumerable<FoodItemDto>> FilterByPrice(double min, double max)
        {
            var foodItems = await _foodService.FilterByPrice(min, max);
            return foodItems.Select(f => new FoodItemDto(f));
        }
    }
}
