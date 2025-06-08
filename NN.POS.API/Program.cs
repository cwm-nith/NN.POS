using NLog;
using NLog.Web;
using NN.POS.API.App;
using NN.POS.API.Core;
using NN.POS.API.Infra;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    logger.Info("Application is starting");

    var builder = WebApplication.CreateBuilder(args);

    // Ensure Kestrel is configured to listen on port 8090
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(8090); // Listen on port 8090
    });

    var env = builder.Environment.EnvironmentName;

    logger.Info("Application is on {Env} Environment", env);

    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Host.UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });
    
    builder.Services.AddControllers().AddNewtonsoftJson();

    builder.Services.AddInfrastructure(builder.Configuration)
        .AddRegistrationMediatR();
    var app = builder.Build();

    var settings = app.Services.GetService<AppSettings>();
    if (settings?.Swagger.IsEnable ?? false) app.UseCustomSwagger();
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseCors(i =>
            i.AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true)
        );

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseInfrastructure();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}