using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Models
{
    public interface ICartsRepository
    {
        /// <summary>
        /// Добавить корзину
        /// </summary>
        /// <param name="cart"> Данные для добавления корзины </param>
        /// <returns></returns>
        Task<Cart> AddCart(Cart cart);

        /// <summary>
        /// Удалить корзину
        /// </summary>
        /// <param name="id"> Идентификатор корзины </param>
        /// <returns></returns>
        Task<bool> DeleteCart(int id);

        /// <summary>
        /// Получить все корзины
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Cart>> GetAllCarts();

        /// <summary>
        /// Получить корзину по id
        /// </summary>
        /// <param name="id"> Идентификатор корзины </param>
        /// <returns></returns>
        Task<Cart> GetCart(int id);

        /// <summary>
        /// Восстановить корзину (по логике названия)
        /// </summary>
        /// <param name="id"> Идентификатор корзины </param>
        /// <returns></returns>
        Task<bool> RestoreCart(int id);

        /// <summary>
        /// Изменить корзину
        /// </summary>
        /// <param name="cart"> Данные для изменения корзины </param>
        /// <returns></returns>
        Task<bool> UpdateCart(Cart cart,int id);
    }
}