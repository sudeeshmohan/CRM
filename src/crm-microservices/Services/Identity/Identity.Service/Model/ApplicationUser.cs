using Microsoft.AspNetCore.Identity;

namespace Identity.Service.Model
{
    public class ApplicationUser : IdentityUser
    {
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
