namespace SalesTaxCalculator.Common.Interfaces;

public interface IConnectivityService
{
    event EventHandler<bool> InternetConnectionChanged;
    bool HasInternetConnection();
}