using AutoMapper;
using FoodDelivery.DTOs;
using FoodDelivery.Models;
using FoodDelivery.Repositories.Interfaces;
using FoodDelivery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository; private readonly FoodDeliverySystemContext _context; private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, FoodDeliverySystemContext context, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto> AddOrderFromCartAsync(string customerEmail)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == customerEmail);
            if (customer == null) throw new Exception("Invalid Customer");

            var cartItems = await _context.Carts
                .Include(c => c.Item)
                .Where(c => c.CustomerId == customer.CustomerId)
                .ToListAsync();

            if (!cartItems.Any()) throw new Exception("Cart is empty");

            var groupedByRestaurant = cartItems.GroupBy(c => c.Item.RestaurantId);
            var orders = new List<Order>();

            foreach (var group in groupedByRestaurant)
            {
                var totalAmount = group.Sum(ci => ci.Quantity * ci.Item.Price);

                var order = new Order
                {
                    CustomerId = customer.CustomerId,
                    RestaurantId = group.Key,
                    Status = "Pending",
                    TotalAmount = totalAmount
                };

                await _context.Orders.AddAsync(order);
                orders.Add(order);
            }

            await _context.SaveChangesAsync();

            _context.Carts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            var firstOrder = orders.First();
            var orderWithRestaurant = await _context.Orders.Include(o => o.Restaurant)
                        .FirstOrDefaultAsync(o => o.OrderId == firstOrder.OrderId);

            return _mapper.Map<OrderDto>(orderWithRestaurant);
        }

        public async Task<OrderDto?> GetByIdAsync(int orderId, string customerEmail)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == customerEmail);
            if (customer == null) return null;

            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null || order.CustomerId != customer.CustomerId) return null;

            return _mapper.Map<OrderDto>(order);
        }
    }

}

