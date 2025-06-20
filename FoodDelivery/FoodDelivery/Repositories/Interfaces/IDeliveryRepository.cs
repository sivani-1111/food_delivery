using FoodDelivery.Models;

namespace FoodDelivery.Repositories.Interfaces 
{
    public interface IDeliveryRepository 
    { 
        Task<Delivery?> GetByIdAsync(int deliveryId); 
        Task<Delivery> AddAsync(Delivery delivery); 
        Task UpdateAsync(Delivery delivery); 
    } 
}

