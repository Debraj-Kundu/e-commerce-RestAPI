using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using UserAuthService.Infrastructure;
using UserAuthService.JWTAuthManager;
using JWTAuthManager;
using Serilog;
using Serilog.Events;
using ExceptionManager.Middleware;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .CreateLogger();

Log.Logger.Information("UserAuthAPI");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.

// Add or register service discovery to your application
builder.Services.AddServiceDiscovery(o => o.UseEureka());

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<UsersData>();

builder.Services.AddScoped<JwtTokenHandler>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddCustomJwtAuth();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.ConfigureExceptionMiddleware();

app.MapControllers();

app.Run();
