using Berkay.ECommerceCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berkay.ECommerceCase.Application
{
    public interface IECommerceDbContext
    {
        DbSet<Category> Categories { get; }
        DbSet<Product> Products { get; }
        DbSet<Cart> Carts { get; }
        DbSet<CartItem> CartItems { get; }
        DbSet<DiscountByCart> DiscountByCarts { get; }
        DbSet<DiscountByCategory> DiscountByCategories { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
