using SalesTaxCalculator.Common.Interfaces;

namespace SalesTaxCalculator.Services;

public sealed class ApiConfigurator : IApiConfigurator
{
    public string GetApiUrl() => "https://api.taxjar.com";
    public string GetApiKey() => "5da2f821eee4035db4771edab942a4cc";
}