using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Models
{
    public class Category : EntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
