using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FoodDelivery.Models;
using System.Security.Claims;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _repository; private readonly FoodDeliverySystemContext _context; private readonly IMapper _mapper; private readonly IHttpContextAccessor _httpContextAccessor;

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



//using AutoMapper;
//using FoodDelivery.DTOs;
//using FoodDelivery.Models;
//using FoodDelivery.Repositories.Interfaces;
//using FoodDelivery.Services.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace FoodDelivery.Services.Implementations
//{
//    public class MenuItemService : IMenuItemService
//    {
//        private readonly IMenuItemRepository _repository; private readonly FoodDeliverySystemContext _context; private readonly IMapper _mapper;

//        public MenuItemService(IMenuItemRepository repository, FoodDeliverySystemContext context, IMapper mapper)
//        {
//            _repository = repository;
//            _context = context;
//            _mapper = mapper;
//        }

//        private async Task<Restaurant?> GetRestaurantAsync(string email)
//        {
//            return await _context.Restaurants.FirstOrDefaultAsync(r => r.ContactEmail == email);
//        }

//        public async Task<IEnumerable<MenuItemDto>> GetAllAsync(string email)
//        {
//            var restaurant = await GetRestaurantAsync(email);
//            if (restaurant == null) return Enumerable.Empty<MenuItemDto>();
//            var items = await _repository.GetAllAsync(restaurant.RestaurantId);
//            return _mapper.Map<IEnumerable<MenuItemDto>>(items);
//        }

//        public async Task<MenuItemDto?> GetByIdAsync(int itemId, string email)
//        {
//            var restaurant = await GetRestaurantAsync(email);
//            if (restaurant == null) return null;
//            var item = await _repository.GetByIdAsync(itemId, restaurant.RestaurantId);
//            return item == null ? null : _mapper.Map<MenuItemDto>(item);
//        }

//        public async Task<MenuItemDto?> AddAsync(CreateMenuItemDto dto, string email)
//        {
//            var restaurant = await GetRestaurantAsync(email);
//            if (restaurant == null) return null;

//            var entity = _mapper.Map<MenuItem>(dto);
//            entity.RestaurantId = restaurant.RestaurantId;
//            var created = await _repository.AddAsync(entity);
//            return _mapper.Map<MenuItemDto>(created);
//        }

//        public async Task<bool> UpdateAsync(int itemId, CreateMenuItemDto dto, string email)
//        {
//            var restaurant = await GetRestaurantAsync(email);
//            if (restaurant == null) return false;

//            var existing = await _repository.GetByIdAsync(itemId, restaurant.RestaurantId);
//            if (existing == null) return false;

//            _mapper.Map(dto, existing);
//            await _repository.UpdateAsync(existing);
//            return true;
//        }

//        public async Task<bool> DeleteAsync(int itemId, string email)
//        {
//            var restaurant = await GetRestaurantAsync(email);
//            if (restaurant == null) return false;

//            var item = await _repository.GetByIdAsync(itemId, restaurant.RestaurantId);
//            if (item == null) return false;

//            await _repository.DeleteAsync(item);
//            return true;
//        }
//    }

//}


//using AutoMapper;
//using FoodDelivery.DTOs;
//using FoodDelivery.Models;
//using FoodDelivery.Repositories.Interfaces;
//using FoodDelivery.Services.Interfaces;

//namespace FoodDelivery.Services.Implementations
//{
//    public class MenuItemService : IMenuItemService
//    {
//        private readonly IMenuItemRepository _repository; private readonly IMapper _mapper;

//        public MenuItemService(IMenuItemRepository repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        public async Task<IEnumerable<MenuItemDto>> GetAllAsync()
//        {
//            var items = await _repository.GetAllAsync();
//            return _mapper.Map<IEnumerable<MenuItemDto>>(items);
//        }

//        public async Task<MenuItemDto?> GetByIdAsync(int itemId, int restaurantId)
//        {
//            var item = await _repository.GetByIdAsync(itemId, restaurantId);
//            return item == null ? null : _mapper.Map<MenuItemDto>(item);
//        }

//        public async Task<MenuItemDto> AddAsync(CreateMenuItemDto dto)
//        {
//            var item = _mapper.Map<MenuItem>(dto);
//            var created = await _repository.AddAsync(item);
//            return _mapper.Map<MenuItemDto>(created);
//        }

//        public async Task<bool> UpdateAsync(int itemId, CreateMenuItemDto dto)
//        {
//            var existing = await _repository.GetByIdAsync(itemId, dto.RestaurantID);
//            if (existing == null) return false;
//            existing.Name = dto.Name;
//            existing.Description = dto.Description;
//            existing.Price = dto.Price;
//            await _repository.UpdateAsync(existing);
//            return true;
//        }

//        public async Task<bool> DeleteAsync(int itemId)
//        {
//            var existing = await _repository.GetAllAsync();
//            var item = existing.FirstOrDefault(i => i.ItemId == itemId);
//            if (item == null) return false;
//            await _repository.DeleteAsync(item);
//            return true;
//        }
//    }

//}

//using AutoMapper;
//using FoodDelivery.DTOs;
//using FoodDelivery.Models;
//using FoodDelivery.Services.Interfaces;
//using FoodDelivery.Repositories.Interfaces;

//namespace FoodDelivery.Services.Implementations
//{
//    public class MenuItemService : IMenuItemService
//    {
//        private readonly IMenuItemRepository _repository;
//        private readonly IMapper _mapper;

//        public MenuItemService(IMenuItemRepository repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        public async Task<IEnumerable<MenuItemDto>> GetAllAsync()
//        {
//            var items = await _repository.GetAllAsync();
//            return _mapper.Map<IEnumerable<MenuItemDto>>(items);
//        }

//        public async Task<MenuItemDto?> GetByIdAsync(int id)
//        {
//            var item = await _repository.GetByIdAsync(id);
//            return _mapper.Map<MenuItemDto?>(item);
//        }

//        public async Task<MenuItemDto> AddAsync(CreateMenuItemDto dto)
//        {
//            var entity = _mapper.Map<MenuItem>(dto);
//            var created = await _repository.AddAsync(entity);
//            return _mapper.Map<MenuItemDto>(created);
//        }

//        public async Task UpdateAsync(int id, CreateMenuItemDto dto)
//        {
//            var existing = await _repository.GetByIdAsync(id);
//            if (existing == null) return;
//            _mapper.Map(dto, existing);
//            await _repository.UpdateAsync(existing);
//        }

//        public async Task DeleteAsync(int id)
//        {
//            await _repository.DeleteAsync(id);
//        }
//    }
//}