using Core.Events;
using Core.Ext;
using static Poly.Net.Http.Server;

namespace Core;

public class Program
{
    public static void Main(string[] args)
    {
        var startUpCollection = new[] {
            typeof(Startup),
            typeof(Commands.Startup),
            typeof(Data.Startup),
            typeof(Events.Startup),
        };

        var builder = WebApplication.CreateBuilder(args);
  
        builder.Services.AddAuthorization();  
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.ToString().Replace(" ",string.Empty).Replace("+",string.Empty));
        });
        builder.Services.AddLogging();

        builder.Services.AddStartupServives(startUpCollection, builder.Configuration);

       

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.AddStartupServives(startUpCollection);    

        app.AddEndpoints(
            IsDevelopment : app.Environment.IsDevelopment(),  
            EnviromentName : app.Environment.EnvironmentName); 

        app.Run();
    }

    public class StorageOptions
    {
        public String StorageConnectionString { get; set; }
        public String AccountName { get; set; }
        public String AccountKey { get; set; }
        public String DefaultEndpointsProtocol { get; set; }
        public String EndpointSuffix { get; set; }

        public StorageOptions() { }
    }
}