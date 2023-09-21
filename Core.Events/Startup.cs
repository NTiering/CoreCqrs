using Core.Sidecar;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Events;

public class Startup : IStartup
{
    public void Main(IApplicationBuilder app)
    {

    }

    public void Main(IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        services.Scan(scan =>
        scan.FromAssembliesOf(typeof(Startup))
            .AddClasses()
            .AsMatchingInterface());
    }
}
