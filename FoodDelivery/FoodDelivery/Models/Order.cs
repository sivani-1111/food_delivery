using System;
using System.Collections.Generic;

namespace FoodDelivery.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public int? RestaurantId { get; set; }

    public string? Status { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Delivery? Delivery { get; set; }

    public virtual Payment? Payment { get; set; }

    public virtual Restaurant? Restaurant { get; set; }


}
