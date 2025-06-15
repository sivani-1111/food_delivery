public interface IMenuItemService { Task<IEnumerable<MenuItemDto>> GetAllForCustomersAsync(); Task<IEnumerable<MenuItemDto>> GetAllForRestaurantAsync(string email); Task<MenuItemDto?> GetByIdAsync(int itemId); Task<MenuItemDto> AddAsync(CreateMenuItemDto dto, string email); Task<bool> UpdateAsync(int itemId, UpdateMenuItemDto dto, string email); Task<bool> DeleteAsync(int itemId, string email); }

//using FoodDelivery.DTOs;

//namespace FoodDelivery.Services.Interfaces { public interface IMenuItemService { Task<IEnumerable<MenuItemDto>> GetAllAsync(string email); Task<MenuItemDto?> GetByIdAsync(int itemId, string email); Task<MenuItemDto?> AddAsync(CreateMenuItemDto dto, string email); Task<bool> UpdateAsync(int itemId, CreateMenuItemDto dto, string email); Task<bool> DeleteAsync(int itemId, string email); } }

//using FoodDelivery.DTOs;

//namespace FoodDelivery.Services.Interfaces
//{
//    public interface IMenuItemService
//    {
//        Task<IEnumerable<MenuItemDto>> GetAllAsync();
//        Task<MenuItemDto?> GetByItemAndRestaurantAsync(int itemId, int restaurantId); // 🔄 Replaced GetByIdAsync
//        Task<MenuItemDto> AddAsync(CreateMenuItemDto dto);
//        Task UpdateAsync(int id, CreateMenuItemDto dto);
//        Task DeleteAsync(int id);
//    }
//}



//using FoodDelivery.DTOs;

//namespace FoodDelivery.Services.Interfaces
//{
//    public interface IMenuItemService
//    {
//        Task<IEnumerable<MenuItemDto>> GetAllAsync();
//        Task<MenuItemDto?> GetByIdAsync(int id);
//        Task<MenuItemDto> AddAsync(CreateMenuItemDto dto);
//        Task UpdateAsync(int id, CreateMenuItemDto dto);
//        Task DeleteAsync(int id);
//    }
//}