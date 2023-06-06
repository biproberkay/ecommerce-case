using Berkay.ECommerceCase.Application.Configurations;
using Berkay.ECommerceCase.Application.Services;
using Berkay.ECommerceCase.Domain.Entities;
using Microsoft.AspNetCore.Identity;
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

        public void Initialize(DemoUser demoUserRegistration)
        {
            AddDemoUser(demoUserRegistration);
            _db.SaveChanges();
        }

        private void AddDemoUser(DemoUser demoUserRegistration)
        {
            Task.Run(async () =>
            {
                var demoUser = new User
                {
                    FirstName = demoUserRegistration.FirstName,
                    LastName = demoUserRegistration.LastName,
                    Email = demoUserRegistration.Email,
                    UserName = demoUserRegistration.UserName,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                var demoUserInDb = await _userManager.FindByEmailAsync(demoUser.Email);
                if (demoUserInDb == null)
                {
                    await _userManager.CreateAsync(demoUser, demoUserRegistration.Password);
                }
            }).GetAwaiter().GetResult(); ;
        }
    }
}
