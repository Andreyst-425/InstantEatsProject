using System.Collections.Generic;

namespace InstantEatService.Models
{
    public class Client : EntityBase
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public IEnumerable<Cart> Carts { get; set; }
    }
}
