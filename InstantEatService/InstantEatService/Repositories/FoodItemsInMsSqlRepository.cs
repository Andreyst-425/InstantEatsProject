using InstantEatService.DtoModels;
using InstantEatService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<FoodItem> GetAllFoodItems()
        {
            var function = nameof(GetAllFoodItems);
            _logger.LogTrace($"{function}() is worked out");

            return _db.FoodItems;
        }

        public async Task<FoodItem> GetFoodItem(Guid id)
        {
            var function = nameof(GetFoodItem);
            _logger.LogTrace($"{function}({nameof(id)}) is worked out");

            if (id == Guid.Empty)
                throw new NullReferenceException($"{nameof(id)} is empty");

            return await _db.FoodItems.FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<FoodItem> CreateFoodItem(FoodItemCreateDto foodItemCreateDto)
        {
            var function = nameof(CreateFoodItem);
            _logger.LogTrace($"{function}({nameof(foodItemCreateDto)}) is worked out");

            if (foodItemCreateDto == null)
                throw new NullReferenceException($"{nameof(foodItemCreateDto)} param is null");


            var newFoodItem = foodItemCreateDto.ToEntity();

            await _db.FoodItems.AddAsync(newFoodItem);
            await _db.SaveChangesAsync();

            return newFoodItem;
        }


        public async Task<bool> UpdateFoodItem(Guid id, FoodItemCreateDto foodItemCreateDto)
        {
            var function = nameof(UpdateFoodItem);
            _logger.LogTrace($"{function}({nameof(id)}, {nameof(foodItemCreateDto)}) is worked out");

            if (foodItemCreateDto == null)
                throw new NullReferenceException($"{nameof(foodItemCreateDto)} param is null");

            if (id == Guid.Empty)
                throw new NullReferenceException($"{nameof(id)} is empty");

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

        public async Task<bool> DeleteFoodItem(Guid id)
        {
            var function = nameof(DeleteFoodItem);
            _logger.LogTrace($"{function}({nameof(id)}) is worked out");

            if (id == Guid.Empty)
                throw new NullReferenceException($"{nameof(id)} param is empty");

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
