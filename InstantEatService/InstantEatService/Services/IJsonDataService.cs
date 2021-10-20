using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public interface IJsonDataService
    {
        /// <summary>
        /// Очистить данные 
        /// </summary>
        void Dispose();

        /// <summary>
        /// Полодить список категорий в БД
        /// </summary>
        /// <returns></returns>
        Task<bool> PostCategories();

        /// <summary>
        /// Положить список блюд в БД
        /// </summary>
        /// <returns></returns>
        Task<bool> PostFoodItems();
    }
}