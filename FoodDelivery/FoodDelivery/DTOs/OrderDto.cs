namespace FoodDelivery.DTOs 
{ 
    public class OrderDto 
    { 
        public int OrderID { get; set; } 
        public string RestaurantName { get; set; } = string.Empty; 
        public decimal TotalAmount { get; set; } 
        public string Status { get; set; } = string.Empty; 
    } 
}

