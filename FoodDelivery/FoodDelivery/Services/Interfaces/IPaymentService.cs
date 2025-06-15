using FoodDelivery.DTOs;

namespace FoodDelivery.Services.Interfaces { public interface IPaymentService { Task<PaymentDto> AddAsync(CreatePaymentDto dto, string customerEmail); } }