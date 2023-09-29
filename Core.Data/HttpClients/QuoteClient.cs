using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Core.Data.HttpClients;

public class QuoteClient : IQuoteClient
{
    private readonly HttpClient httpClient;
    private readonly Configuration configuration;

    public QuoteClient(HttpClient httpClient, IOptions<Configuration> configuration)
    {
        this.httpClient = httpClient;
        this.configuration = configuration.Value;
    }

    public async Task<QuoteRespnse?> GetQuote(CancellationToken cancellationToken)
    {
        var quote = await httpClient.GetFromJsonAsync<QuoteRespnse>(configuration.QuoteApiUrl, cancellationToken);
        return quote;
    }
}

public record QuoteRespnse(string Author, string Quote);
