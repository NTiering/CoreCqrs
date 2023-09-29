namespace Core.Data.HttpClients
{
    public interface IQuoteClient
    {
        Task<QuoteRespnse?> GetQuote(CancellationToken cancellationToken);
    }
}