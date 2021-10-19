using InstantEatService.Dto;
using InstantEatService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public interface IFoodItemsService
    {
        Task<FoodItem> CreateFoodItem(FoodItemCreateDto foodItemCreateDto);
        Task<bool> DeleteFoodItem(int id);
        Task<IEnumerable<FoodItem>> FilterByPrice(double min, double max);
        Task<List<FoodItem>> GetAll();
        Task<IEnumerable<FoodItem>> GetAllBurgers();
        Task<IEnumerable<FoodItem>> GetAllSalades();
        Task<IEnumerable<FoodItem>> GetAllSoups();
        Task<List<FoodItem>> GetAllWithCategories();
        Task<FoodItem> GetFoodItemById(int id);
        Task<bool> UpdateFoodItem(int id, FoodItemCreateDto foodItemCreateDto);
    }
}