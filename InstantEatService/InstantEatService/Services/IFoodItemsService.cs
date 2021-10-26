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
        /// <param name="foodItemCreateDto"> Данные для создания блюда </param>
        /// <returns></returns>
        Task<FoodItem> CreateFoodItem(FoodItemCreateDto foodItemCreateDto);

        /// <summary>
        /// Удалить блюдо
        /// </summary>
        /// <param name="id"> Идентификатор блюда </param>
        /// <returns></returns>
        Task<bool> DeleteFoodItem(int id);

        /// <summary>
        /// Фильтр по цене
        /// </summary>
        /// <param name="min"> Минимальная цена </param>
        /// <param name="max"> Максимальная цена </param>
        /// <returns></returns>
        Task<IEnumerable<FoodItem>> FilterByPrice(double min, double max);

        /// <summary>
        /// Получить список всех блюд
        /// </summary>
        /// <returns></returns>
        Task<List<FoodItem>> GetAll();

        /// <summary>
        /// Получить список всех бургеров
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<FoodItem>> GetAllBurgers();

        /// <summary>
        /// Получить список всех салатов
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<FoodItem>> GetAllSalades();

        /// <summary>
        /// Получить список всех супов
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<FoodItem>> GetAllSoups();

        /// <summary>
        /// Получить все блюда вместе с их категориями
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
        /// <param name="id"> Идентификатор блюда </param>
        /// <param name="foodItemCreateDto"> Информация для обновления </param>
        /// <returns></returns>
        Task<bool> UpdateFoodItem(int id, FoodItemCreateDto foodItemCreateDto);
    }
}