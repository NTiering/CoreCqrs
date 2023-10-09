using Core.Ext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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

        var securityScheme = new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JSON Web Token based security",
        };
        var securityReq = new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        };

        builder.Services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.ToString().Replace(" ", string.Empty).Replace("+", string.Empty));
            options.AddSecurityDefinition("Bearer", securityScheme);
            options.AddSecurityRequirement(securityReq);

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
            configuration: builder.Configuration,
            isDevelopment: app.Environment.IsDevelopment(),
            enviromentName: app.Environment.EnvironmentName);

        app.Run();
    }
}