namespace Core.Commands.Support;

/// <summary>
/// Command used to pass in values 
/// </summary>
public abstract record BaseCommand<TResult> : ICommand<TResult>
   where TResult : ICommandResult
{
  
}



