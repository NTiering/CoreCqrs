using Core.Data.Datamodels;

namespace Core.Commands.Commands.Widgets;

/// <summary>
/// Result used to return values to the calling domain
/// </summary>
public class AddWidgetResult : BaseComandResult
{
    public Widget? Data { get; set; }
}
