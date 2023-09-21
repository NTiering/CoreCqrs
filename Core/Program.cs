using Core.Ext;

namespace Core;

public class Program
{
    public static void Main(string[] args)
    {
        var startUpCollection = new[] {
            typeof(Queries.Startup),
            typeof(Commands.Startup),
            typeof(Data.Startup),
            typeof(Events.Startup),
        };

        var builder = WebApplication.CreateBuilder(args);
  
        builder.Services.AddAuthorization();          

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.ToString().Replace(" ",string.Empty).Replace("+",string.Empty));
        });
        builder.Services.AddLogging();

        builder.Services.AddStartupServives(startUpCollection);
        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
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
}