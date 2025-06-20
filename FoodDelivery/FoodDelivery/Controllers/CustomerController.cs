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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpPut("me")]
        public async Task<IActionResult> Update(UpdateCustomerDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var updated = await _service.UpdateAsync(dto, email);
            return updated ? Ok() : Forbid();
        }
    }

}
