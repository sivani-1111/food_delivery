using FoodDelivery.Models;
using FoodDelivery.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repositories.Implementations
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly FoodDeliverySystemContext _context;

        public RestaurantRepository(FoodDeliverySystemContext context)
        {
            _context = context;
        }

        public async Task<Restaurant?> GetByEmailAsync(string email)
        {
            return await _context.Restaurants.FirstOrDefaultAsync(r => r.ContactEmail == email);
        }

        public async Task UpdateAsync(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();
        }
    }

}




//}