using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTOs
{
    public class RestaurantDto 
    { 
        public int RestaurantID { get; set; } 
        public string Name { get; set; } = string.Empty; 
        public string ContactEmail { get; set; } = string.Empty; 
        public string Phone { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty; 
    }

    public class UpdateRestaurantDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, Phone]
        public string Phone { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string Address { get; set; } = string.Empty;
    }

}


