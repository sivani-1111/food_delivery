using AutoMapper;
using FoodDelivery.DTOs;
using FoodDelivery.Repositories.Interfaces;
using FoodDelivery.Services.Interfaces;

namespace FoodDelivery.Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _repository; private readonly IMapper _mapper;

        public RestaurantService(IRestaurantRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> UpdateAsync(UpdateRestaurantDto dto, string email)
        {
            var restaurant = await _repository.GetByEmailAsync(email);
            if (restaurant == null) return false;

            _mapper.Map(dto, restaurant);
            await _repository.UpdateAsync(restaurant);
            return true;
        }
    }

}


