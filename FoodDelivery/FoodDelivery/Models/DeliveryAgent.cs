using System;
using System.Collections.Generic;

namespace FoodDelivery.Models;

public partial class DeliveryAgent
{
    public int AgentId { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public virtual Delivery? Delivery { get; set; }
}
