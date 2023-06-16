using Berkay.ECommerceCase.Persistance;
using Berkay.ECommerceCase.Application;
using Berkay.ECommerceCase.Api.Extensions;
using Berkay.ECommerceCase.Application.Configurations;
using Microsoft.Extensions.Options;
using Berkay.ECommerceCase.Application.Services;
using Berkay.ECommerceCase.Api.Services;
using Berkay.ECommerceCase.Api.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// todo: configurations
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<DemoUserData>(builder.Configuration.GetSection("DemoUserData"));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionSqlite");

// The initial "bootstrap" logger is able to log errors during start-up. It's completely replaced by the
// logger configured in `UseSerilog()` below, once configuration and dependency-injection have both been
// set up successfully.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("..\\Logs\\log.txt",rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information()
    .CreateLogger();

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
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddSwaggerConfiguration();
builder.Logging.AddSerilog();
builder.Host.UseSerilog();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

var demoUser = builder.Configuration.Get<DemoUserData>();
app.UseInitializer(demoUser);
try
{
    Log.Information("Starting up!");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The Application Failed to start");
	throw;
}
finally
{
    Log.CloseAndFlush();
}

