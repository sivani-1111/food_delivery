using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTOs
{
    public class CustomerDto 
    { 
        public int CustomerID { get; set; } 
        public string? Name { get; set; } 
        public string? Email { get; set; } 
        public string? Phone { get; set; } 
        public string? Address { get; set; 
        } 
    }

    public class UpdateCustomerDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, Phone]
        public string Phone { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Address { get; set; } = string.Empty;
    }

}



