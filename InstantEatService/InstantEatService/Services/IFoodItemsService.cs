using InstantEatService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public interface IFoodItemsService
    {
        Task<IEnumerable<FoodItem>> FilterByPrice(double min, double max);
        Task<IEnumerable<FoodItem>> GetAllSoups();
        Task<IEnumerable<FoodItem>> GetAllSalads();
        Task<IEnumerable<FoodItem>> GetAllBurgers();

    }
}