namespace Core.Commands.Support;

public class CommandCompletedEvent<Tr> : Event<Tr>
{
    public CommandCompletedEvent(Tr payload)
        : base(payload)
    {
    }
}


