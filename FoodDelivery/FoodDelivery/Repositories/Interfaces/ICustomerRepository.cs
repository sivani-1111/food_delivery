using FoodDelivery.Models;

namespace FoodDelivery.Repositories.Interfaces 
{ 
    public interface ICustomerRepository 
    { 
        Task<Customer?> GetByEmailAsync(string email); 
        Task UpdateAsync(Customer customer); 
    } 
}//using FoodDelivery.Models;
