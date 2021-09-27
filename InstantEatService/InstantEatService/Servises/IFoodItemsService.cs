using InstantEatService.Models;
using System.Collections.Generic;

namespace InstantEatService.Servises
{
    public interface IFoodItemsService
    {
        IEnumerable<FoodItem> FilterByPrice(double min, double max);
    }
}