using FoodDelivery.DTOs;

namespace FoodDelivery.Services.Interfaces { public interface ICartService { Task<IEnumerable<CartDto>> GetByCustomerAsync(string email); Task<CartDto?> GetCartByIdAsync(int cartId, string email); Task<CartDto?> AddAsync(CreateCartDto dto, string email); Task<bool> UpdateAsync(int cartId, UpdateCartDto dto, string email); Task<bool> DeleteAsync(int cartId, string email); } }
//using FoodDelivery.DTOs;

//namespace FoodDelivery.Services.Interfaces 
//{ 
//    public interface ICartService 
//    { 
//        Task<IEnumerable<CartDto>> GetByCustomerIdAsync(int customerId); 
//        Task<CartDto?> GetByIdAsync(int cartId, int customerId); 
//        Task<CartDto> AddAsync(CreateCartDto dto); 
//        Task<CartDto?> UpdateAsync(int cartId, int customerId, UpdateCartDto dto); 
//        Task<bool> DeleteAsync(int cartId, int customerId); 
//    } 
//}





//using FoodDelivery.DTOs;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace FoodDelivery.Services.Interfaces 
//{ 
//    public interface ICartService 
//    { 
//        Task<IEnumerable<CartDto>> GetByCustomerIdAsync(int customerId); 
//        Task<CartDto?> GetByIdAsync(int cartId, int customerId); 
//        Task<CartDto> AddAsync(CreateCartDto dto); 
//        Task UpdateAsync(int cartId, int customerId, CreateCartDto dto); 
//        Task DeleteAsync(int cartId, int customerId); 
//    } 
//}