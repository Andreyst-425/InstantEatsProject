
namespace InstantEatService.Models
{
    public class DeliveryAddress : EntityBase
    {
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int Floor { get; set; }
        public int FlatNumber { get; set; }
    }
}
