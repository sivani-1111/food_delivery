using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTOs
{
    public class PaymentDto 
    { 
        public int PaymentId { get; set; } 
        public decimal Amount { get; set; } 
        public string PaymentMethod { get; set; } = string.Empty; 
        public string Status { get; set; } = string.Empty; 
    }

    public class CreatePaymentDto
    {
        public int OrderId { get; set; }

        [Required]
        public string PaymentMethod { get; set; } = string.Empty;
       
    }

}
