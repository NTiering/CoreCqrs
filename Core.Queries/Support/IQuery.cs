
namespace Core.Queries.Support;

public interface IQuery 
{ 
    long Duration { get; } 
}

public interface IQuery<TResult> : IQuery, IRequest<TResult>
    where TResult : IQueryResult
{
   
}
