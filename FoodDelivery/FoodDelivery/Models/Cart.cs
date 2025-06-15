using System;
using System.Collections.Generic;

namespace FoodDelivery.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int? CustomerId { get; set; }

    public int? ItemId { get; set; }

    public int? Quantity { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual MenuItem? Item { get; set; }
}
