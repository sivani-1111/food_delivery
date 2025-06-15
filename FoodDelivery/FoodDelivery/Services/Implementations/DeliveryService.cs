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




//using AutoMapper;
//using FoodDelivery.DTOs;
//using FoodDelivery.Models;
//using FoodDelivery.Repositories.Interfaces;
//using FoodDelivery.Services.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace FoodDelivery.Services.Implementations
//{
//    public class DeliveryService : IDeliveryService
//    {
//        private readonly IDeliveryRepository _repository; private readonly IMapper _mapper; private readonly FoodDeliverySystemContext _context;

//        public DeliveryService(IDeliveryRepository repository, IMapper mapper, FoodDeliverySystemContext context)
//        {
//            _repository = repository;
//            _mapper = mapper;
//            _context = context;
//        }

//        public async Task<DeliveryDto?> GetByIdAsync(int deliveryId, int restaurantId)
//        {
//            var delivery = await _repository.GetByIdAsync(deliveryId);
//            if (delivery?.Order?.RestaurantId != restaurantId) return null;
//            return _mapper.Map<DeliveryDto?>(delivery);
//        }

//        public async Task<DeliveryDto> AddAsync(CreateDeliveryDto dto, int restaurantId)
//        {
//            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == dto.OrderID && o.RestaurantId == restaurantId);
//            if (order == null) throw new UnauthorizedAccessException();

//            var entity = _mapper.Map<Delivery>(dto);
//            entity.Status = "In Progress";
//            var created = await _repository.AddAsync(entity);
//            return _mapper.Map<DeliveryDto>(created);
//        }

//        public async Task<bool> UpdateAsync(int deliveryId, int restaurantId, UpdateDeliveryDto dto)
//        {
//            var existing = await _repository.GetByIdAsync(deliveryId);
//            if (existing == null || existing.Order?.RestaurantId != restaurantId) return false;

//            existing.Status = dto.Status;
//            existing.EstimatedTimeOfArrival = dto.EstimatedTimeOfArrival;
//            await _repository.UpdateAsync(existing);
//            return true;
//        }
//    }

//}



//using AutoMapper;
//using FoodDelivery.DTOs;
//using FoodDelivery.Models;
//using FoodDelivery.Repositories.Interfaces;
//using FoodDelivery.Services.Interfaces;

//namespace FoodDelivery.Services.Implementations
//{
//    public class DeliveryService : IDeliveryService
//    {
//        private readonly IDeliveryRepository _repository;
//        private readonly IMapper _mapper;

//        public DeliveryService(IDeliveryRepository repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        public async Task<IEnumerable<DeliveryDto>> GetAllAsync()
//        {
//            var deliveries = await _repository.GetAllAsync();
//            return _mapper.Map<IEnumerable<DeliveryDto>>(deliveries);
//        }

//        public async Task<DeliveryDto?> GetByIdAsync(int id)
//        {
//            var delivery = await _repository.GetByIdAsync(id);
//            return _mapper.Map<DeliveryDto?>(delivery);
//        }

//        public async Task<DeliveryDto> AddAsync(CreateDeliveryDto dto)
//        {
//            var entity = _mapper.Map<Delivery>(dto);
//            var created = await _repository.AddAsync(entity);
//            return _mapper.Map<DeliveryDto>(created);
//        }

//        public async Task UpdateAsync(int id, CreateDeliveryDto dto)
//        {
//            var existing = await _repository.GetByIdAsync(id);
//            if (existing == null) return;
//            _mapper.Map(dto, existing);
//            await _repository.UpdateAsync(existing);
//        }

//        public async Task DeleteAsync(int id)
//        {
//            await _repository.DeleteAsync(id);
//        }
//    }
//}