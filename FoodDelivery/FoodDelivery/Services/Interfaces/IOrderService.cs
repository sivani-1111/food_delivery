using FoodDelivery.DTOs;
namespace FoodDelivery.Services.Interfaces 
{ 
    public interface IOrderService 
    { 
        Task<OrderDto> AddOrderFromCartAsync(string customerEmail); 
        Task<OrderDto?> GetByIdAsync(int orderId, string customerEmail); 
    } 
}


