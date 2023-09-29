using Polly;
using Polly.Extensions.Http;

namespace Core.Data.Support;

public static class HttpClientPolicyFactory
{

    public static IAsyncPolicy<HttpResponseMessage> RetryPolicy(int retryAttempts = 5)
    {
        return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .WaitAndRetryAsync(retryAttempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                 retryAttempt)));
    }
}
