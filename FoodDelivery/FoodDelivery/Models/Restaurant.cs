using System;
using System.Collections.Generic;

namespace FoodDelivery.Models;

public partial class Restaurant
{
    public int RestaurantId { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? ContactEmail { get; set; }

    public string? Phone { get; set; }

    public string Role { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
