namespace Core.Queries.Support;

public abstract class BaseQuery<TResult> : IQuery<TResult>
    where TResult : IQueryResult
{
    public long Duration { get; set; }
}
