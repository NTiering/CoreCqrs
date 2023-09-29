using Core.Data.Datamodels;
using Core.Queries.Support.HttpClients;
using Microsoft.Extensions.Logging;

namespace Core.Queries.Queries.ApiConsume;

public class ApiConsumeHandler : BaseQueryHandler<ApiConsumeQuery, ApiConsumeResult>
{
     private readonly IQuoteClient quoteClient;

    public ApiConsumeHandler(ILogger<ApiConsumeQuery> logger, IQuoteClient quoteClient)
        : base(logger)
    {
        this.quoteClient = quoteClient;
    }

    protected override async Task HandleQuery(ApiConsumeQuery query, ApiConsumeResult result, CancellationToken cancellationToken)
    {
        var quote = await quoteClient.GetQuote(cancellationToken);

        if (quote != null)
        { 
            result.Author = quote.Author;
            result.Quote = quote.Quote;
        }       
       
    }
}
