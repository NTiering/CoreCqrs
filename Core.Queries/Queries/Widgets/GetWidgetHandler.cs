using Core.Data.Datamodels;
using Microsoft.Extensions.Logging;

namespace Core.Queries.Queries.Widgets;

public class GetWidgetHandler : BaseQueryHandler<GetWidgetQuery, GetWidgetResult>
{
    public GetWidgetHandler(ILogger<GetWidgetQuery> logger)
        : base(logger)
    {

    }

    protected override Task HandleQuery(GetWidgetQuery query, GetWidgetResult result, CancellationToken cancellationToken)
    {
        result.Widget = new Widget { Name = "New Widget" };
        return Task.CompletedTask;
    }
}
