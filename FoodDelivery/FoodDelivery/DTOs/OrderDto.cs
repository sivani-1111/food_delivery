namespace FoodDelivery.DTOs
{
    public class OrderDto { public int OrderID { get; set; } public string Status { get; set; } = string.Empty; public decimal TotalAmount { get; set; } public string RestaurantName { get; set; } = string.Empty; }

    public class CreateOrderDto
    {
        public int RestaurantID { get; set; }
        public decimal TotalAmount { get; set; }
    }

}



//namespace FoodDelivery.DTOs
//{
//    public class OrderDto 
//    { 
//        public int OrderID { get; set; } 
//        public string Status { get; set; } = string.Empty; 
//        public decimal TotalAmount { get; set; } }

//    public class CreateOrderDto
//    {
//        public int CustomerID { get; set; }
//        public int RestaurantID { get; set; }
//        public decimal TotalAmount { get; set; }
//    }

//}