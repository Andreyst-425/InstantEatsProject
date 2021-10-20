using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Models
{
    public interface ICarts
    {
        /// <summary>
        /// Добавить карзину
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        Task<Cart> AddCart(Cart cart);

        /// <summary>
        /// Удалить карзину
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteCart(int id);

        /// <summary>
        /// Получить список карзин
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Cart>> GetAllCarts();

        /// <summary>
        /// Получить карзину по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Cart> GetCart(int id);

        /// <summary>
        /// Отменить карзину - ??? хз кароче
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RestoreCart(int id);

        /// <summary>
        /// Изменить карзину
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        Task<bool> UpdateCart(Cart cart);
    }
}