using InstantEatService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Dto
{
    public class CategoryDto
    {
        public string Name;

        public CategoryDto(Category category)
        {
            Name =  category.Name;
        }
    }
}
