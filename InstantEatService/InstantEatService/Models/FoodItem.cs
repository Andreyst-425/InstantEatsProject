using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Models
{
    public class FoodItem : EntityBase
    {
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }

        public IEnumerable<Cart> Carts { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
