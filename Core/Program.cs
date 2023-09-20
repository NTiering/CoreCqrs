namespace Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString().Replace(" ",string.Empty).Replace("+",string.Empty));
            });
            builder.Services.AddLogging();

            Queries.Startup.Main(builder.Services);
            Commands.Startup.Main(builder.Services);
            Data.Startup.Main(builder.Services);
            Events.Startup.Main(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();          

            Queries.Startup.Main(app);
            Commands.Startup.Main(app);
            Data.Startup.Main(app);
            Events.Startup.Main(app);


            app.AddEndpoints(
                IsDevelopment : app.Environment.IsDevelopment(),  
                EnviromentName : app.Environment.EnvironmentName); 

            app.Run();
        }
    }
}