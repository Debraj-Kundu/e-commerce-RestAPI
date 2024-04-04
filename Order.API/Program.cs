using MassTransit;
using Order.API.Infrastructure;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using JWTAuthManager;
using Serilog;
using Serilog.Events;
using ExceptionManager.Middleware;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .CreateLogger();

Log.Logger.Information("OrderAPI");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddServiceDiscovery(o => o.UseEureka());

builder.Services.AddControllers();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<OrderData>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddCustomJwtAuth();

builder.Services.AddMassTransit(busConfg =>
{
    busConfg.SetKebabCaseEndpointNameFormatter();

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

//builder.Services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
//builder.Services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());

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
