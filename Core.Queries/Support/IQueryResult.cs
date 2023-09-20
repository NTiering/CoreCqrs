using MediatR;

namespace Core.Queries.Support;

public interface IQueryResult
{
    public long DurationInMs { get; set; }
}

public abstract class BaseQueryResult : IQueryResult
{
    public long DurationInMs { get; set; }
}


