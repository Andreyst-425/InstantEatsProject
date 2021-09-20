using InstantEatService.Dto;
using InstantEatService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpGet]
        public async Task<ActionResult<List<CartDto>>> Get()
        {
            var carts = await _carts.GetCarts();
            return Ok(carts);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> Get(Guid id)
        {
            var cart = await _carts.GetCart(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }


    }
}
