using InstantEatService.DtoModels;
using InstantEatService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace InstantEatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemController : ControllerBase
    {
        /// <summary>
        /// Получить список всех блюд
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodItemDto>>> GetAllFoodItems()
        {
            await Task.CompletedTask;
            return Ok();
        }

        /// <summary>
        /// Получить блюдо по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodItemDto>> GetFoodItem(Guid id)
        {
            await Task.CompletedTask;
            return Ok();
        }

        /// <summary>
        /// Создать новое блюдо
        /// </summary>
        /// <param name="foodItemDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostFoodItem([FromBody] FoodItemDto foodItemDto)
        {
            await Task.CompletedTask;
            return Ok();
        }

        /// <summary>
        /// Обновить блюдо
        /// </summary>
        /// <param name="id"></param>
        /// <param name="foodItemDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> PutFoodItem(Guid id, [FromBody] FoodItemDto foodItemDto )
        {
            await Task.CompletedTask;
            return Ok();
        }

        /// <summary>
        /// Удалить блюдо
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFoodItem(Guid id)
        {
            await Task.CompletedTask;
            return Ok();
        }
    }
}
