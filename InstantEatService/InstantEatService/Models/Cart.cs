using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Models
{
    public class Cart : EntityBase
    {
        public int OrderNumber { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
        public string DeliveryAdress { get; set; }
        public bool IsCanceled { get; set; }

        public IEnumerable<FoodItem> FoodItems { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
    }
}
