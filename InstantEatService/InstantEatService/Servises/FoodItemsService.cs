using InstantEatService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Servises
{
    public class FoodItemsService : IFoodItemsService
    {
        private readonly InstantEatDbContext _db;
        private readonly ILogger<FoodItemsService> _logger;

        public FoodItemsService(InstantEatDbContext db, ILogger<FoodItemsService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IEnumerable<FoodItem> FilterByPrice(double min, double max)
        {
            return _db.FoodItems.Where(f => f.Price > min && f.Price < max);
        }


    }
}
