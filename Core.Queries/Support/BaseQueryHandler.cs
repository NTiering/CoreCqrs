using Core.Data.Support;
using Microsoft.Extensions.Logging;

namespace Core.Queries.Support;

public abstract class BaseQueryHandler<Query, Result> : IRequestHandler<Query, Result>
    where Query : IRequest<Result>
    where Result : class, IQueryResult, new()
{
    private readonly ILogger<Query> logger;

    public BaseQueryHandler(ILogger<Query> logger)
    {
        this.logger = logger;
    }
    public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
    {
        var dl = new DurationLogger<Query>(logger);
        var result = new Result();
        await HandleQuery(query, result, cancellationToken);
        result.DurationInMs = dl.StopAndRead();
        return result;
    }

    protected abstract Task HandleQuery(Query query, Result result, CancellationToken cancellationToken);
}
