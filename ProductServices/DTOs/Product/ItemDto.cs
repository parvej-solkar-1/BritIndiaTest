using System.ComponentModel.DataAnnotations;

namespace ProductServices.DTOs.Product;

public class ItemDto
{
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }

    //public virtual ProductDto Product { get; set; } = null!;
}
