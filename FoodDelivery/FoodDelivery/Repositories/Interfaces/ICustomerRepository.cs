using FoodDelivery.Models;

namespace FoodDelivery.Repositories.Interfaces { public interface ICustomerRepository { Task<Customer?> GetByEmailAsync(string email); Task UpdateAsync(Customer customer); } }//using FoodDelivery.Models;
//namespace FoodDelivery.Repositories.Interfaces
//{
//    public interface ICustomerRepository
//    {
//        Task<IEnumerable<Customer>> GetAllAsync();
//        Task<Customer?> GetByIdAsync(int id);
//        Task<Customer?> GetByEmailAsync(string email);
//        Task AddAsync(Customer customer);
//        void Update(Customer customer);
//        Task SaveChangesAsync();
//    }
//}

