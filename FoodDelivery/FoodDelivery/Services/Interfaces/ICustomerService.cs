using FoodDelivery.DTOs;

namespace FoodDelivery.Services.Interfaces 
{ 
    public interface ICustomerService 
    { 
        Task<bool> UpdateAsync(UpdateCustomerDto dto, string email); 
    } 
}
