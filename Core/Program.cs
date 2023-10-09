using Core.Events;
using Core.Ext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static Poly.Net.Http.Server;

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
    
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.ToString().Replace(" ",string.Empty).Replace("+",string.Empty));
        });
        builder.Services.AddLogging();

        builder.Services.AddStartupServives(startUpCollection, builder.Configuration);

        builder.Services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });
        builder.Services.AddAuthorization();

        var app = builder.Build();
        app.UseAuthentication();
        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.AddStartupServives(startUpCollection);    

        app.AddEndpoints(
            configuration : builder.Configuration,
            isDevelopment : app.Environment.IsDevelopment(),  
            enviromentName : app.Environment.EnvironmentName); 

        app.Run();
    }    
}