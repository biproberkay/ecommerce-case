using Berkay.ECommerceCase.Application.Configurations;
using Berkay.ECommerceCase.Application.Services;

namespace Berkay.ECommerceCase.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        internal static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", typeof(Program).Assembly.GetName().Name);
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
            });
        }
        internal static IApplicationBuilder UseInitializer(this IApplicationBuilder app, DemoUserData demoUserRegistration)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var initializers = serviceScope.ServiceProvider.GetServices<IDatabaseSeeder>();

            foreach (var initializer in initializers)
            {
                initializer.Initialize(demoUserRegistration);
            }

            return app;
        }
    }
}
