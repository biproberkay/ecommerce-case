using Berkay.ECommerceCase.Persistance;
using Berkay.ECommerceCase.Application;
using Berkay.ECommerceCase.Api.Extensions;
using Berkay.ECommerceCase.Application.Configurations;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// todo: configurations
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<DemoUserData>(builder.Configuration.GetSection("DemoUserData"));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionSqlite");

// Add services to the container.

builder.Services.AddDatabaseConfiguration(connectionString);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddIdentityConfiguration();
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddJwtConfiguration(jwtSettings );
builder.Services.AddApplicationLayerServices();
builder.Services.AddPersistanceLayerServices();
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

var demoUser = builder.Configuration.Get<DemoUserData>();
app.UseInitializer(demoUser);

app.Run();

