namespace Core.Commands.Support;

public interface ICommandResult
{
    bool Success { get; }
    long DurationInMs { get; set; }
    IEnumerable<string> Errors { get; set; }
    Guid Id { get; set; }

    void SetErrors(IEnumerable<string> errors);
}
