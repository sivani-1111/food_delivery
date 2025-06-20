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
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }

        [HttpPut("me")]
        public async Task<IActionResult> Update(UpdateRestaurantDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var updated = await _service.UpdateAsync(dto, email);
            return updated ? Ok() : Forbid();
        }
    }

}

