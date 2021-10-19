using System.Collections.Generic;

namespace InstantEatService.Models
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public IEnumerable<FoodItem> FoodItems { get; set; }
    }
}
