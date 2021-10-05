using InstantEatService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstantEatJsonConroller : ControllerBase
    {
        private readonly IJsonDataService _jsonDataService;

        public InstantEatJsonConroller(IJsonDataService jsonDataService)
        {
            _jsonDataService = jsonDataService;
        }

        /// <summary>
        /// Положить foodItems в БД (имя файла должно быть food_items.json)
        /// Директория: ../InstantEatService\bin\Debug\net5.0
        /// </summary>
        [HttpPost("foodItems")]
        public async Task<bool> PostFoodItems()
        {
            var isPosted = await _jsonDataService.PostFoodItems();

            return isPosted;
        }


        /// <summary>
        /// Положить categories в БД (имя файла должно быть categories.json)
        /// Директория: ../InstantEatService\bin\Debug\net5.0
        /// </summary>
        [HttpPost("categories")]
        public async Task<bool> PostCategories()
        {
            var isPosted = await _jsonDataService.PostCategories();

            return isPosted;
        }

    }
}
