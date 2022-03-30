using DiscountStore.Services;
using DiscountStore.WebAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DiscountStore.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
        }

        [HttpGet("total")]
        public IActionResult GetCartTotal([FromQuery] Guid cartId)
        {
            var result = _cartService.GetTotal(cartId);
            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult AddCartProduct([FromBody] AddCartProductRequest request)
        {
            _cartService.Add(request.CartGuid, request.Product);
            return Ok();
        }

        [HttpPost("deleteProduct")]
        public IActionResult DeleteCartProduct([FromBody] DeleteProductRequest request)
        {
            _cartService.RemoveProduct(request.CartGuid, request.Product);
            return Ok();
        }

        [HttpPost("deleteCart")]
        public IActionResult DeleteCart([FromBody] Guid cartId)
        {
            _cartService.RemoveCart(cartId);
            return Ok();
        }
    }
}
