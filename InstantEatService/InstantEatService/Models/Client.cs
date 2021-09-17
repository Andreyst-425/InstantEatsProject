using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Models
{
    public class Client : EntityBase
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Role Role { get; set; }
        public PaymentType PaymentType { get; set; }
        public IEnumerable<Cart> Carts { get; set; } //поработать с коллекциями ????
    }
}
