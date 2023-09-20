using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Core.Data;

public static class Startup
{
    public static void Main(IApplicationBuilder app)
    {

    }

    public static void Main(IServiceCollection services)
    {
        services.Scan(scan =>
        scan.FromAssembliesOf(typeof(Startup))
            .AddClasses()
            .AsMatchingInterface());
    }
}
