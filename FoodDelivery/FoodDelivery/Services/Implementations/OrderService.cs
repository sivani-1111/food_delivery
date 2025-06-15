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
        private readonly IOrderRepository _repository; private readonly FoodDeliverySystemContext _context; private readonly IMapper _mapper;

        public OrderService(IOrderRepository repository, FoodDeliverySystemContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto?> GetByIdAsync(int orderId, string customerEmail)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == customerEmail);
            if (customer == null) return null;

            var order = await _repository.GetByIdAsync(orderId);
            if (order == null || order.CustomerId != customer.CustomerId) return null;

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> AddAsync(CreateOrderDto dto, string customerEmail)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == customerEmail);
            if (customer == null) throw new Exception("Unauthorized");

            var order = new Order
            {
                CustomerId = customer.CustomerId,
                RestaurantId = dto.RestaurantID,
                Status = "Pending",
                TotalAmount = dto.TotalAmount
            };

            var created = await _repository.AddAsync(order);
            return _mapper.Map<OrderDto>(created);
        }
    }

}



//using AutoMapper;
//using FoodDelivery.DTOs;
//using FoodDelivery.Models;
//using FoodDelivery.Repositories.Interfaces;
//using FoodDelivery.Services.Interfaces;

//namespace FoodDelivery.Services.Implementations
//{
//    public class OrderService : IOrderService
//    {
//        private readonly IOrderRepository _repository; private readonly IMapper _mapper;

//        public OrderService(IOrderRepository repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        public async Task<OrderDto?> GetByIdAsync(int orderId, int customerId)
//        {
//            var order = await _repository.GetByIdAsync(orderId);
//            if (order == null || order.CustomerId != customerId) return null;
//            return _mapper.Map<OrderDto>(order);
//        }

//        public async Task<OrderDto> AddAsync(CreateOrderDto dto)
//        {
//            var order = new Order
//            {
//                CustomerId = dto.CustomerID,
//                RestaurantId = dto.RestaurantID,
//                Status = "Pending",
//                TotalAmount = dto.TotalAmount
//            };

//            var created = await _repository.AddAsync(order);
//            return _mapper.Map<OrderDto>(created);
//        }
//    }

//}
