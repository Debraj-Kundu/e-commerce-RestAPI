using Inventory.API.Infrastructure;
using MassTransit;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using JWTAuthManager;
using Inventory.API.Infrastructure.features;
using Serilog;
using Serilog.Events;
using ExceptionManager.Middleware;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddServiceDiscovery(o => o.UseEureka());

builder.Services.AddControllers();

builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();

builder.Services.AddSingleton<InventoryData>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddCustomJwtAuth();


builder.Services.AddMassTransit(busConfg =>
{
    busConfg.SetKebabCaseEndpointNameFormatter();

    busConfg.AddConsumer<CheckInventoryConsumer>();
    
    busConfg.AddConsumer<ProductAddedConsumer>();
    
    busConfg.AddConsumer<ProductUpdatedConsumer>();
    
    busConfg.AddConsumer<ProductDeletedConsumer>();

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
