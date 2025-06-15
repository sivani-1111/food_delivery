using FoodDelivery.Models;
using FoodDelivery.Repositories.Interfaces;

namespace FoodDelivery.Repositories.Implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly FoodDeliverySystemContext _context;

        public PaymentRepository(FoodDeliverySystemContext context)
        {
            _context = context;
        }

        public async Task<Payment> AddAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
    }

}
