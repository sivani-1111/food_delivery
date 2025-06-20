using FoodDelivery.DTOs;

namespace FoodDelivery.Services.Interfaces 
{ 
    public interface ICartService 
    { 
        Task<IEnumerable<CartDto>> GetByCustomerAsync(string email); 
        Task<CartDto?> GetCartByIdAsync(int cartId, string email); 
        Task<CartDto?> AddAsync(CreateCartDto dto, string email); 
        Task<bool> UpdateAsync(int cartId, UpdateCartDto dto, string email); 
        Task<bool> DeleteAsync(int cartId, string email); 
    } 
}