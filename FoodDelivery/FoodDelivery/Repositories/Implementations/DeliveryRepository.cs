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


