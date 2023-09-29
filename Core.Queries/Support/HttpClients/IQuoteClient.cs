namespace Core.Queries.Support.HttpClients
{
    public interface IQuoteClient
    {
        Task<QuoteRespnse?> GetQuote(CancellationToken cancellationToken);
    }
}