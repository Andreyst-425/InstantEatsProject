using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public interface IJsonDataService
    {
        void Dispose();
        Task<bool> PostCategories();
        Task<bool> PostFoodItems();
    }
}