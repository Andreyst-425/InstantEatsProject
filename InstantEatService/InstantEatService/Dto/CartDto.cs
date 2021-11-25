using InstantEatService.Models;

namespace InstantEatService.Dto
{
    public class CartDto
    {
        /// <summary>
        /// Общая стоимость
        /// </summary>
        public double TotalPrice { get; set; }

        /// <summary>
        /// Количество позиций в корзине
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Адрес доставки
        /// </summary>
        public DeliveryAddress AddressForDelivery { get; set; }
        public CartDto(Cart cart)
        {
            TotalPrice = cart.TotalPrice;
            Quantity = cart.Quantity;
            AddressForDelivery = cart.AddressForDelivery;
        }


    }
}
