using ECommerce.Api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Data
{
    public class ECommerceDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
