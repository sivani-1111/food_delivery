using FoodDelivery.Models;
namespace FoodDelivery.Repositories.Interfaces 
{ 
    public interface IOrderRepository 
    { 
        Task<Order?> GetByIdAsync(int orderId); 
        Task<Order> AddAsync(Order order); 
    } 
}
