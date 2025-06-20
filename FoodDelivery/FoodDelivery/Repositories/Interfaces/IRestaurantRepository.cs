using FoodDelivery.Models;

namespace FoodDelivery.Repositories.Interfaces 
{ 
    public interface IRestaurantRepository 
    { 
        Task<Restaurant?> GetByEmailAsync(string email); 
        Task UpdateAsync(Restaurant restaurant); 
    } 
}



