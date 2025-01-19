using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Entities.Entities
{
    public class IdentityServiceDbContext : IdentityDbContext<User>
    {
        public IdentityServiceDbContext(DbContextOptions<IdentityServiceDbContext> options) : base(options) {}
    }
}
