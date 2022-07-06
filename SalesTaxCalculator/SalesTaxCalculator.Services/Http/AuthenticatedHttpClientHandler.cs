using System.Net.Http.Headers;

namespace SalesTaxCalculator.Services.Http;

public class AuthenticatedHttpClientHandler : HttpClientHandler
{
    private readonly Func<Task<string>> _getToken;

    public AuthenticatedHttpClientHandler(Func<Task<string>> getToken)
    {
        _getToken = getToken ?? throw new ArgumentNullException(nameof(getToken));
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _getToken().ConfigureAwait(false);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}