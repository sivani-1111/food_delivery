using FoodDelivery.Models;

namespace FoodDelivery.Repositories.Interfaces { public interface ICartRepository { Task<IEnumerable<Cart>> GetByCustomerIdAsync(int customerId); Task<Cart?> GetByCartIdAsync(int cartId, int customerId); Task<Cart> AddAsync(Cart cart); Task UpdateAsync(Cart cart); Task DeleteAsync(Cart cart); } }

//using FoodDelivery.Models;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace FoodDelivery.Repositories.Interfaces 
//{ 
//    public interface ICartRepository 
//    { 
//        Task<IEnumerable<Cart>> GetByCustomerIdAsync(int customerId); 
//        Task<Cart?> GetByIdAndCustomerAsync(int cartId, int customerId); 
//        Task<Cart> AddAsync(Cart cart); Task UpdateAsync(Cart cart); 
//        Task DeleteAsync(Cart cart); 
//    } 
//}