using Berkay.ECommerceCase.Application.Configurations;
using Berkay.ECommerceCase.Application.Services;
using Berkay.ECommerceCase.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berkay.ECommerceCase.Persistance
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly ECommerceDbContext _db;

        public DatabaseSeeder(UserManager<User> userManager, ECommerceDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public void Initialize(DemoUserData demoUserData)
        {
            if (_db.Database.GetPendingMigrations().Any())
            {
                _db.Database.Migrate();
            }
            if (!_db.Users.Any(u => u.Email == demoUserData.Email))
            { 
                AddDemoUser(demoUserData);
                _db.SaveChanges();
            }
            if (!_db.Categories.Any())
            {
                SeedCategories();
                _db.SaveChanges();
                if (!_db.Products.Any())
                {
                    SeedProducts();
                    _db.SaveChanges();
                    if (!_db.Carts.Any())
                    {
                        SeedCarts();
                    }
                }
            }
            _db.SaveChanges();
            if (!_db.CartItems.Any())
            {
                SeedCartItems();
                _db.SaveChanges();
            }
            if (!_db.DiscountByCarts.Any())
            {
                SeedDiscountByCart();
                _db.SaveChanges();
            }
            if (!_db.DiscountByCategories.Any())
            {
                SeedDiscountByCategory();
                _db.SaveChanges();
            }
        }

        private void SeedDiscountByCart()
        {
            var discounts = new List<DiscountByCart>
            {
                new DiscountByCart { MinTotalPrice = 1000m, Percentage = 0.1m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30) },
            };

            _db.DiscountByCarts.AddRange(discounts);
        }

        private void SeedDiscountByCategory()
        {
            var discounts = new List<DiscountByCategory>
            {
                new DiscountByCategory { CategoryId = 1, MinQuantity = 2, Percentage = 0.20m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30) },
                new DiscountByCategory { CategoryId = 2, MinQuantity = 6, Percentage = 1.00m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30) },
            };

            _db.DiscountByCategories.AddRange(discounts);
        }


        private void SeedCartItems()
        {
            var carts = _db.Carts.ToList();
            var products = _db.Products.ToList();
            var cartItems = new List<CartItem>
            {
                new CartItem
                {
                    ProductId = products[0].Id, 
                    Product = products[0], 
                    CartId = carts[0].Id, 
                    Cart = carts[0], 
                    Quantity = 2
                },
                new CartItem
                {
                    ProductId = products[1].Id,
                    Product = products[1],
                    CartId = carts[0].Id,
                    Cart = carts[0],
                    Quantity = 6
                },
                new CartItem
                {
                    ProductId = products[2].Id,
                    Product = products[2],
                    CartId = carts[0].Id,
                    Cart = carts[0],
                    Quantity = 5
                },
                new CartItem
                {
                    ProductId = products[3].Id,
                    Product = products[3],
                    CartId = carts[0].Id,
                    Cart = carts[0],
                    Quantity = 2
                },
                new CartItem
                {
                    ProductId = products[4].Id,
                    Product = products[4],
                    CartId = carts[0].Id,
                    Cart = carts[0],
                    Quantity = 6
                },
                new CartItem
                {
                    ProductId = products[5].Id,
                    Product = products[5],
                    CartId = carts[0].Id,
                    Cart = carts[0],
                    Quantity = 5
                }
            };
            foreach (var cartItem in cartItems)
            {
                _db.CartItems.Add(cartItem);
            }
        }

        private void SeedCarts()
        {
            var users = _db.Users.ToList();
            var carts = new List<Cart>
            {
                new Cart{ UserId=users[0].Id }
            };
            _db.Carts.AddRange(carts);
        }

        private void AddDemoUser(DemoUserData demoUserData)
        {
            Task.Run(async () =>
            {
                var demoUser = new User
                {
                    FirstName = demoUserData.FirstName,
                    LastName = demoUserData.LastName,
                    Email = demoUserData.Email,
                    UserName = demoUserData.UserName,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                var demoUserInDb = await _userManager.FindByEmailAsync(demoUser.Email);
                if (demoUserInDb == null)
                {
                    await _userManager.CreateAsync(demoUser, demoUserData.Password);
                }
            }).GetAwaiter().GetResult(); ;
        }

        private void SeedProducts()
        {
            var categories = _db.Categories.ToList();
            var products = new List<Product>
            {
                new Product { Name = "Product 1", Category = categories[0], Price = 100.99m, Stock = 10 },
                new Product { Name = "Product 2", Category = categories[0], Price = 190.99m, Stock = 100 },
                new Product { Name = "Product 3", Category = categories[0], Price = 190.99m, Stock = 100 },
                new Product { Name = "Product 4", Category = categories[1], Price = 190.99m, Stock = 100 },
                new Product { Name = "Product 5", Category = categories[1], Price = 50.99m, Stock = 30 },
                new Product { Name = "Product 6", Category = categories[1], Price = 100.99m, Stock = 50 },
                new Product { Name = "Product 7", Category = categories[1], Price = 190.99m, Stock = 100 },
                new Product { Name = "Product 8", Category = categories[0], Price = 190.99m, Stock = 100 },
                new Product { Name = "Product 9", Category = categories[1], Price = 190.99m, Stock = 100 },
                new Product { Name = "Product 10", Category = categories[2], Price = 50.99m, Stock = 30 }
                // Diğer ürün örneklerini buraya ekleyebilirsiniz
            };

            _db.Products.AddRange(products);

        }

        private void SeedCategories()
        {
            var categories = new List<Category>
            {
                new Category { Name = "Electronics", Description="Electronics" },
                new Category { Name = "Clothing", Description="Clothing"  },
                new Category { Name = "Books", Description="Books"  }
                // Diğer kategori örneklerini buraya ekleyebilirsiniz
            };

            _db.Categories.AddRange(categories);
        }

    }
}
