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


//using AutoMapper;
//using FoodDelivery.DTOs;
//using FoodDelivery.Models;
//using FoodDelivery.Repositories.Interfaces;
//using FoodDelivery.Services.Interfaces;

//namespace FoodDelivery.Services.Implementations
//{
//    public class RestaurantService : IRestaurantService
//    {
//        private readonly IRestaurantRepository _repository;
//        private readonly IMapper _mapper;

//        public RestaurantService(IRestaurantRepository repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
//        {
//            var restaurants = await _repository.GetAllAsync();
//            return _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
//        }

//        public async Task<RestaurantDto?> GetByIdAsync(int id)
//        {
//            var restaurant = await _repository.GetByIdAsync(id);
//            return _mapper.Map<RestaurantDto?>(restaurant);
//        }

//        public async Task<RestaurantDto> AddAsync(CreateRestaurantDto dto)
//        {
//            var restaurant = _mapper.Map<Restaurant>(dto);
//            var created = await _repository.AddAsync(restaurant);
//            return _mapper.Map<RestaurantDto>(created);
//        }

//        public async Task UpdateAsync(int id, CreateRestaurantDto dto)
//        {
//            var existing = await _repository.GetByIdAsync(id);
//            if (existing == null) return;
//            _mapper.Map(dto, existing);
//            await _repository.UpdateAsync(existing);
//        }

//        public async Task DeleteAsync(int id)
//        {
//            await _repository.DeleteAsync(id);
//        }
//    }
//}

