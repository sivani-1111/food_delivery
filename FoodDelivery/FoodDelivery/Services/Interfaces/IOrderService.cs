//using FoodDelivery.DTOs;

//namespace FoodDelivery.Services.Interfaces { public interface IOrderService { Task<OrderDto?> GetByIdAsync(int orderId, int customerId); Task<OrderDto> AddAsync(CreateOrderDto dto); } }

using FoodDelivery.DTOs;

namespace FoodDelivery.Services.Interfaces { public interface IOrderService { Task<OrderDto?> GetByIdAsync(int orderId, string customerEmail); Task<OrderDto> AddAsync(CreateOrderDto dto, string customerEmail); } }