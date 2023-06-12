using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using secureFunctions.Middleware;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(builder =>
    {
        builder.UseMiddleware<AuthenticationMiddleware>();
        builder.UseMiddleware<AuthorizationMiddleware>();
    })
    .ConfigureAppConfiguration(builder =>
        {
            builder
                .AddJsonFile("local.settings.json", true, true)
                .AddEnvironmentVariables();
        }
    )
    .Build();
host.Run();