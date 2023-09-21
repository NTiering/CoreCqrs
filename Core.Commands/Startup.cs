using Core.Sidecar;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Commands;

public class Startup : IStartup
{
    public void Main(IApplicationBuilder app)
    {

    }

    public void Main(IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        services.AddValidatorsFromAssemblyContaining(typeof(Startup));
    }
}
