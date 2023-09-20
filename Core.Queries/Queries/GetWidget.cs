using Microsoft.Extensions.Logging;

namespace Core.Queries.Queries;

public  class GetWidget
{
    public class Query : BaseQuery<Result>{ }
    public class Result : BaseQueryResult
    {
        public Widget? Widget { get; set; }
    }

    public class Handler : BaseQueryHandler<Query, Result>
    {
        public Handler(ILogger<Query> logger)
            :base(logger) 
        {
            
        }

        protected override Task HandleQuery(Query query, Result result, CancellationToken cancellationToken)
        {
            result.Widget = new Widget { Name = "New Widget" };
            return Task.CompletedTask;
        }
    }
}
