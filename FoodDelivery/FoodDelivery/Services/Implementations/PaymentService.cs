using AutoMapper;
using FoodDelivery.DTOs;
using FoodDelivery.Models;
using FoodDelivery.Repositories.Interfaces;
using FoodDelivery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepo; private readonly FoodDeliverySystemContext _context; private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepo, FoodDeliverySystemContext context, IMapper mapper)
        {
            _paymentRepo = paymentRepo;
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaymentDto> AddAsync(CreatePaymentDto dto, string customerEmail)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == customerEmail);
            if (customer == null) throw new Exception("Invalid customer");

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == dto.OrderId);
            if (order == null || order.CustomerId != customer.CustomerId)
                throw new Exception("Unauthorized or invalid order");

            var payment = new Payment
            {
                OrderId = dto.OrderId,
                PaymentMethod = dto.PaymentMethod,
                Amount = order.TotalAmount, 
                Status = "Successful"
            };

            var saved = await _paymentRepo.AddAsync(payment);

            return _mapper.Map<PaymentDto>(saved);
        }
    }

}
