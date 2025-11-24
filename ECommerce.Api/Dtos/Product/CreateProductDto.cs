using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.Dtos.Product
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0.01, 100000)]
        public decimal Price { get; set; }

        [Url]
        public string? ImageUrl { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
    }
}
