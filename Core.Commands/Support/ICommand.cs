using MediatR;

namespace Core.Commands.Support;


public interface ICommand<TResult> : IRequest<TResult>
where TResult : ICommandResult
{ }



