using Berkay.ECommerceCase.Api.Extensions;
using Berkay.ECommerceCase.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

// todo: configurations
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
var demoUser = builder.Configuration.GetSection("DemoUser").Get<DemoUserRegistration>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionSqlite");

// Add services to the container.

builder.Services.AddDatabaseConfiguration(connectionString);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddIdentityConfiguration();
builder.Services.AddJwtConfiguration(jwtSettings);
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Initialize(demoUser);

app.Run();

