using FoodDelivery.Models;

namespace FoodDelivery.Repositories.Interfaces { public interface IRestaurantRepository { Task<Restaurant?> GetByEmailAsync(string email); Task UpdateAsync(Restaurant restaurant); } }


//using FoodDelivery.Models;

//namespace FoodDelivery.Repositories.Interfaces
//{
//    public interface IRestaurantRepository
//    {
//        Task<IEnumerable<Restaurant>> GetAllAsync();
//        Task<Restaurant?> GetByIdAsync(int id);
//        Task<Restaurant> AddAsync(Restaurant restaurant);
//        Task UpdateAsync(Restaurant restaurant);
//        Task DeleteAsync(int id);
//    }
//}

