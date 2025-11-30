using AutoMapper;
using ECommerce.Api.Dtos.Product;
using ECommerce.Api.Entities;
using ECommerce.Api.Helpers;
using ECommerce.Api.Repositories.Interfaces;
using ECommerce.Api.Services.Interfaces;

namespace ECommerce.Api.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            await _repo.AddAsync(product);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<PagedList<ProductDto>> GetAllProductsAsync(ProductParams productParams)
        {
            var pagedProducts = await _repo.GetAllAsync(productParams);

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(pagedProducts);

            return new PagedList<ProductDto>(
                productDtos.ToList(),
                pagedProducts.TotalCount,
                pagedProducts.CurrentPage,
                pagedProducts.PageSize
            );
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _repo.GetByIdAsync(id);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateProductAsync(Guid id, CreateProductDto dto)
        {
            var existingProduct = await _repo.GetByIdAsync(id);
            if (existingProduct == null) return;
            _mapper.Map(dto, existingProduct);
            await _repo.UpdateAsync(existingProduct);
        }
    }
}
