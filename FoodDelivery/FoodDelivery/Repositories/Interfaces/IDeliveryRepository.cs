using FoodDelivery.Models;

namespace FoodDelivery.Repositories.Interfaces { public interface IDeliveryRepository { Task<Delivery?> GetByIdAsync(int deliveryId); Task<Delivery> AddAsync(Delivery delivery); Task UpdateAsync(Delivery delivery); } }

//using FoodDelivery.Models;

//namespace FoodDelivery.Repositories.Interfaces 
//{ 
//    public interface IDeliveryRepository 
//    { 
//        Task<Delivery?> GetByIdAsync(int id); 
//        Task<Delivery> AddAsync(Delivery delivery); 
//        Task UpdateAsync(Delivery delivery); 
//    } 
//}



//using FoodDelivery.Models;

//namespace FoodDelivery.Repositories.Interfaces
//{
//    public interface IDeliveryRepository
//    {
//        Task<IEnumerable<Delivery>> GetAllAsync();
//        Task<Delivery?> GetByIdAsync(int id);
//        Task<Delivery> AddAsync(Delivery delivery);
//        Task UpdateAsync(Delivery delivery);
//        Task DeleteAsync(int id);
//    }
//}
