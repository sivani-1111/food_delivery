using FoodDelivery.Models;

public interface IMenuItemRepository 
{ 
    Task<IEnumerable<MenuItem>> GetAllMenuItemsWithRestaurantsAsync(); 
    Task<IEnumerable<MenuItem>> GetAllByRestaurantIdAsync(int restaurantId); 
    Task<MenuItem?> GetByIdAsync(int itemId); 
    Task<MenuItem> AddAsync(MenuItem item); 
    Task UpdateAsync(MenuItem item); 
    Task DeleteAsync(MenuItem item); 
}
