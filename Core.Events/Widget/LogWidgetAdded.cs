namespace Core.Events.Widget;

public class LogWidgetAdded : BaseEventHandler<AddWidget.Result>
{
    protected override Task Handle(AddWidget.Result payload, CancellationToken cancellationToken)
    {
        Console.WriteLine(payload?.Data?.Name);
        return Task.CompletedTask;
    }
}