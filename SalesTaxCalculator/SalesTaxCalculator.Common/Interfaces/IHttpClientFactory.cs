namespace SalesTaxCalculator.Common.Interfaces;

public interface IHttpClientFactory
{
    HttpClient CreateHttpClient(string baseUrl, bool requiresAuthorisation);
}