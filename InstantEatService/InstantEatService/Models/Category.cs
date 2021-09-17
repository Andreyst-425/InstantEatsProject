using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Models
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public IEnumerable<FoodItem> FoodItems { get; set; }
    }
}
