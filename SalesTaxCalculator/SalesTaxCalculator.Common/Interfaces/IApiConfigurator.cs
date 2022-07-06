namespace SalesTaxCalculator.Common.Interfaces;

public interface IApiConfigurator
{
    string GetApiUrl();
    string GetApiKey();
}