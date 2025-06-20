using FoodDelivery.Models;
using Microsoft.EntityFrameworkCore;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly FoodDeliverySystemContext _context;

    public MenuItemRepository(FoodDeliverySystemContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MenuItem>> GetAllMenuItemsWithRestaurantsAsync()
    {
        return await _context.MenuItems.Include(m => m.Restaurant).ToListAsync();
    }

    public async Task<IEnumerable<MenuItem>> GetAllByRestaurantIdAsync(int restaurantId)
    {
        return await _context.MenuItems.Include(m => m.Restaurant).Where(m => m.RestaurantId == restaurantId).ToListAsync();
    }

    public async Task<MenuItem?> GetByIdAsync(int itemId)
    {
        return await _context.MenuItems.Include(m => m.Restaurant).FirstOrDefaultAsync(m => m.ItemId == itemId);
    }

    public async Task<MenuItem> AddAsync(MenuItem item)
    {
        _context.MenuItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task UpdateAsync(MenuItem item)
    {
        _context.MenuItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(MenuItem item)
    {
        _context.MenuItems.Remove(item);
        await _context.SaveChangesAsync();
    }

}

