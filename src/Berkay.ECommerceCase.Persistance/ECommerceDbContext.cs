using Berkay.ECommerceCase.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Berkay.ECommerceCase.Persistance
{
    public class ECommerceDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ECommerceDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}