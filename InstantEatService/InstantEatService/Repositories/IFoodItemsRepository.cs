using InstantEatService.Dto;
using InstantEatService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Repositories
{
    public interface IFoodItemsRepository
    {   
        /// <summary>
        /// Создать блюдо
        /// </summary>
        /// <param name="foodItemCreateDto"> Данные для создания </param>
        /// <returns></returns>
        Task<FoodItem> CreateFoodItem(FoodItemCreateDto foodItemCreateDto);

        /// <summary>
        /// Удалить блюдо по id
        /// </summary>
        /// <param name="id"> Идентификатор блюда </param>
        /// <returns></returns>
        Task<bool> DeleteFoodItem(int id);

       /// <summary>
       /// Очистка ресурсов
       /// </summary>
        void Dispose();

        /// <summary>
        /// Получить список всех блюд
        /// </summary>
        Task<IEnumerable<FoodItem>> GetAllFoodItems();

        /// <summary>
        /// Получить список всех блюд вместе с категориями
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<FoodItem>> GetAllFoodItemsWithCategories();

        /// <summary>
        /// Получить блюдо по id
        /// </summary>
        /// <param name="id"> Идентификатор блюда </param>
        /// <returns></returns>
        Task<FoodItem> GetFoodItem(int id);

        /// <summary>
        /// Обновить блюдо по id
        /// </summary>
        /// <param name="id"> Идентификатор блюда </param>
        /// <param name="foodItemCreateDto"> Данные для обновления </param>
        /// <returns></returns>
        Task<bool> UpdateFoodItem(int id, FoodItemCreateDto foodItemCreateDto);
    }
}