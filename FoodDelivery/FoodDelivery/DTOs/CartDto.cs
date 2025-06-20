using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTOs
{
    public class CartDto { public int CartID { get; set; } public string ItemName { get; set; } = string.Empty; public decimal Price { get; set; } public int Quantity { get; set; } }

    public class CreateCartDto
    {
        [Required, StringLength(100)]
        public string ItemName { get; set; } = string.Empty;

        [Required, Range(1,100)]
        public int Quantity { get; set; }
    }

    public class UpdateCartDto
    {
        public int Quantity { get; set; }
    }

}



//namespace FoodDelivery.DTOs 
//{ 
//    public class CartDto 
//    { 
//        public string ItemName { get; set; } = string.Empty; 
//        public decimal Price { get; set; } 
//        public int Quantity { get; set; } }

//    public class CreateCartDto
//    {
//        public int CustomerID { get; set; }
//        public int ItemID { get; set; }
//        public int Quantity { get; set; }
//    }

//    public class UpdateCartDto
//    {
//        public string ItemName { get; set; } = string.Empty;
//        public int Quantity { get; set; }
//    }

//}


//public class CartDto
//{
//    public int CartID { get; set; }
//    public int CustomerID { get; set; }
//    public int ItemID { get; set; }
//    public int Quantity { get; set; }
//}

