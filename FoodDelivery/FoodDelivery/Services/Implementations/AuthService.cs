using FoodDelivery.DTOs;
using FoodDelivery.Models;
using FoodDelivery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FoodDelivery.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly FoodDeliverySystemContext _context; private readonly IConfiguration _config;

        public AuthService(FoodDeliverySystemContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var role = dto.Role?.Trim();
            if (role != "Customer" && role != "RestaurantOwner")
                role = "Customer";

            if (role == "Customer")
            {
                var customer = new Customer
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Address = dto.Address,
                    PasswordHash = HashPassword(dto.Password),
                    Role = role
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return new AuthResponseDto
                {
                    //Email = customer.Email ?? string.Empty,
                    //Role = customer.Role,
                    Token = GenerateToken(customer.Email ?? string.Empty, customer.Name ?? string.Empty, customer.Role ?? "Customer")
                };
            }
            else
            {
                var restaurant = new Restaurant
                {
                    Name = dto.Name,
                    ContactEmail = dto.Email,
                    Phone = dto.Phone,
                    Address = dto.Address,
                    PasswordHash = HashPassword(dto.Password),
                    Role = role
                };

                _context.Restaurants.Add(restaurant);
                await _context.SaveChangesAsync();

                return new AuthResponseDto
                {
                    //Email = restaurant.ContactEmail ?? string.Empty,
                    //Role = restaurant.Role,
                    Token = GenerateToken(restaurant.ContactEmail ?? string.Empty, restaurant.Name ?? string.Empty, restaurant.Role ?? "RestaurantOwner")
                };
            }
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var role = dto.Role?.Trim();

            if (role == "Customer")
            {
                var user = await _context.Customers.FirstOrDefaultAsync(u => u.Email == dto.Email);
                if (user == null || string.IsNullOrEmpty(user.PasswordHash) || !VerifyPassword(dto.Password, user.PasswordHash))
                    return null;

                return new AuthResponseDto
                {
                    //Email = user.Email ?? string.Empty,
                    //Role = user.Role,
                    Token = GenerateToken(user.Email ?? string.Empty, user.Name ?? string.Empty, user.Role ?? "Customer")
                };
            }
            else if (role == "RestaurantOwner")
            {
                var user = await _context.Restaurants.FirstOrDefaultAsync(u => u.ContactEmail == dto.Email);
                if (user == null || string.IsNullOrEmpty(user.PasswordHash) || !VerifyPassword(dto.Password, user.PasswordHash))
                    return null;

                return new AuthResponseDto
                {
                    //Email = user.ContactEmail ?? string.Empty,
                    //Role = user.Role,
                    Token = GenerateToken(user.ContactEmail ?? string.Empty, user.Name ?? string.Empty, user.Role ?? "RestaurantOwner")
                };
            }

            return null;
        }

        private string GenerateToken(string email, string name, string role)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Role, role)
        };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["DurationInMinutes"]!)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private static bool VerifyPassword(string password, string hash)
        {
            var inputHash = HashPassword(password);
            return inputHash == hash;
        }
    }

}



