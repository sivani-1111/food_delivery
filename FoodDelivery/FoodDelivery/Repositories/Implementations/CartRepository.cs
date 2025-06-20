using FoodDelivery.Models;
using FoodDelivery.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly FoodDeliverySystemContext _context;

        public CartRepository(FoodDeliverySystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cart>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Carts
                .Include(c => c.Item)
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<Cart?> GetByCartIdAsync(int cartId, int customerId)
        {
            return await _context.Carts.Include(c => c.Item)
                .FirstOrDefaultAsync(c => c.CartId == cartId && c.CustomerId == customerId);
        }

        public async Task<Cart> AddAsync(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task UpdateAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Cart cart)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }
    }

}

}
