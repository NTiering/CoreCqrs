
namespace Core.Ext
{
    public static class IServicesExt
    {
        public static IServiceCollection AddStartupServives(this IServiceCollection services, IEnumerable<Type> types)
        {

            var items = types
                .Where(x => x.IsAssignableTo(typeof(Sidecar.IStartup)))
                .Select(x => Activator.CreateInstance(x))
                .Cast<Sidecar.IStartup>()
                .ToList();
            
            Console.WriteLine($"{nameof(AddStartupServives)} found {items.Count} items");

            items.ForEach(x => x.Main(services));

            return services;
        }
    }
}
