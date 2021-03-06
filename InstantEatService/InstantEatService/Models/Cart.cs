using System.Collections.Generic;

namespace InstantEatService.Models
{
    public class Cart : EntityBase
    {
        public int OrderNumber { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
        public DeliveryAddress AddressForDelivery { get; set; }
        public bool IsCanceled { get; set; }

        public IEnumerable<FoodItem> FoodItems { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
