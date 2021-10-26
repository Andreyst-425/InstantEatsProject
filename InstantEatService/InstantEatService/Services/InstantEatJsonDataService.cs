using InstantEatService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public class InstantEatJsonDataService : IDisposable, IJsonDataService
    {
        private readonly ILogger<InstantEatJsonDataService> _logger;
        private readonly IConfiguration _config;
        private readonly InstantEatDbContext _db;
        public InstantEatJsonDataService(InstantEatDbContext db, IConfiguration config, ILogger<InstantEatJsonDataService> logger)
        {
            _logger = logger;
            _config = config;
            _db = db;
        }

        public async Task<bool> PostFoodItems()
        {
            var function = nameof(PostFoodItems);
            _logger.LogTrace($"{function}() is worked out");

            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config["Tables:foodItems"]);
                var jsonString = File.ReadAllText(path);
                var foodItems = JsonConvert.DeserializeObject<List<FoodItem>>(jsonString);
                foreach (var foodItem in foodItems)
                {
                    await _db.FoodItems.AddAsync(foodItem);
                }
                await _db.SaveChangesAsync();
                File.Delete(path);
                _logger.LogInformation("Data have been added successfully");
                return true;
            }
            catch
            {
                _logger.LogWarning($"The exeption was captured in {function}()." +
                    $" Check correctness of configuration or JASON data.");
                return false;
            }

        }

        public async Task<bool> PostCategories()
        {
            var function = nameof(PostCategories);
            _logger.LogTrace($"{function}() is worked out");

            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config["Tables:categories"]);
                var jsonString = File.ReadAllText(path);
                var categories = JsonConvert.DeserializeObject<List<Category>>(jsonString);
                foreach (var foodItem in categories)
                {
                    await _db.Categories.AddAsync(foodItem);
                }
                await _db.SaveChangesAsync();
                File.Delete(path);
                return true;
            }
            catch
            {
                _logger.LogWarning($"The exeption was captured in {function}()." +
                    $" Check correctness of configuration or JASON data.");
                return false;
            }

        }


        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
