using FoodDelivery.Models;
using FoodDelivery.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FoodDeliverySystemContext _context;

        public CustomerRepository(FoodDeliverySystemContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }
    }

}

//using FoodDelivery.Models;
//using Microsoft.EntityFrameworkCore;
//using FoodDelivery.Repositories.Interfaces;

//namespace FoodDelivery.Repositories.Implementations
//{
//    public class CustomerRepository : ICustomerRepository
//    {
//        private readonly FoodDeliverySystemContext _context;

//        public CustomerRepository(FoodDeliverySystemContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Customer>> GetAllAsync() =>
//            await _context.Customers.ToListAsync();

//        public async Task<Customer?> GetByIdAsync(int id) =>
//            await _context.Customers.FindAsync(id);

//        public async Task<Customer?> GetByEmailAsync(string email) =>
//            await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);

//        public async Task AddAsync(Customer customer) =>
//            await _context.Customers.AddAsync(customer);

//        public void Update(Customer customer) =>
//            _context.Customers.Update(customer);

//        public async Task SaveChangesAsync() =>
//            await _context.SaveChangesAsync();
//    }
//}