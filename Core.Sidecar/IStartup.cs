using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Core.Sidecar;

public interface IStartup
{
    void Main(IApplicationBuilder app);
    void Main(IServiceCollection services, IConfigurationSection configuration);
}