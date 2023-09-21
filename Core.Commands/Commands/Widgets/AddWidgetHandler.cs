using Microsoft.Extensions.Logging;

namespace Core.Commands.Commands.Widgets;

/// <summary>
/// Executes the operation on the values in the command
/// </summary>
public class AddWidgetHandler : BaseCommandHandler<AddWidgetCommand, AddWidgetResult>
{
    public AddWidgetHandler(IValidator<AddWidgetCommand> validator, ILogger<AddWidgetHandler> logger, IMediator mediator)
        : base(validator, logger, mediator)
    { }

    protected override Task<AddWidgetResult> HandleCommand(AddWidgetCommand request, AddWidgetResult result, CancellationToken cancellationToken)
    {        
        result.Data = new Widget { Name = request.Name };
        return Task.FromResult(result);
    }
}