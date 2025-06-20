using FoodDelivery.Models;
using FoodDelivery.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace FoodDelivery.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FoodDeliverySystemContext _context; public OrderRepository(FoodDeliverySystemContext context) => _context = context;

        public async Task<Order?> GetByIdAsync(int orderId)
        {
            return await _context.Orders
    .Include(o => o.Restaurant)
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

