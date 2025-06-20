using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required,RegularExpression("^(Customer|RestaurantOwner)$")]
        public string Role { get; set; } = null!;
    }

    public class LoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required]

        public string Role { get; set; } = null!;
    }

    public class AuthResponseDto
    {
        public string Token { get; set; } = null!;
        //public string Email { get; set; } = null!;
        //public string Role { get; set; } = null!;
    }
}