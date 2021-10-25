using InstantEatService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Dto
{
    public class FoodItemDto
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ссылка на картинку
        /// </summary>
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
