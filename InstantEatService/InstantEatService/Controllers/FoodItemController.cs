using InstantEatService.Dto;
using InstantEatService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace InstantEatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<List<FoodItemDto>> GetAll()
        {
            var foodItems = await _foodService.GetAll();
            return foodItems.Select(f => new FoodItemDto(f)).ToList();
        }


        /// <summary>
        /// Получить все блюда из категории супов
        /// </summary>
        /// <returns></returns>
        [HttpGet("foodItems/category/soup")]
        public async Task<IEnumerable<FoodItemDto>> GetSoups()
        {
            var soups = await _foodService.GetAllSoups();
            return soups.Select(f => new FoodItemDto(f));
        }

        /// <summary>
        /// Получить все блюда из категории салатов
        /// </summary>
        /// <returns></returns>
        [HttpGet("foodItems/category/salade")]
        public async Task<IEnumerable<FoodItemDto>> GetSalades()
        {
            var salades = await _foodService.GetAllSalades();
            return salades.Select(f => new FoodItemDto(f));
        }

        /// <summary>
        /// Получить все блюда из категории бургеров
        /// </summary>
        /// <returns></returns>
        [HttpGet("foodItems/category/burger")]
        public async Task<IEnumerable<FoodItemDto>> GetBurgers()
        {
            var burgers = await _foodService.GetAllBurgers();
            return burgers.Select(f => new FoodItemDto(f));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FoodItemDto>> Get(int id)
        {
            var foodItem = await _foodService.GetFoodItemById(id);
            if (foodItem == null) return NotFound();
            return Ok(new FoodItemDto(foodItem));
        }

        /// <summary>
        /// Создать новое блюдо
        /// </summary>
        /// <param name="newItemInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<FoodItemDto>> Add([FromBody] FoodItemCreateDto newItemInfo)
        {
            var foodItem = await _foodService.CreateFoodItem(newItemInfo);
            return Ok(new FoodItemDto(foodItem));
        }

        /// <summary>
        /// Обновить блюдо по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="foodItemCreateDto"></param>
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
        /// <param name="id"></param>
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
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [HttpGet("foodItems/filteredByPrice")]
        public async Task<IEnumerable<FoodItemDto>> FilterByPrice(double min, double max)
        {
            var foodItems = await _foodService.FilterByPrice(min, max);
            return foodItems.Select(f => new FoodItemDto(f));
        }
    }
}
