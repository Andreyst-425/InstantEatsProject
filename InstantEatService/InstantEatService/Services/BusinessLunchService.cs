using InstantEatService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public class BusinessLunchService : IBusinessLunch
    {
        private readonly IFoodItemsService _foodItems;

        public BusinessLunchService(IFoodItemsService foodItems)
        {
            _foodItems = foodItems;
        }

        public async Task<List<FoodItem>> GetBusinessLunchFirst()
        {
            var soups = await _foodItems.GetAllSoups();
            return soups.ToList();
        }

        public async Task<List<FoodItem>> GetBusinessLunchSecond()
        {
            var salads = await _foodItems.GetAllSalads();
            return salads.ToList();
        }

        public async Task<List<FoodItem>> GetBusinessLunchThird()
        {
            var burgers = await _foodItems.GetAllBurgers();
            return burgers.ToList();
        }
    }
}
