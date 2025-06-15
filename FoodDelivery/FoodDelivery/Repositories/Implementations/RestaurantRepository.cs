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




//using FoodDelivery.Models;
//using Microsoft.EntityFrameworkCore;
//using FoodDelivery.Repositories.Interfaces;

//namespace FoodDelivery.Repositories.Implementations
//{
//    public class RestaurantRepository : IRestaurantRepository
//    {
//        private readonly FoodDeliverySystemContext _context;

//        public RestaurantRepository(FoodDeliverySystemContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Restaurant>> GetAllAsync()
//        {
//            return await _context.Restaurants.ToListAsync();
//        }

//        public async Task<Restaurant?> GetByIdAsync(int id)
//        {
//            return await _context.Restaurants.FindAsync(id);
//        }

//        public async Task<Restaurant> AddAsync(Restaurant restaurant)
//        {
//            _context.Restaurants.Add(restaurant);
//            await _context.SaveChangesAsync();
//            return restaurant;
//        }

//        public async Task UpdateAsync(Restaurant restaurant)
//        {
//            _context.Entry(restaurant).State = EntityState.Modified;
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeleteAsync(int id)
//        {
//            var restaurant = await _context.Restaurants.FindAsync(id);
//            if (restaurant != null)
//            {
//                _context.Restaurants.Remove(restaurant);
//                await _context.SaveChangesAsync();
//            }
//        }
//    }
//}