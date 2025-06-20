using System.ComponentModel.DataAnnotations;

public class MenuItemDto 
{ 
    public string Name { get; set; } = null!; 
    public string Description { get; set; } = null!; 
    public decimal Price { get; set; } 
    public string RestaurantName { get; set; } = null!; }

public class CreateMenuItemDto 
{
    [Required, StringLength(100)]
    public string Name { get; set; } = null!; 

    [Required, StringLength(500)]
    public string Description { get; set; } = null!; 

    [Required, Range(1,100)]
    public decimal Price { get; set; } 
}

public class UpdateMenuItemDto 
{ 
    public string Name { get; set; } = null!; 
    public string Description { get; set; } = null!; 
    public decimal Price { get; set; } 
}
