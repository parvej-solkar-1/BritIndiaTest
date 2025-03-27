using System.ComponentModel.DataAnnotations;

namespace ProductServices.DTOs.Product;

public class ProductDto
{
    public int? Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string ProductName { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

}
