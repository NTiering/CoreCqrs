using Core.Data.HttpClients;
using Core.Sidecar;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Core.Data.Support.HttpClientPolicyFactory;

namespace Core.Data;

public class Startup : IStartup
{
    public void Main(IApplicationBuilder app)
    {

    }


    public void Main(IServiceCollection services, IConfigurationSection configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));

        services.AddHttpClient<IQuoteClient, QuoteClient>()
            .AddPolicyHandler(RetryPolicy());

        services.Configure<Configuration>(configuration);

        services.Scan(scan =>
        scan.FromAssembliesOf(typeof(Startup))
            .AddClasses()
            .AsMatchingInterface());
    }
}
