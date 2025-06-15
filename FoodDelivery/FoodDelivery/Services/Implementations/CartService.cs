using AutoMapper;
using FoodDelivery.DTOs;
using FoodDelivery.Models;
using FoodDelivery.Repositories.Interfaces;
using FoodDelivery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository; private readonly FoodDeliverySystemContext _context; private readonly IMapper _mapper;

        public CartService(ICartRepository repository, FoodDeliverySystemContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CartDto>> GetByCustomerAsync(string email)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            if (customer == null) return Enumerable.Empty<CartDto>();
            var carts = await _repository.GetByCustomerIdAsync(customer.CustomerId);
            return _mapper.Map<IEnumerable<CartDto>>(carts);
        }

        public async Task<CartDto?> GetCartByIdAsync(int cartId, string email)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            if (customer == null) return null;
            var cart = await _repository.GetByCartIdAsync(cartId, customer.CustomerId);
            return cart == null ? null : _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto?> AddAsync(CreateCartDto dto, string email)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            var item = await _context.MenuItems.FirstOrDefaultAsync(i => i.Name == dto.ItemName);
            if (customer == null || item == null) return null;

            var existing = await _context.Carts.FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId && c.ItemId == item.ItemId);
            if (existing != null)
            {
                existing.Quantity += dto.Quantity;
                await _repository.UpdateAsync(existing);
                return _mapper.Map<CartDto>(existing);
            }

            var newCart = new Cart
            {
                CustomerId = customer.CustomerId,
                ItemId = item.ItemId,
                Quantity = dto.Quantity
            };
            var added = await _repository.AddAsync(newCart);
            return _mapper.Map<CartDto>(added);
        }

        public async Task<bool> UpdateAsync(int cartId, UpdateCartDto dto, string email)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            if (customer == null) return false;

            var cart = await _repository.GetByCartIdAsync(cartId, customer.CustomerId);
            if (cart == null) return false;

            cart.Quantity = dto.Quantity;
            await _repository.UpdateAsync(cart);
            return true;
        }

        public async Task<bool> DeleteAsync(int cartId, string email)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            if (customer == null) return false;

            var cart = await _repository.GetByCartIdAsync(cartId, customer.CustomerId);
            if (cart == null) return false;

            await _repository.DeleteAsync(cart);
            return true;
        }
    }

}


//using AutoMapper;
//using FoodDelivery.DTOs;
//using FoodDelivery.Models;
//using FoodDelivery.Repositories.Interfaces;
//using FoodDelivery.Services.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace FoodDelivery.Services.Implementations
//{
//    public class CartService : ICartService
//    {
//        private readonly ICartRepository _repository; 
//        private readonly IMapper _mapper; 
//        private readonly FoodDeliverySystemContext _context;

//        public CartService(ICartRepository repository, IMapper mapper, FoodDeliverySystemContext context)
//        {
//            _repository = repository;
//            _mapper = mapper;
//            _context = context;
//        }

//        public async Task<IEnumerable<CartDto>> GetByCustomerIdAsync(int customerId)
//        {
//            var carts = await _repository.GetByCustomerIdAsync(customerId);
//            return _mapper.Map<IEnumerable<CartDto>>(carts);
//        }

//        public async Task<CartDto?> GetByIdAsync(int cartId, int customerId)
//        {
//            var cart = await _repository.GetByIdAndCustomerAsync(cartId, customerId);
//            return _mapper.Map<CartDto?>(cart);
//        }

//        public async Task<CartDto> AddAsync(CreateCartDto dto)
//        {
//            var entity = _mapper.Map<Cart>(dto);
//            var created = await _repository.AddAsync(entity);
//            return _mapper.Map<CartDto>(created);
//        }

//        public async Task<CartDto?> UpdateAsync(int cartId, int customerId, UpdateCartDto dto)
//        {
//            var existing = await _repository.GetByIdAndCustomerAsync(cartId, customerId);
//            if (existing == null) return null;

//            var item = await _context.MenuItems.FirstOrDefaultAsync(i => i.Name == dto.ItemName);
//            if (item == null) return null;

//            existing.ItemId = item.ItemId;
//            existing.Quantity = dto.Quantity;

//            await _repository.UpdateAsync(existing);
//            return _mapper.Map<CartDto>(existing);
//        }

//        public async Task<bool> DeleteAsync(int cartId, int customerId)
//        {
//            var existing = await _repository.GetByIdAndCustomerAsync(cartId, customerId);
//            if (existing == null) return false;

//            await _repository.DeleteAsync(existing);
//            return true;
//        }
//    }

//}






//using AutoMapper;
//using FoodDelivery.DTOs;
//using FoodDelivery.Models;
//using FoodDelivery.Repositories.Interfaces;
//using FoodDelivery.Services.Interfaces;

//namespace FoodDelivery.Services.Implementations
//{
//    public class CartService : ICartService
//    {
//        private readonly ICartRepository _repository; private readonly IMapper _mapper;

//        public CartService(ICartRepository repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        public async Task<IEnumerable<CartDto>> GetByCustomerIdAsync(int customerId)
//        {
//            var carts = await _repository.GetByCustomerIdAsync(customerId);
//            return _mapper.Map<IEnumerable<CartDto>>(carts);
//        }

//        public async Task<CartDto?> GetByIdAsync(int cartId, int customerId)
//        {
//            var cart = await _repository.GetByIdAndCustomerAsync(cartId, customerId);
//            return _mapper.Map<CartDto?>(cart);
//        }

//        public async Task<CartDto> AddAsync(CreateCartDto dto)
//        {
//            var entity = _mapper.Map<Cart>(dto);
//            var created = await _repository.AddAsync(entity);
//            return _mapper.Map<CartDto>(created);
//        }

//        public async Task UpdateAsync(int cartId, int customerId, CreateCartDto dto)
//        {
//            var existing = await _repository.GetByIdAndCustomerAsync(cartId, customerId);
//            if (existing == null) return;

//            _mapper.Map(dto, existing);
//            await _repository.UpdateAsync(existing);
//        }

//        public async Task DeleteAsync(int cartId, int customerId)
//        {
//            var existing = await _repository.GetByIdAndCustomerAsync(cartId, customerId);
//            if (existing == null) return;

//            await _repository.DeleteAsync(existing);
//        }
//    }

//}