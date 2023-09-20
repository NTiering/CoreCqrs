using Microsoft.Extensions.Logging;

namespace Core.Commands.Commands;

public class AddWidget
{
    /// <summary>
    /// Command used to pass in values 
    /// </summary>
    /// <param name="Name"></param>
    public record Command(string Name) : BaseCommand<Result>;

    /// <summary>
    /// Result used to return values to the calling domain
    /// </summary>
    public class Result : BaseComandResult
    {
        public Widget? Data { get; set; } 
    }

    /// <summary>
    /// Ensures the correctness of the values passed in
    /// </summary>
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty().WithErrorCode(GetErrorCode("NameEmpty"));
        }

        private static string GetErrorCode(string msg)
        {
            return typeof(AddWidget).Name + "." + msg;
        }
    }

    /// <summary>
    /// Executes the operation on the values in the command
    /// </summary>
    public class Handler : BaseCommandHandler<Command, Result>
    {
        public Handler(IValidator<Command> validator, ILogger<Handler> logger, IMediator mediator)
            :base(validator, logger, mediator) 
        {}

        protected override Task<Result> HandleCommand(Command request, Result result, CancellationToken cancellationToken)
        {
            result.Data = new Widget { Name = request.Name  };
            return Task.FromResult(result);
        }
    }        
}
