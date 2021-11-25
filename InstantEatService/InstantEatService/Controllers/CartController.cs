﻿using InstantEatService.Dto;
using InstantEatService.Models;
using InstantEatService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly InstantEatDbContext _db;
        private readonly ICartService _carts;

        public CartController(InstantEatDbContext db, ICartService carts)
        {
            _db = db;
            _carts = carts;
        }

        /// <summary>
        /// Получение всех объектов Cart
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<CartDto>> Get()
        {
            var cartsIEnumerable = await _carts.GetCarts();
            var carts = cartsIEnumerable.ToList();
            var cartsCreateDto = new List<CartDto>();
            foreach (var cart in carts)
            {
                cartsCreateDto.Add(new CartDto(cart));
            }
            return cartsCreateDto;
        }


        /// <summary>
        /// Получение корзины по идентификатору
        /// </summary>
        /// <param name="id"> идентификатор </param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<CartDto> Get(int id)
        {
            var cart = await _carts.GetCart(id);
            if (cart == null) return null;
            return new CartDto(cart);
        }

        /// <summary>
        /// Добавить новую корзину 
        /// </summary>
        /// <param name="cart"> Данные для добавления </param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> Add([FromBody] Cart cart)
        {
            var isAdded = await _carts.AddCart(cart.AddressForDelivery, cart.ClientId, cart.Client);

            return isAdded;
        }

        /// <summary>
        /// Обновить содержимое корзины
        /// </summary>
        /// <param name="cart"> Данные для обновления </param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<bool> Update([FromBody] Cart cart)
        {
            var updatedCart = await _carts.UpdateCart(cart);
            return updatedCart;
        }

        /// <summary>
        /// Удалить карзину по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<bool> Delete(int id)
        {
            var isDeleted = await _carts.DeleteCart(id);
            return isDeleted;
        }
    }
}
