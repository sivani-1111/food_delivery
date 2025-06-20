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


