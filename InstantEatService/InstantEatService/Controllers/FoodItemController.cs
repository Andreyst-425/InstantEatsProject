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
        /// Получить все блюда из заданной категории 
        /// </summary>
        /// <returns></returns>
        [HttpGet("foodItems/category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<FoodItemDto>> GetFoodItemByCategory(string category)
        {
            var foodItems = await _foodService.GetFoodItemsByCategory(category);
            return foodItems.Select(f => new FoodItemDto(f)).ToList();
        }
        
        /// <summary>
        /// Получить блюдо по id
        /// </summary>
        /// <param name="id"> идентификатор блюда </param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<FoodItemDto> GetById(int id)
        {
            //проверка на null добавлена в сервис
            var foodItem = await _foodService.GetFoodItemById(id);
            return new FoodItemDto(foodItem);
        }

        /// <summary>
        /// Создать новое блюдо
        /// </summary>
        /// <param name="newItemInfo"> информация о блюде </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<FoodItemDto> Add([FromBody] FoodItemCreateDto newItemInfo)
        {
            var foodItem = await _foodService.CreateFoodItem(newItemInfo);
            return new FoodItemDto(foodItem);
        }

        /// <summary>
        /// Обновить блюдо 
        /// </summary>
        /// <param name="id"> идентификатор блюда </param>
        /// <param name="foodItemCreateDto"> новая информация о блюде </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<bool> Update(int id, [FromBody] FoodItemCreateDto foodItemCreateDto )
        {
            var isUpdated = await _foodService.UpdateFoodItem(id, foodItemCreateDto);
            return isUpdated;
        }

        /// <summary>
        /// Удалить блюдо по id 
        /// </summary>
        /// <param name="id"> идентификатор блюда </param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            var isDeleted = await _foodService.DeleteFoodItem(id);
            return isDeleted;
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
