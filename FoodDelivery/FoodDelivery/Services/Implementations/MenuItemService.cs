using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FoodDelivery.Models;
using System.Security.Claims;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _repository; 
    private readonly FoodDeliverySystemContext _context; 
    private readonly IMapper _mapper; 
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MenuItemService(
        IMenuItemRepository repository,
        FoodDeliverySystemContext context,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<MenuItemDto>> GetAllForCustomersAsync()
    {
        var items = await _repository.GetAllMenuItemsWithRestaurantsAsync();
        return _mapper.Map<IEnumerable<MenuItemDto>>(items);
    }

    public async Task<IEnumerable<MenuItemDto>> GetAllForRestaurantAsync(string email)
    {
        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.ContactEmail == email);
        if (restaurant == null) return new List<MenuItemDto>();

        var items = await _repository.GetAllByRestaurantIdAsync(restaurant.RestaurantId);
        return _mapper.Map<IEnumerable<MenuItemDto>>(items);
    }

    public async Task<MenuItemDto?> GetByIdAsync(int itemId)
    {
        var item = await _repository.GetByIdAsync(itemId);
        if (item == null) return null;

        var httpContext = _httpContextAccessor.HttpContext;
        var role = httpContext?.User.FindFirstValue(ClaimTypes.Role);
        var email = httpContext?.User.FindFirstValue(ClaimTypes.Email);

        if (role == "RestaurantOwner")
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.ContactEmail == email);
            if (restaurant == null || item.RestaurantId != restaurant.RestaurantId)
                return null;
        }

        return _mapper.Map<MenuItemDto>(item);
    }

    public async Task<MenuItemDto> AddAsync(CreateMenuItemDto dto, string email)
    {
        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.ContactEmail == email);
        if (restaurant == null) throw new Exception("Unauthorized");

        var item = _mapper.Map<MenuItem>(dto);
        item.RestaurantId = restaurant.RestaurantId;
        var created = await _repository.AddAsync(item);
        return _mapper.Map<MenuItemDto>(created);
    }

    public async Task<bool> UpdateAsync(int itemId, UpdateMenuItemDto dto, string email)
    {
        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.ContactEmail == email);
        if (restaurant == null) return false;

        var existing = await _repository.GetByIdAsync(itemId);
        if (existing == null || existing.RestaurantId != restaurant.RestaurantId) return false;

        _mapper.Map(dto, existing);
        await _repository.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> DeleteAsync(int itemId, string email)
    {
        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.ContactEmail == email);
        if (restaurant == null) return false;

        var existing = await _repository.GetByIdAsync(itemId);
        if (existing == null || existing.RestaurantId != restaurant.RestaurantId) return false;

        await _repository.DeleteAsync(existing);
        return true;
    }

}



