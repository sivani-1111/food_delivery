using System;
using System.Collections.Generic;

namespace FoodDelivery.Models;

public partial class MenuItem
{
    public int ItemId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? RestaurantId { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Restaurant? Restaurant { get; set; }
}
