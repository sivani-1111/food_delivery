using FoodDelivery.Models;
using FoodDelivery.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FoodDeliverySystemContext _context;

        public OrderRepository(FoodDeliverySystemContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetByIdAsync(int orderId)
        {
            return await _context.Orders.Include(o => o.Restaurant)
                        .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<Order> AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }

}



//using FoodDelivery.Models;
//using FoodDelivery.Repositories.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace FoodDelivery.Repositories.Implementations
//{
//    public class OrderRepository : IOrderRepository
//    {
//        private readonly FoodDeliverySystemContext _context;

//        public OrderRepository(FoodDeliverySystemContext context)
//        {
//            _context = context;
//        }

//        public async Task<Order?> GetByIdAsync(int orderId)
//        {
//            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
//        }

//        public async Task<Order> AddAsync(Order order)
//        {
//            _context.Orders.Add(order);
//            await _context.SaveChangesAsync();
//            return order;
//        }
//    }

//}



//using FoodDelivery.Models;
//using Microsoft.EntityFrameworkCore;
//using FoodDelivery.Repositories.Interfaces;

//namespace FoodDelivery.Repositories.Implementations
//{
//    public class OrderRepository : IOrderRepository
//    {
//        private readonly FoodDeliverySystemContext _context;

//        public OrderRepository(FoodDeliverySystemContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Order>> GetAllAsync()
//        {
//            return await _context.Orders.ToListAsync();
//        }

//        public async Task<Order?> GetByIdAsync(int id)
//        {
//            return await _context.Orders.FindAsync(id);
//        }

//        public async Task<Order> AddAsync(Order order)
//        {
//            _context.Orders.Add(order);
//            await _context.SaveChangesAsync();
//            return order;
//        }

//        public async Task UpdateAsync(Order order)
//        {
//            _context.Entry(order).State = EntityState.Modified;
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeleteAsync(int id)
//        {
//            var order = await _context.Orders.FindAsync(id);
//            if (order != null)
//            {
//                _context.Orders.Remove(order);
//                await _context.SaveChangesAsync();
//            }
//        }
//    }
//}