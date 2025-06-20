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


