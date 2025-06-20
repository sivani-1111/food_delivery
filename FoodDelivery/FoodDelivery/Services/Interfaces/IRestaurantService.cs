using FoodDelivery.DTOs;

namespace FoodDelivery.Services.Interfaces 
{ 
    public interface IRestaurantService 
    { 
        Task<bool> UpdateAsync(UpdateRestaurantDto dto, string email); 
    } 
}

