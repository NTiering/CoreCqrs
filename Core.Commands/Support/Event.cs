using MediatR;

namespace Core.Commands.Support;

public abstract class Event<Tr> : INotification
{
    public Event(Tr payload)
    {
        Payload = payload;
        RasiedOn = DateTime.UtcNow;
    }

    public Tr Payload { get; }
    public DateTime RasiedOn { get; }
}
