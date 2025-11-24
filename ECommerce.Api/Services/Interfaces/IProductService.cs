using ECommerce.Api.Dtos.Product;

namespace ECommerce.Api.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(Guid id);
        Task<ProductDto> CreateProductAsync(CreateProductDto newProduct);
        Task UpdateProductAsync(Guid id, CreateProductDto updatedProduct);
        Task DeleteProductAsync(Guid id);
    }
}
