using ECommerce.Api.Data;
using ECommerce.Api.Entities;
using ECommerce.Api.Helpers;
using ECommerce.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _context;

        public ProductRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PagedList<Product>> GetAllAsync(ProductParams productParams)
        {
            var query = _context.Products.AsQueryable();

            // filter by search term
            if (!string.IsNullOrEmpty(productParams.SearchTerm))
            {
                query = query.Where(p => p.Name.Contains(productParams.SearchTerm) || (p.Description != null && p.Description.Contains(productParams.SearchTerm)));
            }

            // sorting
            query = productParams.OrderBy switch
            {
                "priceAsc" => query.OrderBy(p => p.Price),
                "pricedesc" => query.OrderByDescending(p => p.Price),
                "nameDesc" => query.OrderBy(p => p.Name),
                _ => query.OrderBy(p => p.Name)
            };

            // pagination
            return await PagedList<Product>.CreateAsync(query, productParams.PageNumber, productParams.PageSize);
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
