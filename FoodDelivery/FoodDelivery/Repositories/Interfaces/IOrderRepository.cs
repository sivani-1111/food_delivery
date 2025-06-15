using FoodDelivery.Models;

namespace FoodDelivery.Repositories.Interfaces { public interface IOrderRepository { Task<Order?> GetByIdAsync(int orderId); Task<Order> AddAsync(Order order); } }


//using FoodDelivery.Models;

//namespace FoodDelivery.Repositories.Interfaces
//{
//    public interface IOrderRepository
//    {
//        Task<IEnumerable<Order>> GetAllAsync();
//        Task<Order?> GetByIdAsync(int id);
//        Task<Order> AddAsync(Order order);
//        Task UpdateAsync(Order order);
//        Task DeleteAsync(int id);
//    }
//}
