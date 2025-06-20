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
