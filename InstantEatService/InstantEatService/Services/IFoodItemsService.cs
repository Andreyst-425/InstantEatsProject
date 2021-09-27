using InstantEatService.Models;
using System.Collections.Generic;

namespace InstantEatService.Services
{
    public interface IFoodItemsService
    {
        IEnumerable<FoodItem> FilterByPrice(double min, double max);
    }
}