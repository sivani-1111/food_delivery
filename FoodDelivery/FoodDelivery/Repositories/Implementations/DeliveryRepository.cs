using FoodDelivery.Models;
using FoodDelivery.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repositories.Implementations
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly FoodDeliverySystemContext _context;

        public DeliveryRepository(FoodDeliverySystemContext context)
        {
            _context = context;
        }

        public async Task<Delivery?> GetByIdAsync(int deliveryId)
        {
            return await _context.Deliveries
                .Include(d => d.Agent)
                .Include(d => d.Order)
    .ThenInclude(o => o.Restaurant)
                .FirstOrDefaultAsync(d => d.DeliveryId == deliveryId);
        }

        public async Task<Delivery> AddAsync(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();
            return delivery;
        }

        public async Task UpdateAsync(Delivery delivery)
        {
            _context.Deliveries.Update(delivery);
            await _context.SaveChangesAsync();
        }
    }

}


//using FoodDelivery.Models;
//using FoodDelivery.Repositories.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace FoodDelivery.Repositories.Implementations
//{
//    public class DeliveryRepository : IDeliveryRepository
//    {
//        private readonly FoodDeliverySystemContext _context;

//        public DeliveryRepository(FoodDeliverySystemContext context)
//        {
//            _context = context;
//        }

//        public async Task<Delivery?> GetByIdAsync(int id)
//        {
//            return await _context.Deliveries
//                .Include(d => d.Agent)
//                .Include(d => d.Order)
//                .FirstOrDefaultAsync(d => d.DeliveryId == id);
//        }

//        public async Task<Delivery> AddAsync(Delivery delivery)
//        {
//            _context.Deliveries.Add(delivery);
//            await _context.SaveChangesAsync();
//            return delivery;
//        }

//        public async Task UpdateAsync(Delivery delivery)
//        {
//            _context.Deliveries.Update(delivery);
//            await _context.SaveChangesAsync();
//        }
//    }

//}


//using FoodDelivery.Models;
//using Microsoft.EntityFrameworkCore;
//using FoodDelivery.Repositories.Interfaces;

//namespace FoodDelivery.Repositories.Implementations
//{
//    public class DeliveryRepository : IDeliveryRepository
//    {
//        private readonly FoodDeliverySystemContext _context;

//        public DeliveryRepository(FoodDeliverySystemContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Delivery>> GetAllAsync()
//        {
//            return await _context.Deliveries.ToListAsync();
//        }

//        public async Task<Delivery?> GetByIdAsync(int id)
//        {
//            return await _context.Deliveries.FindAsync(id);
//        }

//        public async Task<Delivery> AddAsync(Delivery delivery)
//        {
//            _context.Deliveries.Add(delivery);
//            await _context.SaveChangesAsync();
//            return delivery;
//        }

//        public async Task UpdateAsync(Delivery delivery)
//        {
//            _context.Entry(delivery).State = EntityState.Modified;
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeleteAsync(int id)
//        {
//            var delivery = await _context.Deliveries.FindAsync(id);
//            if (delivery != null)
//            {
//                _context.Deliveries.Remove(delivery);
//                await _context.SaveChangesAsync();
//            }
//        }
//    }
//}