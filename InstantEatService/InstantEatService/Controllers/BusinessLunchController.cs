using InstantEatService.Dto;
using InstantEatService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessLunchController
    {
        private readonly IBusinessLunch _businessLunch;

        public BusinessLunchController(IBusinessLunch businessLunch)
        {
            _businessLunch = businessLunch;
        }

        [HttpGet("soups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<FoodItemDto>> GetSoups()
        {
            var soups = await  _businessLunch.GetBusinessLunchFirst();
            return soups.Select(s=>new FoodItemDto(s)).ToList();
        }

        [HttpGet("salades")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<FoodItemDto>> GetSalades()
        {
            var soups = await _businessLunch.GetBusinessLunchSecond();
            return soups.Select(s => new FoodItemDto(s)).ToList();
        }

        [HttpGet("burgers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<FoodItemDto>> GetBurgers()
        {
            var soups = await _businessLunch.GetBusinessLunchThird();
            return soups.Select(s => new FoodItemDto(s)).ToList();
        }

    }
}
