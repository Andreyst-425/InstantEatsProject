using InstantEatService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public interface IBusinessLunch
    {
        Task<List<FoodItem>> GetBusinessLunchFirst();
        Task<List<FoodItem>> GetBusinessLunchSecond();
        Task<List<FoodItem>> GetBusinessLunchThird();
    }
}