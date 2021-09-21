using InstantEatService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.DtoModels
{
    public class FoodItemCreateDto
    {
        /// <summary>
        /// Имя
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

        public FoodItem ToEntity()
        {
            return new FoodItem()
            {
                Name = Name,
                Price = Price,
                Description = Description,
                PictureUrl = PictureUrl
            };
        }
    }
}
