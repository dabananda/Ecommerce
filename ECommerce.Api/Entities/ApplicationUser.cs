using Microsoft.AspNetCore.Identity;

namespace ECommerce.Api.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
    }
}
