using FoodDelivery.Models;

namespace FoodDelivery.Repositories.Interfaces { public interface IPaymentRepository { Task<Payment> AddAsync(Payment payment); } }