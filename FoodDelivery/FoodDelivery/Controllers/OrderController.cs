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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return Unauthorized();

            var created = await _service.AddOrderFromCartAsync(email);
            return CreatedAtAction(nameof(Get), new { orderId = created.OrderID }, created);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(int orderId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return Unauthorized();

            var order = await _service.GetByIdAsync(orderId, email);
            if (order == null) return Forbid();

            return Ok(order);
        }
    }

}
