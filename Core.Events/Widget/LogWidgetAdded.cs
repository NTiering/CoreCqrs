using Core.Commands.Commands.Widgets;

namespace Core.Events.Widget;

public class LogWidgetAdded : BaseEventHandler<AddWidgetResult>
{
    protected override Task Handle(AddWidgetResult payload, CancellationToken cancellationToken)
    {
        Console.WriteLine(payload?.Data?.Name);
        return Task.CompletedTask;
    }
}