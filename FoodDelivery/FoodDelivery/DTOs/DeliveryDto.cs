namespace FoodDelivery.DTOs
{
    public class DeliveryDto { public int DeliveryID { get; set; } public string Status { get; set; } = string.Empty; public DateTime EstimatedTimeOfArrival { get; set; } public string AgentName { get; set; } = string.Empty; public string RestaurantName { get; set; } = string.Empty; }

    public class CreateDeliveryDto
    {
        public int OrderID { get; set; }
        public int AgentID { get; set; }
        public DateTime EstimatedTimeOfArrival { get; set; }
    }

    public class UpdateDeliveryDto
    {
        public string Status { get; set; } = string.Empty;
        public DateTime EstimatedTimeOfArrival { get; set; }
    }

}


//namespace FoodDelivery.DTOs
//{
//    public class DeliveryDto 
//    { 
//        public int DeliveryID { get; set; } 
//        public string Status { get; set; } = string.Empty; 
//        public DateTime EstimatedTimeOfArrival { get; set; } 
//        public string AgentName { get; set; } = string.Empty;
//        public string RestaurantName { get; set; } = string.Empty;
//    }

//    public class CreateDeliveryDto
//    {
//        public int OrderID { get; set; }
//        public int AgentID { get; set; }
//        public DateTime EstimatedTimeOfArrival { get; set; }
//    }

//    public class UpdateDeliveryDto
//    {
//        public string Status { get; set; } = string.Empty;
//        public DateTime EstimatedTimeOfArrival { get; set; }
//    }

//}