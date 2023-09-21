using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Sidecar
{
    public interface IStartup
    {
        void Main(IApplicationBuilder app);
        void Main(IServiceCollection services);
    }
}