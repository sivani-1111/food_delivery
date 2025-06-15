using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemService _service;

    public MenuItemController(IMenuItemService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllForCustomersAsync());
    }

    [HttpGet("{itemId}")]
    [Authorize(Roles = "RestaurantOwner")]
    public async Task<IActionResult> GetById(int itemId)
    {
        var item = await _service.GetByIdAsync(itemId);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    [Authorize(Roles = "RestaurantOwner")]
    public async Task<IActionResult> Create(CreateMenuItemDto dto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var created = await _service.AddAsync(dto, email);
        return CreatedAtAction(nameof(GetById), new { itemId = created.Name }, created);
    }

    [HttpPut("{itemId}")]
    [Authorize(Roles = "RestaurantOwner")]
    public async Task<IActionResult> Update(int itemId, UpdateMenuItemDto dto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await _service.UpdateAsync(itemId, dto, email);
        if (!result) return Forbid();
        return NoContent();
    }

    [HttpDelete("{itemId}")]
    [Authorize(Roles = "RestaurantOwner")]
    public async Task<IActionResult> Delete(int itemId)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await _service.DeleteAsync(itemId, email);
        if (!result) return Forbid();
        return NoContent();
    }

}
//using FoodDelivery.DTOs;
//using FoodDelivery.Services.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace FoodDelivery.Controllers
//{
//    //[Authorize(Roles = "RestaurantOwner")]
//    [ApiController]
//    [Route("api/[controller]")]
//    public class MenuItemController : ControllerBase
//    {
//        private readonly IMenuItemService _service;

//        public MenuItemController(IMenuItemService service)
//        {
//            _service = service;
//        }

//        [Authorize(Roles = "Customer")]
//        [HttpGet] 
//        public async Task<IActionResult> GetAll()
//        {
//            var email = User.FindFirstValue(ClaimTypes.Email);
//            return Ok(await _service.GetAllAsync(email));
//        }

//        [Authorize(Roles = "RestaurantOwner")]
//        [HttpGet("{itemId}")]
//        public async Task<IActionResult> GetById(int itemId)
//        {
//            var email = User.FindFirstValue(ClaimTypes.Email);
//            var item = await _service.GetByIdAsync(itemId, email);
//            return item == null ? Forbid() : Ok(item);
//        }

//        [Authorize(Roles = "RestaurantOwner")]
//        [HttpPost]
//        public async Task<IActionResult> Create(CreateMenuItemDto dto)
//        {
//            var email = User.FindFirstValue(ClaimTypes.Email);
//            var created = await _service.AddAsync(dto, email);
//            return created == null ? Forbid() : Ok(created);
//        }

//        [Authorize(Roles = "RestaurantOwner")]
//        [HttpPut("{itemId}")]
//        public async Task<IActionResult> Update(int itemId, CreateMenuItemDto dto)
//        {
//            var email = User.FindFirstValue(ClaimTypes.Email);
//            var updated = await _service.UpdateAsync(itemId, dto, email);
//            return updated ? Ok() : Forbid();
//        }

//        [Authorize(Roles = "RestaurantOwner")]
//        [HttpDelete("{itemId}")]
//        public async Task<IActionResult> Delete(int itemId)
//        {
//            var email = User.FindFirstValue(ClaimTypes.Email);
//            var deleted = await _service.DeleteAsync(itemId, email);
//            return deleted ? Ok() : Forbid();
//        }
//    }

//}


//using FoodDelivery.DTOs;
//using FoodDelivery.Services.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace FoodDelivery.Controllers
//{
//    [Authorize]
//    [ApiController]
//    [Route("api/[controller]")]
//    public class MenuItemController : ControllerBase
//    {
//        private readonly IMenuItemService _service;

//        public MenuItemController(IMenuItemService service)
//        {
//            _service = service;
//        }

//        [HttpGet]
//        [AllowAnonymous]
//        public async Task<IActionResult> GetAll()
//        {
//            return Ok(await _service.GetAllAsync());
//        }

//        [HttpGet("{itemId}/{restaurantId}")]
//        [AllowAnonymous]
//        public async Task<IActionResult> Get(int itemId, int restaurantId)
//        {
//            var item = await _service.GetByIdAsync(itemId, restaurantId);
//            if (item == null) return NotFound();
//            return Ok(item);
//        }

//        [HttpPost]
//        [Authorize(Roles = "RestaurantOwner")]
//        public async Task<IActionResult> Create(CreateMenuItemDto dto)
//        {
//            var created = await _service.AddAsync(dto);
//            return CreatedAtAction(nameof(Get), new { itemId = created.ItemID, restaurantId = dto.RestaurantID }, created);
//        }

//        [HttpPut("{itemId}")]
//        [Authorize(Roles = "RestaurantOwner")]
//        public async Task<IActionResult> Update(int itemId, CreateMenuItemDto dto)
//        {
//            var success = await _service.UpdateAsync(itemId, dto);
//            if (!success) return NotFound();
//            return Ok();
//        }

//        [HttpDelete("{itemId}")]
//        [Authorize(Roles = "RestaurantOwner")]
//        public async Task<IActionResult> Delete(int itemId)
//        {
//            var success = await _service.DeleteAsync(itemId);
//            if (!success) return NotFound();
//            return Ok();
//        }
//    }

//}
//using FoodDelivery.DTOs;
//using FoodDelivery.Services.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace FoodDelivery.Controllers
//{
//    [Authorize]
//    [ApiController]
//    [Route("api/[controller]")]
//    public class MenuItemController : ControllerBase
//    {
//        private readonly IMenuItemService _service;

//        public MenuItemController(IMenuItemService service)
//        {
//            _service = service;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            return Ok(await _service.GetAllAsync());
//        }

//        [HttpGet("{id}")]
//        [AllowAnonymous]
//        public async Task<IActionResult> Get(int id)
//        {
//            var item = await _service.GetByIdAsync(id);
//            if (item == null) return NotFound();
//            return Ok(item);
//        }

//        [HttpPost]
//        [Authorize(Roles = "RestaurantOwner")]
//        public async Task<IActionResult> Create(CreateMenuItemDto dto)
//        {
//            var created = await _service.AddAsync(dto);
//            return CreatedAtAction(nameof(Get), new { id = created.ItemID }, created);
//        }

//        [HttpPut("{id}")]
//        [Authorize(Roles = "RestaurantOwner")]
//        public async Task<IActionResult> Update(int id, CreateMenuItemDto dto)
//        {
//            await _service.UpdateAsync(id, dto);
//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        [Authorize(Roles = "RestaurantOwner")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            await _service.DeleteAsync(id);
//            return NoContent();
//        }
//    }
//}