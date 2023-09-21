
namespace Core.Ext
{
    public static class IWebApplicationExt
    {
        public static WebApplication AddStartupServives(this WebApplication app, IEnumerable<Type> types)
        {

            var items = types
                .Where(x => x.IsAssignableTo(typeof(Sidecar.IStartup)))
                .Select(x => Activator.CreateInstance(x))
                .Cast<Sidecar.IStartup>()
                .ToList();

            Console.WriteLine($"{nameof(AddStartupServives)} found {items.Count} items");

            items.ForEach(x => x.Main(app));
            return app;
        }
    }
}
