using InstantEatService.DtoModels;
using InstantEatService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Repositories
{
    public interface IFoodItemsRepository
    {   
        /// <summary>
        /// Создать блюдо
        /// </summary>
        /// <param name="foodItemCreateDto"></param>
        /// <returns></returns>
        Task<FoodItem> CreateFoodItem(FoodItemCreateDto foodItemCreateDto);

        /// <summary>
        /// Удалить блюдо по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteFoodItem(Guid id);

       /// <summary>
       /// Очистка ресурсов
       /// </summary>
        void Dispose();

        /// <summary>
        /// Получить список всех блюд
        /// </summary>
        IEnumerable<FoodItem> GetAllFoodItems();

        /// <summary>
        /// Получить блюдо по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<FoodItem> GetFoodItem(Guid id);

        /// <summary>
        /// Обновить блюдо по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="foodItemCreateDto"></param>
        /// <returns></returns>
        Task<bool> UpdateFoodItem(Guid id, FoodItemCreateDto foodItemCreateDto);
    }
}