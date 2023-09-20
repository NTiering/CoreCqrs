using Core.Data.Datamodels;

namespace Core.Queries.Queries.Widgets;
public class GetWidgetResult : BaseQueryResult
{
    public Widget? Widget { get; set; }
}
