using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Eureka;
using Ocelot.Provider.Polly;
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

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.

var routes = "Routes";

builder.Configuration.AddOcelotWithSwaggerSupport(options =>
{
    options.Folder = routes;
});


builder.Services.AddOcelot(builder.Configuration).AddEureka().AddPolly();

builder.Services.AddSwaggerForOcelot(builder.Configuration);


builder.Services.AddServiceDiscovery(o => o.UseEureka());

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
}




app.UseAuthentication();

app.UseAuthorization();

app.UseOcelot().Wait();

app.ConfigureExceptionMiddleware();

app.MapControllers();

app.Run();
