namespace FoodDelivery.DTOs
{
    public class CustomerDto { public int CustomerID { get; set; } public string? Name { get; set; } public string? Email { get; set; } public string? Phone { get; set; } public string? Address { get; set; } }

    public class UpdateCustomerDto
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

}



//namespace FoodDelivery.DTOs
//{
//    public class CustomerDto
//    {
//        public int CustomerID { get; set; }
//        public string? Name { get; set; }
//        public string? Email { get; set; }
//        public string? Phone { get; set; }
//        public string? Address { get; set; }
//    }

//    public class CreateCustomerDto
//    {
//        public string Name { get; set; } = null!;
//        public string Email { get; set; } = null!;
//        public string Phone { get; set; } = null!;
//        public string Address { get; set; } = null!;
//        public string Password { get; set; } = null!;
//    }

//public class LoginDto
//{
//    public string Email { get; set; } = null!;
//    public string Password { get; set; } = null!;
//}


