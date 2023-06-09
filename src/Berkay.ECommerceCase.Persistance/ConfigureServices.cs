using Berkay.ECommerceCase.Application;
using Berkay.ECommerceCase.Application.Services;
using Berkay.ECommerceCase.Persistance.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berkay.ECommerceCase.Persistance
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPersistanceLayerServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, IdentityService>();
            services.AddScoped<IECommerceDbContext>(provider => provider.GetRequiredService<ECommerceDbContext>());
            return services;
        }
    }
}
