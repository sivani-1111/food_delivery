using FoodDelivery.DTOs;

namespace FoodDelivery.Services.Interfaces { public interface ICustomerService { Task<bool> UpdateAsync(UpdateCustomerDto dto, string email); } }
//using FoodDelivery.DTOs;


//namespace FoodDelivery.Services.Interfaces
//{
//    public interface ICustomerService
//    {
//        Task<IEnumerable<CustomerDto>> GetAllAsync();
//        Task<CustomerDto?> GetByIdAsync(int id);
//        Task<bool> RegisterAsync(CreateCustomerDto dto);
//        Task<CustomerDto?> LoginAsync(LoginDto dto);
//        Task<bool> UpdateAsync(int id, CreateCustomerDto dto);
//    }
//}