
using Microsoft.Extensions.Configuration;
using static Core.Program;

namespace Core.Ext
{
    public static class IServicesExt
    {
        public static IServiceCollection AddStartupServives(this IServiceCollection services, IEnumerable<Type> types, ConfigurationManager configuration)
        {

            var items = types
                .Where(x => x.IsAssignableTo(typeof(Sidecar.IStartup)))
                .Select(x => Activator.CreateInstance(x))
                .Cast<Sidecar.IStartup>()
                .Where(x => x is not null)
                .ToList();
            
            Console.WriteLine($"{nameof(AddStartupServives)} found {items.Count} items");

         

            items.ForEach(x =>
            {
                var configSection = configuration.GetRequiredSection(GetConfigSectionName(x));
                x.Main(services, configSection);
            });

            return services;
        }

        private static string GetConfigSectionName(Sidecar.IStartup x)
        {
            var rtn = x.GetType().Assembly.GetName().Name;
            if (string.IsNullOrEmpty(rtn)) throw new InvalidOperationException($"no name for {x.GetType().FullName} found");
            return rtn;
        }
    }
}
