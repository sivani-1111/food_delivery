using FoodDelivery.DTOs;
using FoodDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.Controllers
{
    [Authorize(Roles = "RestaurantOwner")]
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _service;

        public DeliveryController(IDeliveryService service)
        {
            _service = service;
        }

        [HttpGet("{deliveryId}")]
        public async Task<IActionResult> Get(int deliveryId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var delivery = await _service.GetByIdAsync(deliveryId, email);
            if (delivery == null) return Forbid();
            return Ok(delivery);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDeliveryDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var created = await _service.AddAsync(dto, email);
            return CreatedAtAction(nameof(Get), new { deliveryId = created.DeliveryID }, created);
        }

        [HttpPut("{deliveryId}")]
        public async Task<IActionResult> Update(int deliveryId, UpdateDeliveryDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var updated = await _service.UpdateAsync(deliveryId, dto, email);
            if (!updated) return Forbid();
            return Ok();
        }
    }

}


//using FoodDelivery.DTOs;
//using FoodDelivery.Services.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace FoodDelivery.Controllers
//{
//    [Authorize(Roles = "RestaurantOwner")]
//    [ApiController]
//    [Route("api/[controller]")]
//    public class DeliveryController : ControllerBase
//    {
//        private readonly IDeliveryService _service;

//        public DeliveryController(IDeliveryService service)
//        {
//            _service = service;
//        }

//        [HttpGet("{restaurantId}/{deliveryId}")]
//        public async Task<IActionResult> Get(int restaurantId, int deliveryId)
//        {
//            var delivery = await _service.GetByIdAsync(deliveryId, restaurantId);
//            if (delivery == null) return NotFound();
//            return Ok(delivery);
//        }

//        [HttpPost("{restaurantId}")]
//        public async Task<IActionResult> Create(int restaurantId, CreateDeliveryDto dto)
//        {
//            var created = await _service.AddAsync(dto, restaurantId);
//            return CreatedAtAction(nameof(Get), new { deliveryId = created.DeliveryID, restaurantId = restaurantId }, created);
//        }

//        [HttpPut("{restaurantId}/{deliveryId}")]
//        public async Task<IActionResult> Update(int restaurantId, int deliveryId, UpdateDeliveryDto dto)
//        {
//            var success = await _service.UpdateAsync(deliveryId, restaurantId, dto);
//            if (!success) return Forbid();
//            return Ok();
//        }
//    }

//}


//using FoodDelivery.DTOs;
//using Microsoft.AspNetCore.Mvc;
//using FoodDelivery.Services.Interfaces;
//using Microsoft.AspNetCore.Authorization;

//namespace FoodDelivery.Controllers
//{
//    [Authorize(Roles = "RestaurantOwner")]
//    [ApiController]
//    [Route("api/[controller]")]
//    public class DeliveryController : ControllerBase
//    {
//        private readonly IDeliveryService _service;

//        public DeliveryController(IDeliveryService service)
//        {
//            _service = service;
//        }


//        [HttpGet("{id}")]
//        public async Task<IActionResult> Get(int id)
//        {
//            var delivery = await _service.GetByIdAsync(id);
//            if (delivery == null) return NotFound();
//            return Ok(delivery);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(CreateDeliveryDto dto)
//        {
//            var created = await _service.AddAsync(dto);
//            return CreatedAtAction(nameof(Get), new { id = created.DeliveryID }, created);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update(int id, CreateDeliveryDto dto)
//        {
//            await _service.UpdateAsync(id, dto);
//            return NoContent();
//        }

//    }
//}