using InstantEatService.Dto;
using InstantEatService.Models;
using InstantEatService.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public class FoodItemsService : IFoodItemsService
    {
        private readonly IFoodItemsRepository _foodItems;
        private readonly ICategoryService _category;
        private readonly ILogger<FoodItemsService> _logger;

        public FoodItemsService(IFoodItemsRepository foodItems, ICategoryService category, ILogger<FoodItemsService> logger)
        {
            _foodItems = foodItems;
            _category = category;
            _logger = logger;
        }

        public async Task<List<FoodItem>> GetAll()
        {
            var items = await _foodItems.GetAllFoodItems();
            return items.ToList();
        }
        public async Task<List<FoodItem>> GetAllWithCategories()
        {
            var items = await _foodItems.GetAllFoodItemsWithCategories();
            return items.ToList();
        }

        public async Task<FoodItem> GetFoodItemById(int id)
        {
            var item = await _foodItems.GetFoodItem(id);
            return item;
        }
        public async Task<FoodItem> CreateFoodItem(FoodItemCreateDto foodItemCreateDto)
        {
            var newFoodItem = await _foodItems.CreateFoodItem(foodItemCreateDto);
            return newFoodItem;
        }
        public async Task<bool> UpdateFoodItem(int id, FoodItemCreateDto foodItemCreateDto)
        {
            var IsUpdated = await _foodItems.UpdateFoodItem(id, foodItemCreateDto);
            return IsUpdated;
        }
        public async Task<bool> DeleteFoodItem(int id)
        {
            var IsDeleted = await _foodItems.DeleteFoodItem(id);
            return IsDeleted;
        }


        public async Task<IEnumerable<FoodItem>> FilterByPrice(double min, double max)
        {
            var foodItems = await GetAll();
            return foodItems.Where(f => f.Price > min && f.Price < max);
        }

        public async Task<IEnumerable<FoodItem>> GetAllSoups()
        {
            var soup = await GetAllWithCategories();
            var result = soup.Where(s => s.Categories.Select(c => c.Name).First() == "Супы");
            return result;
        }

        public async Task<IEnumerable<FoodItem>> GetAllSalades()
        {
            var salades = await GetAllWithCategories();
            var result = salades.Where(s => s.Categories.Select(c => c.Name).First() == "Салаты");
            return result;
        }
        public async Task<IEnumerable<FoodItem>> GetAllBurgers()
        {
            var burger = await GetAllWithCategories();
            var result = burger.Where(s => s.Categories.Select(c => c.Name).First() == "Бургеры");
            return result;
        }
    }
}
