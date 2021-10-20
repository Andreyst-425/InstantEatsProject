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
        /// Добавить карзину
        /// </summary>
        /// <param name="address"></param>
        /// <param name="clientId"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        Task<Cart> AddCart(DeliveryAddress address, int clientId, [FromBody] Client client);

        /// <summary>
        /// Удалить карзину
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteCart(int id);

        /// <summary>
        /// Получить карзину по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Cart> GetCart(int id);

        /// <summary>
        /// Получить список карзин
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Cart>> GetCarts();

        /// <summary>
        /// Изменить информацию о карзине
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        Task<bool> UpdateCart(Cart cart);

        /// <summary>
        /// Получить текущие карзины - ??? тоже не очень понятно 
        /// </summary>
        /// <returns></returns>
        Task<List<Cart>> GetCurrentCarts();

    }
}