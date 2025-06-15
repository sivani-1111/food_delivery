using AutoMapper;
using FoodDelivery.DTOs;
using FoodDelivery.Repositories.Interfaces;
using FoodDelivery.Services.Interfaces;

namespace FoodDelivery.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository; private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> UpdateAsync(UpdateCustomerDto dto, string email)
        {
            var customer = await _repository.GetByEmailAsync(email);
            if (customer == null) return false;

            _mapper.Map(dto, customer);
            await _repository.UpdateAsync(customer);
            return true;
        }
    }

}
//using AutoMapper;
//using FoodDelivery.DTOs;
//using FoodDelivery.Repositories.Interfaces;
//using FoodDelivery.Services.Interfaces;
//using System.Security.Cryptography;
//using System.Text;
//using FoodDelivery.Models;

//namespace FoodDelivery.Services.Implementations
//{
//    public class CustomerService : ICustomerService
//    {
//        private readonly ICustomerRepository _repo;
//        private readonly IMapper _mapper;

//        public CustomerService(ICustomerRepository repo, IMapper mapper)
//        {
//            _repo = repo;
//            _mapper = mapper;
//        }

//        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
//        {
//            var data = await _repo.GetAllAsync();
//            return _mapper.Map<IEnumerable<CustomerDto>>(data);
//        }

//        public async Task<CustomerDto?> GetByIdAsync(int id)
//        {
//            var customer = await _repo.GetByIdAsync(id);
//            return customer == null ? null : _mapper.Map<CustomerDto>(customer);
//        }

//        public async Task<bool> RegisterAsync(CreateCustomerDto dto)
//        {
//            var existing = await _repo.GetByEmailAsync(dto.Email);
//            if (existing != null) return false;

//            var customer = _mapper.Map<Customer>(dto);
//            customer.PasswordHash = HashPassword(dto.Password);

//            await _repo.AddAsync(customer);
//            await _repo.SaveChangesAsync();
//            return true;
//        }

//        public async Task<CustomerDto?> LoginAsync(LoginDto dto)
//        {
//            var customer = await _repo.GetByEmailAsync(dto.Email);
//            if (customer == null || string.IsNullOrEmpty(customer.PasswordHash) || !VerifyPassword(dto.Password, customer.PasswordHash))
//                return null;

//            return _mapper.Map<CustomerDto>(customer);
//        }

//        public async Task<bool> UpdateAsync(int id, CreateCustomerDto dto)
//        {
//            var customer = await _repo.GetByIdAsync(id);
//            if (customer == null) return false;

//            customer.Name = dto.Name;
//            customer.Phone = dto.Phone;
//            customer.Address = dto.Address;
//            customer.PasswordHash = HashPassword(dto.Password);

//            _repo.Update(customer);
//            await _repo.SaveChangesAsync();
//            return true;
//        }

//        private string HashPassword(string password)
//        {
//            using var sha = SHA256.Create();
//            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
//            return Convert.ToBase64String(bytes);
//        }

//        private bool VerifyPassword(string password, string hash) =>
//            HashPassword(password) == hash;
//    }
//}