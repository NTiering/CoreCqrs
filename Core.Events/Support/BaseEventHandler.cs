using Core.Commands.Support;

namespace Core.Events.Support;

public abstract class BaseEventHandler<T> : INotificationHandler<CommandCompletedEvent<T>>
{
    public async Task Handle(CommandCompletedEvent<T> notification, CancellationToken cancellationToken)
    {
        await Handle(notification.Payload, cancellationToken);
    }

    protected abstract Task Handle(T payload, CancellationToken cancellationToken);
}