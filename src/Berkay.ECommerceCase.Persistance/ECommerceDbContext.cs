using Berkay.ECommerceCase.Domain.Contracts;
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

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<DiscountByCart> DiscountByCarts { get; set; }
        public DbSet<DiscountByCategory> DiscountByCategories { get; set; }
    }
}