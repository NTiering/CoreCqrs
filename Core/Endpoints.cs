using Core.Commands.Commands.Widgets;
using Core.Queries.Queries.Widgets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Core;

public static class Endpoints
{
    public static void AddEndpoints(this WebApplication app, bool IsDevelopment, string EnviromentName)
    {
        AddQueries(app,IsDevelopment,EnviromentName);
        AddCommands(app, IsDevelopment, EnviromentName);
    }

    private static void AddCommands(WebApplication app, bool IsDevelopment, string EnviromentName)
    {
        if (IsDevelopment)
        {
            app.MapPost("/Core/AddWidget", async (HttpContext httpContext, IMediator mediator, [FromBody] string message) =>
            {
                var result = await mediator.Send(new AddWidgetCommand(message));
                var rtn = result.Format(httpContext.Request.Method);
                return rtn;
            })
             .WithName("CoreAddWidget")
            .WithOpenApi();
        }
        
    }

    private static void AddQueries(WebApplication app, bool IsDevelopment, string EnviromentName)
    {
        if (IsDevelopment)
        {
            app.MapGet("/Core/GetWidget", async (HttpContext httpContext, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetWidgetQuery());
                var rtn = result.Format();
                return rtn;
            })
             .WithName("CoreGetWidget")
            .WithOpenApi();
        }

    }
}
