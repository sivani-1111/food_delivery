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
