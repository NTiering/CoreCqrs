using Core.Sidecar;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Commands;

public class Startup : IStartup
{
    public void Main(IApplicationBuilder app)
    {

    }

    public void Main(IServiceCollection services, IConfigurationSection configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));

        services.Configure<Configuration>(configuration);

        services.AddValidatorsFromAssemblyContaining(typeof(Startup));
    }
}
