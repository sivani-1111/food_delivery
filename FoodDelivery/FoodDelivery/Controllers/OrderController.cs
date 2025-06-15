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

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(int orderId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _service.GetByIdAsync(orderId, email);
            if (order == null) return Forbid();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var created = await _service.AddAsync(dto, email);
            return CreatedAtAction(nameof(Get), new { orderId = created.OrderID }, created);
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
//    public class OrderController : ControllerBase
//    {
//        private readonly IOrderService _service;

//        public OrderController(IOrderService service)
//        {
//            _service = service;
//        }

//        [HttpGet("{orderId}/{customerId}")]
//        public async Task<IActionResult> Get(int orderId, int customerId)
//        {
//            var order = await _service.GetByIdAsync(orderId, customerId);
//            if (order == null) return Forbid();
//            return Ok(order);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(CreateOrderDto dto)
//        {
//            var created = await _service.AddAsync(dto);
//            return CreatedAtAction(nameof(Get), new { orderId = created.OrderID, customerId = dto.CustomerID }, created);
//        }
//    }
//}