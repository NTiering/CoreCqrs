namespace Core.Commands.Support;

/// <summary>
/// Result used to return values to the calling domain
/// </summary>
public abstract class BaseComandResult : ICommandResult
{
    public Guid Id { get; set; }
    public long DurationInMs { get; set; }
    public bool Success => !Errors.Any();
    public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();

    public void SetErrors(IEnumerable<string> errors)
    {
        Errors = errors;
    }
}
