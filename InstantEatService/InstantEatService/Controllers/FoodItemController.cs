using InstantEatService.Dto;
using InstantEatService.Models;
using InstantEatService.Repositories;
using InstantEatService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace InstantEatService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodItemController : ControllerBase
    {
        private readonly IFoodItemsRepository _foodItems;
        private readonly IFoodItemsService _foodService;

        public FoodItemController(IFoodItemsRepository foodItems, IFoodItemsService foodService)
        {
            _foodItems = foodItems;
            _foodService = foodService;
        }

        /// <summary>
        /// Получить список всех блюд
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<FoodItemDto> GetAllFoodItems()
        {
           // await Task.CompletedTask;
            var foodItems = _foodItems.GetAllFoodItems();
            return foodItems.Select(f => new FoodItemDto(f)).ToArray();
        }

        //    /// <summary>
        //    /// Получить блюдо по id
        //    /// </summary>
        //    /// <param name="id"></param>
        //    /// <returns></returns>
        //    [HttpGet("{id}")]
        //    public async Task<ActionResult<FoodItemDto>> GetFoodItem(int id)
        //    {
        //        var foodItem = await _foodItems.GetFoodItem(id);

        //        if (foodItem == null) return NotFound();

        //        return Ok(new FoodItemDto(foodItem));
        //    }

        //    /// <summary>
        //    /// Создать новое блюдо
        //    /// </summary>
        //    /// <param name="foodItemCreateDto"></param>
        //    /// <returns></returns>
        //    [HttpPost]
        //    public async Task<ActionResult<FoodItemDto>> PostFoodItem([FromBody] FoodItemCreateDto foodItemCreateDto)
        //    {
        //        var foodItem = await _foodItems.CreateFoodItem(foodItemCreateDto);
        //        return Ok(new FoodItemDto(foodItem));
        //    }

        //    /// <summary>
        //    /// Обновить блюдо по id
        //    /// </summary>
        //    /// <param name="id"></param>
        //    /// <param name="foodItemCreateDto"></param>
        //    /// <returns></returns>
        //    [HttpPut("{id}")]
        //    public async Task<ActionResult> PutFoodItem(int id, [FromBody] FoodItemCreateDto foodItemCreateDto )
        //    {
        //        var isUpdated = await _foodItems.UpdateFoodItem(id, foodItemCreateDto);
        //        return isUpdated ? Ok() : NotFound();
        //    }

        //    /// <summary>
        //    /// Удалить блюдо по id 
        //    /// </summary>
        //    /// <param name="id"></param>
        //    /// <returns></returns>
        //    [HttpDelete("{id}")]
        //    public async Task<ActionResult> DeleteFoodItem(int id)
        //    {
        //        var isDeleted = await _foodItems.DeleteFoodItem(id);
        //        return isDeleted ? Ok() : NotFound();
        //    }


        //    /// <summary>
        //    /// Фильтровать по цене
        //    /// </summary>
        //    /// <param name="min"></param>
        //    /// <param name="max"></param>
        //    /// <returns></returns>
        //    [HttpGet("foodItems/filteredByPrice")]
        //    public async Task<IEnumerable<FoodItemDto>> FilterByPrice(double min, double max)
        //    {
        //        await Task.CompletedTask;
        //        var foodItems = _foodService.FilterByPrice(min, max);
        //        return foodItems.Select(f => new FoodItemDto(f));
        //    }
        //}
    }
}
