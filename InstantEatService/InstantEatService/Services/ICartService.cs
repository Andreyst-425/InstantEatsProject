using InstantEatService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public interface ICartService
    {
        /// <summary>
        /// Добавить корзину
        /// </summary>
        /// <param name="address"> Адрес доставки </param>
        /// <param name="clientId"> Идентификатор клиента </param>
        /// <param name="client"> Информация о клиенте </param>
        /// <returns></returns>
        Task<Cart> AddCart(DeliveryAddress address, int clientId, [FromBody] Client client);

        /// <summary>
        /// Удалить корзину
        /// </summary>
        /// <param name="id"> Идентификатор корзины </param>
        /// <returns></returns>
        Task<bool> DeleteCart(int id);

        /// <summary>
        /// Получить корзину по id
        /// </summary>
        /// <param name="id"> Идентификатор корзины </param>
        /// <returns></returns>
        Task<Cart> GetCart(int id);

        /// <summary>
        /// Получить список корзин
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Cart>> GetCarts();

        /// <summary>
        /// Изменить информацию о корзине
        /// </summary>
        /// <param name="cart"> Информация для обновления </param>
        /// <param name="id"> Идентификатор корзины </param>
        /// <returns></returns>
        Task<bool> UpdateCart(Cart cart);

        /// <summary>
        /// Получить все открытые корзины (действующие в данный момент)
        /// </summary>
        /// <returns></returns>
        Task<List<Cart>> GetCurrentCarts();
    }
}