using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTOs
{
    public class DeliveryDto { public int DeliveryID { get; set; } public string Status { get; set; } = string.Empty; public DateTime EstimatedTimeOfArrival { get; set; } public string AgentName { get; set; } = string.Empty; public string RestaurantName { get; set; } = string.Empty; }

    public class CreateDeliveryDto
    {
        [Required]
        public int OrderID { get; set; }

        [Required]
        public int AgentID { get; set; }
        
        [Required]
        public DateTime EstimatedTimeOfArrival { get; set; }
    }

    public class UpdateDeliveryDto
    {
        [Required]
        public string Status { get; set; } = string.Empty;
        
        [Required]
        public DateTime EstimatedTimeOfArrival { get; set; }
    }

}


