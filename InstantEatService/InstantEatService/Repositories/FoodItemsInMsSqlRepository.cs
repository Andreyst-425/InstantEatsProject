using InstantEatService.Dto;
using InstantEatService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Repositories
{
    public class FoodItemsInMsSqlRepository : IDisposable, IFoodItemsRepository
    {
        private readonly InstantEatDbContext _db;
        private readonly ILogger<FoodItemsInMsSqlRepository> _logger;

        public FoodItemsInMsSqlRepository(InstantEatDbContext db, ILogger<FoodItemsInMsSqlRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        private void Logging(string methodName)
        {
            _logger.LogTrace($"{methodName}is worked out (FoodItemsInMSSQLRepository)");
        }
        private void Logging(string methodName, string param)
        {
            _logger.LogTrace($"{methodName}({param})is worked out (FoodItemsInMSSQLRepository)");
        }
        private void Logging(string methodName, string param1, string param2)
        {
            _logger.LogTrace($"{methodName}({param1}, {param2}) is worked out (FoodItemsInMSSQLRepository)");
        }
        private void ThrowException(string paramName)
        {
            throw new NullReferenceException($"{nameof(paramName)} is empty or null");
        }
        private void ThrowException()
        {
            throw new NullReferenceException($"database (foodItems) is empty or null");
        }

        public async Task<IEnumerable<FoodItem>> GetAllFoodItems()
        {
            await Task.CompletedTask;
            Logging(nameof(GetAllFoodItems));

            if (_db.FoodItems == null) ThrowException();

            return _db.FoodItems;
        }
        public async Task<IEnumerable<FoodItem>> GetAllFoodItemsWithCategories()
        {
            await Task.CompletedTask;
            Logging(nameof(GetAllFoodItems));

            if (_db.FoodItems == null) ThrowException();

            return _db.FoodItems.Include(c=>c.Categories);
        }

        public async Task<FoodItem> GetFoodItem(int id)
        {
            Logging(nameof(GetFoodItem), nameof(id));

            if (id == 0) ThrowException(nameof(id));
            return await _db.FoodItems.FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<FoodItem> CreateFoodItem(FoodItemCreateDto foodItemCreateDto)
        {
            Logging(nameof(CreateFoodItem), nameof(foodItemCreateDto));

            if (foodItemCreateDto == null) ThrowException(nameof(foodItemCreateDto));

            var newFoodItem = foodItemCreateDto.ToEntity();

            await _db.FoodItems.AddAsync(newFoodItem);
            await _db.SaveChangesAsync();
            return newFoodItem;
        }


        public async Task<bool> UpdateFoodItem(int id, FoodItemCreateDto foodItemCreateDto)
        {
            Logging(nameof(UpdateFoodItem), nameof(id), nameof(foodItemCreateDto));

            if (foodItemCreateDto == null) ThrowException(nameof(foodItemCreateDto));
            if (id == 0) ThrowException(nameof(id));

            var foodItem = await GetFoodItem(id);
            if (foodItem == null) return false;
            foodItem.Name = foodItemCreateDto.Name;
            foodItem.Price = foodItemCreateDto.Price;
            foodItem.PictureUrl = foodItemCreateDto.PictureUrl;
            foodItem.Description = foodItemCreateDto.Description;

            _db.Update(foodItem);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteFoodItem(int id)
        {
            Logging(nameof(DeleteFoodItem),nameof(id));

            if (id == 0) ThrowException(nameof(id));

            var foodItem = await GetFoodItem(id);
            if (foodItem == null) return false;
            _db.Remove(foodItem);
            await _db.SaveChangesAsync();

            return true;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
