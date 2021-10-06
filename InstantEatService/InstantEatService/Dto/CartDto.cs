using InstantEatService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Dto
{
    public class CartDto
    {
        public CartDto(Cart cart)
        {
            TotalPrice = cart.TotalPrice;
            Quantity = cart.Quantity;
            AddressForDelivery = cart.AddressForDelivery;
        }

        public double TotalPrice { get; set; }

        public int Quantity { get; set; }

        [MinLength(10)]
        public DeliveryAddress AddressForDelivery { get; set; }

    }
}
