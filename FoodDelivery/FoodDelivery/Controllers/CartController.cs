using FoodDelivery.DTOs;
using FoodDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.Controllers
{
    [Authorize(Roles = "Customer")]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpGet("customer")]
        public async Task<IActionResult> GetCustomerCart()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var carts = await _service.GetByCustomerAsync(email);
            return Ok(carts);
        }

        [HttpGet("{cartId}/customer")]
        public async Task<IActionResult> GetByCartId(int cartId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var cartItem = await _service.GetCartByIdAsync(cartId, email);
            return cartItem == null ? Forbid() : Ok(cartItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCartDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var created = await _service.AddAsync(dto, email);
            return created == null ? NotFound() : Ok(created);
        }

        [HttpPut("{cartId}")]
        public async Task<IActionResult> Update(int cartId, UpdateCartDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var updated = await _service.UpdateAsync(cartId, dto, email);
            return updated ? Ok() : Forbid();
        }

        [HttpDelete("{cartId}")]
        public async Task<IActionResult> Delete(int cartId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var deleted = await _service.DeleteAsync(cartId, email);
            return deleted ? Ok() : Forbid();
        }
    }

}


