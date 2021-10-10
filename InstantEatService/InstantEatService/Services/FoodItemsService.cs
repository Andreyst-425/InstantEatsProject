using InstantEatService.Models;
using InstantEatService.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public class FoodItemsService : IFoodItemsService
    {
        private readonly IFoodItemsRepository _foodItems;
        private readonly ILogger<FoodItemsService> _logger;

        public FoodItemsService(IFoodItemsRepository foodItems, ILogger<FoodItemsService> logger)
        {
            _foodItems = foodItems;
            _logger = logger;
        }

        public async Task<IEnumerable<FoodItem>> FilterByPrice(double min, double max)
        {
            var foodItems = await _foodItems.GetAllFoodItems(); 
            return foodItems.ToList().Where(f => f.Price > min && f.Price < max);
        }

        public async Task<IEnumerable<FoodItem>> GetAllSoups()
        {
            var foodItems = await _foodItems.GetAllFoodItems();
            return foodItems.ToList().Where(f => f.Categories.Any(c => c.Name == "Суп"));
        }

        public async Task<IEnumerable<FoodItem>> GetAllSalads()
        {
            var foodItems = await _foodItems.GetAllFoodItems();
            return foodItems.ToList().Where(f => f.Categories.Any(c => c.Name == "Салаты"));
        }

        public async Task<IEnumerable<FoodItem>> GetAllBurgers()
        {
            var foodItems = await _foodItems.GetAllFoodItems();
            return foodItems.ToList().Where(f => f.Categories.Any(c => c.Name == "Бургер"));
        }
    }
}
