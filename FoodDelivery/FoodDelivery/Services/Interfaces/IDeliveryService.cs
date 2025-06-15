using FoodDelivery.DTOs;

namespace FoodDelivery.Services.Interfaces { public interface IDeliveryService { Task<DeliveryDto?> GetByIdAsync(int deliveryId, string restaurantEmail); Task<DeliveryDto> AddAsync(CreateDeliveryDto dto, string restaurantEmail); Task<bool> UpdateAsync(int deliveryId, UpdateDeliveryDto dto, string restaurantEmail); } }


//using FoodDelivery.DTOs;

//namespace FoodDelivery.Services.Interfaces 
//{ 
//    public interface IDeliveryService 
//    { 
//        Task<DeliveryDto?> GetByIdAsync(int deliveryId, int restaurantId); 
//        Task<DeliveryDto> AddAsync(CreateDeliveryDto dto, int restaurantId); 
//        Task<bool> UpdateAsync(int deliveryId, int restaurantId, UpdateDeliveryDto dto); 
//    } 
//}