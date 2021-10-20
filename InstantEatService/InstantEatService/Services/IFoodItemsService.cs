using InstantEatService.Dto;
using InstantEatService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public interface IFoodItemsService
    {
        /// <summary>
        /// Создать блюдо
        /// </summary>
        /// <param name="foodItemCreateDto"></param>
        /// <returns></returns>
        Task<FoodItem> CreateFoodItem(FoodItemCreateDto foodItemCreateDto);

        /// <summary>
        /// Удалить блюдо
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteFoodItem(int id);

        /// <summary>
        /// Фильтр по цене
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        Task<IEnumerable<FoodItem>> FilterByPrice(double min, double max);

        /// <summary>
        /// Получить список всех блюд
        /// </summary>
        /// <returns></returns>
        Task<List<FoodItem>> GetAll();

        /// <summary>
        /// Получить список всех видов бургеров
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<FoodItem>> GetAllBurgers();

        /// <summary>
        /// Получить список всех видов салатов
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<FoodItem>> GetAllSalades();

        /// <summary>
        /// Получить список всех видов супов
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<FoodItem>> GetAllSoups();

        /// <summary>
        /// Получить все блюда одной категории
        /// </summary>
        /// <returns></returns>
        Task<List<FoodItem>> GetAllWithCategories();

        /// <summary>
        /// Получить блюдо по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<FoodItem> GetFoodItemById(int id);

        /// <summary>
        /// Обновить блюдо
        /// </summary>
        /// <param name="id"></param>
        /// <param name="foodItemCreateDto"></param>
        /// <returns></returns>
        Task<bool> UpdateFoodItem(int id, FoodItemCreateDto foodItemCreateDto);
    }
}