using InstantEatService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.DtoModels
{
    public class FoodItemDto
    {
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }

        public FoodItemDto(FoodItem foodItem)
        {
            Name = foodItem.Name;
            Price = foodItem.Price;
            Description = foodItem.Description;
            PictureUrl = foodItem.PictureUrl;
        }
    }
}
