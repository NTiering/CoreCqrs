using Core.Data.Support;
using Microsoft.Extensions.Logging;

namespace Core.Commands.Support;

/// <summary>
/// Executes the operation on the values in the command
/// </summary>
public abstract class BaseCommandHandler<Tcmd, Tr> : IRequestHandler<Tcmd, Tr>
    where Tcmd : ICommand<Tr>
    where Tr : ICommandResult, new()
{
    private readonly IValidator<Tcmd> validator;
    private readonly ILogger<BaseCommandHandler<Tcmd, Tr>> logger;
    private readonly IMediator mediator;

    public BaseCommandHandler(IValidator<Tcmd> validator, ILogger<BaseCommandHandler<Tcmd, Tr>> logger, IMediator mediator)
    {
        this.validator = validator;
        this.logger = logger;
        this.mediator = mediator;
    }
    public async Task<Tr> Handle(Tcmd command, CancellationToken cancellationToken)
    {
        var dl = new DurationLogger<Tcmd>(logger);
        var result = await ValidateCommand(command, cancellationToken);
        if (result.Success)
        {
            await HandleCommand(command, result, cancellationToken);
            await RaiseCommandCompletedEvent(result, cancellationToken);
        }

        result.DurationInMs = dl.StopAndRead();       
        return result;
    }

    private async Task RaiseCommandCompletedEvent(Tr result, CancellationToken cancellationToken)
    {
        var e = new CommandCompletedEvent<Tr>(result);
        await mediator.Publish(e, cancellationToken);
    }

    private async Task<Tr> ValidateCommand(Tcmd request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        Tr result = new();
        result.SetErrors(validationResult.Errors.Select(x => x.ErrorMessage));
        return result;
    }

    protected abstract Task HandleCommand(Tcmd request, Tr result, CancellationToken cancellationToken);
}
