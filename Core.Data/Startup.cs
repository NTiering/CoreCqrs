using Core.Sidecar;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Core.Data;

public class Startup : IStartup
{
    public void Main(IApplicationBuilder app)
    {

    }

    public void Main(IServiceCollection services)
    {
        services.Scan(scan =>
        scan.FromAssembliesOf(typeof(Startup))
            .AddClasses()
            .AsMatchingInterface());
    }
}
