using Core.Commands.Commands.Widgets;
using Core.Queries.Queries.ApiConsume;
using Core.Queries.Queries.Auth;
using Core.Queries.Queries.Widgets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Core;

public static class Endpoints
{
    public static class Tags
    {
        public static string Demo => nameof(Demo);
        public static string Auth => nameof(Auth);
    }

    public static void AddEndpoints(this WebApplication app, bool isDevelopment, ConfigurationManager configuration, string enviromentName)
    {
        AddQueries(app, isDevelopment, configuration, enviromentName);
        AddCommands(app, isDevelopment, configuration, enviromentName);
        AddAuth(app, isDevelopment, configuration, enviromentName);
    }

    private static void AddAuth(WebApplication app, bool isDevelopment, ConfigurationManager configuration, string enviromentName)
    {
        app.MapPost("/auth/token", async (HttpContext httpContext, IMediator mediator, [FromBody] string username, string password) =>
        {
            var result = await mediator.Send(new GetUserQuery { Password = password, Username = username });
            var rtn = result.Format();
            return rtn;
        })
            .WithName("AuthToken")
            .WithTags(Tags.Auth)
            .WithOpenApi();
    }

    private static void AddCommands(WebApplication app, bool isDevelopment, ConfigurationManager configuration, string enviromentName)
    {
        if (isDevelopment)
        {
            app.MapPost("/Widgets", async (HttpContext httpContext, IMediator mediator, [FromBody] string message) =>
            {
                var result = await mediator.Send(new AddWidgetCommand(message));
                var rtn = result.Format(httpContext.Request.Method);
                return rtn;
            })
            .WithName("CoreAddWidget")
            .WithTags(Tags.Demo)
            .WithOpenApi();
        }
    }

    private static void AddQueries(WebApplication app, bool isDevelopment, ConfigurationManager configuration, string enviromentName)
    {
        if (isDevelopment)
        {
            app.MapGet("/Widgets", async (HttpContext httpContext, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetWidgetQuery());
                var rtn = result.Format();
                return rtn;
            })
            .WithName("CoreGetWidget")
            .WithTags(Tags.Demo)
            .WithOpenApi();

            app.MapGet("/ApiConsume", async (HttpContext httpContext, IMediator mediator) =>
            {
                var result = await mediator.Send(new ApiConsumeQuery());
                var rtn = result.Format();
                return rtn;
            })
            .WithName("ApiConsume")
            .WithTags(Tags.Demo)
            .WithOpenApi();
        }

    }
}
