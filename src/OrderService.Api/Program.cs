using OrderService.Api;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Data.Seeding;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .CreateBootstrapLogger();

try
{
    Log.Information("Engine starting up...");

    var builder = WebApplication.CreateBuilder(args);

    // Configure Serilog with full settings
    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console();
    });

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddApiServices(builder.Configuration);

    var app = builder.Build();

    app.UseApiServices();

    if (app.Environment.IsDevelopment())
    {
        await app.InitializeDatabaseAsync();
        await app.SeedDatabaseAsync();
    }

    app.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "Application startup failed!");
    throw;
}
finally
{
    Log.CloseAndFlush();
}