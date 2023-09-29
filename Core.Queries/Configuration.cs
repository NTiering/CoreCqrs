using Microsoft.Extensions.Options;

namespace Core.Queries;

public class Configuration
{
    public string QuoteApiUrl { get; set; } = string.Empty;
}
