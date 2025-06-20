public interface IMenuItemService 
{ 
    Task<IEnumerable<MenuItemDto>> GetAllForCustomersAsync(); 
    Task<IEnumerable<MenuItemDto>> GetAllForRestaurantAsync(string email); 
    Task<MenuItemDto?> GetByIdAsync(int itemId); 
    Task<MenuItemDto> AddAsync(CreateMenuItemDto dto, string email); 
    Task<bool> UpdateAsync(int itemId, UpdateMenuItemDto dto, string email); 
    Task<bool> DeleteAsync(int itemId, string email); 
}

