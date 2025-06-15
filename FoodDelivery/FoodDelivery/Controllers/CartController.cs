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


//using FoodDelivery.DTOs;
//using FoodDelivery.Services.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace FoodDelivery.Controllers
//{
//    [Authorize(Roles = "Customer")]
//    [ApiController]
//    [Route("api/[controller]")]
//    public class CartController : ControllerBase
//    {
//        private readonly ICartService _service;

//        public CartController(ICartService service)
//        {
//            _service = service;
//        }

//        [HttpGet("{customerId}")]
//        public async Task<IActionResult> GetByCustomerId(int customerId)
//        {
//            return Ok(await _service.GetByCustomerIdAsync(customerId));
//        }

//        [HttpGet("{cartId}/{customerId}")]
//        public async Task<IActionResult> GetById(int cartId, int customerId)
//        {
//            var cart = await _service.GetByIdAsync(cartId, customerId);
//            if (cart == null) return NotFound();
//            return Ok(cart);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(CreateCartDto dto)
//        {
//            var created = await _service.AddAsync(dto);
//            return CreatedAtAction(nameof(GetByCustomerId), new { customerId = dto.CustomerID }, created);
//        }

//        [HttpPut("{cartId}/{customerId}")]
//        public async Task<IActionResult> Update(int cartId, int customerId, UpdateCartDto dto)
//        {
//            var updated = await _service.UpdateAsync(cartId, customerId, dto);
//            if (updated == null) return Forbid();
//            return Ok("updated successfully");
//        }

//        [HttpDelete("{cartId}/{customerId}")]
//        public async Task<IActionResult> Delete(int cartId, int customerId)
//        {
//            var deleted = await _service.DeleteAsync(cartId, customerId);
//            if (!deleted) return Forbid();
//            return Ok(deleted);
//        }
//    }

//}



//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
//using FoodDelivery.DTOs;
//using FoodDelivery.Services.Interfaces;

//namespace FoodDelivery.Controllers
//{
//    [Authorize(Roles = "Customer")]
//    [ApiController]
//    [Route("api/[controller]")]
//    public class CartController : ControllerBase
//    {
//        private readonly ICartService _service;

//        public CartController(ICartService service)
//        {
//            _service = service;
//        }

//        [HttpGet("customer/{customerId}")]
//        public async Task<IActionResult> GetByCustomerId(int customerId)
//        {
//            return Ok(await _service.GetByCustomerIdAsync(customerId));
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(CreateCartDto dto)
//        {
//            var created = await _service.AddAsync(dto);
//            return CreatedAtAction(nameof(GetByCustomerId), new { customerId = created.CustomerID }, created);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update(int id, CreateCartDto dto)
//        {
//            await _service.UpdateAsync(id, dto);
//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            await _service.DeleteAsync(id);
//            return NoContent();
//        }
//    }
//}





