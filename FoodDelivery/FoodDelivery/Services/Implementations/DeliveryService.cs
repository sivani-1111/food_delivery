using AutoMapper;
using FoodDelivery.DTOs;
using FoodDelivery.Models;
using FoodDelivery.Repositories.Interfaces;
using FoodDelivery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Services.Implementations
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _repository; private readonly FoodDeliverySystemContext _context; private readonly IMapper _mapper;

        public DeliveryService(IDeliveryRepository repository, FoodDeliverySystemContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<DeliveryDto?> GetByIdAsync(int deliveryId, string restaurantEmail)
        {
            var delivery = await _repository.GetByIdAsync(deliveryId);
            if (delivery == null || delivery.Order == null) return null;

            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.ContactEmail == restaurantEmail);
            if (restaurant == null || delivery.Order.RestaurantId != restaurant.RestaurantId) return null;

            return _mapper.Map<DeliveryDto>(delivery);
        }

        public async Task<DeliveryDto> AddAsync(CreateDeliveryDto dto, string restaurantEmail)
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.ContactEmail == restaurantEmail);
            if (restaurant == null) throw new Exception("Unauthorized");

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == dto.OrderID);
            if (order == null || order.RestaurantId != restaurant.RestaurantId)
                throw new Exception("Invalid or unauthorized order");

            var delivery = _mapper.Map<Delivery>(dto);
            delivery.Status = "In Progress";
            var created = await _repository.AddAsync(delivery);
            return _mapper.Map<DeliveryDto>(created);
        }

        public async Task<bool> UpdateAsync(int deliveryId, UpdateDeliveryDto dto, string restaurantEmail)
        {
            var delivery = await _repository.GetByIdAsync(deliveryId);
            if (delivery == null || delivery.Order == null) return false;

            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.ContactEmail == restaurantEmail);
            if (restaurant == null || delivery.Order.RestaurantId != restaurant.RestaurantId) return false;

            _mapper.Map(dto, delivery);
            await _repository.UpdateAsync(delivery);
            return true;
        }
    }

}




