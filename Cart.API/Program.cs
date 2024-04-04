using MassTransit;
using Cart.API.Infrastructure;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using JWTAuthManager;
using Cart.API.Infrastructure.features;
using Serilog.Events;
using Serilog;
using ExceptionManager.Middleware;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .CreateLogger();

Log.Logger.Information("CartAPI");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddServiceDiscovery(o => o.UseEureka());

builder.Services.AddControllers();

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddSingleton<CartData>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddCustomJwtAuth();

builder.Services.AddMassTransit(busConfg =>
{
    busConfg.SetKebabCaseEndpointNameFormatter();

    busConfg.AddConsumer<CheckCartConsumer>();

    busConfg.UsingRabbitMq((context, confg) =>
    {
        confg.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), h =>
        {
            h.Username(builder.Configuration["MessageBroker:Username"]);
            h.Password(builder.Configuration["MessageBroker:Password"]);
        });
        confg.ConfigureEndpoints(context);
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.ConfigureExceptionMiddleware();

app.MapControllers();

app.Run();
