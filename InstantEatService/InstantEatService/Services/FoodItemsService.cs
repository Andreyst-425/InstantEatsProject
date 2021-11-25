using System;
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
        private readonly ILogger<FoodItemsService> _logger;

        public FoodItemsService(IFoodItemsRepository foodItems, ILogger<FoodItemsService> logger)
        {
            _foodItems = foodItems;
            _logger = logger;
        }
        private void Logging(string methodName)
        {
            _logger.LogTrace($"{methodName}is worked out (FoodItemsService)");
        }

        private void Logging(string methodName, string param)
        {
            _logger.LogTrace($"{methodName}({param})is worked out (FoodItemsService)");
        }

        private void Logging(string methodName, string param1, string param2)
        {
            _logger.LogTrace($"{methodName}({param1},{param2})is worked out (FoodItemsService)");
        }
        public async Task<List<FoodItem>> GetAll()
        {
            Logging(nameof(GetAll));
            var items = await _foodItems.GetAllFoodItems();
            return items.ToList();
        }

        public async Task<List<FoodItem>> GetAllWithCategories()
        {
            Logging(nameof(GetAllWithCategories));
            var items = await _foodItems.GetAllFoodItemsWithCategories();
            return items.ToList();
        }

        public async Task<FoodItem> GetFoodItemById(int id)
        {
            Logging(nameof(GetFoodItemById), nameof(id));
            var item = await _foodItems.GetFoodItem(id);
            if (item == null)
                throw new NullReferenceException("is null or empty.");
            return item;
        }

        public async Task<FoodItem> CreateFoodItem(FoodItemCreateDto foodItemCreateDto)
        {
            Logging(nameof(CreateFoodItem),nameof(foodItemCreateDto));
            var newFoodItem = await _foodItems.CreateFoodItem(foodItemCreateDto);
            return newFoodItem;
        }

        public async Task<bool> UpdateFoodItem(int id, FoodItemCreateDto foodItemCreateDto)
        {
            Logging(nameof(UpdateFoodItem), nameof(id), nameof(foodItemCreateDto));
            var IsUpdated = await _foodItems.UpdateFoodItem(id, foodItemCreateDto);
            return IsUpdated;
        }

        public async Task<bool> DeleteFoodItem(int id)
        {
            Logging(nameof(DeleteFoodItem), nameof(id));
            var IsDeleted = await _foodItems.DeleteFoodItem(id);
            return IsDeleted;
        }

        public async Task<IEnumerable<FoodItem>> FilterByPrice(double min, double max)
        {
            Logging(nameof(FilterByPrice), nameof(min),nameof(max));
            var foodItems = await GetAll();
            return foodItems.Where(f => f.Price > min && f.Price < max);
        }

        public async Task<IEnumerable<FoodItem>> GetFoodItemsByCategory(string category)
        {
            Logging(nameof(GetFoodItemsByCategory));
            var foodItems = await GetAllWithCategories();
            var result = foodItems.Where(s => s.Categories.Select(c => c.Name).First() == category);
            return result;
        }

    }
}
