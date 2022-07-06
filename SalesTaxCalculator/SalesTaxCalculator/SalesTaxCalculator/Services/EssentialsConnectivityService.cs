using System;
using SalesTaxCalculator.Common.Interfaces;
using Xamarin.Essentials;

namespace SalesTaxCalculator.Services;

public class EssentialsConnectivityService : IConnectivityService
{
    public event EventHandler<bool> InternetConnectionChanged;

    public EssentialsConnectivityService()
    {
        Connectivity.ConnectivityChanged += ConnectivityOnConnectivityChanged;
    }

    public bool HasInternetConnection()
    {
        return Connectivity.NetworkAccess == NetworkAccess.Internet;
    }

    private void ConnectivityOnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        InternetConnectionChanged?.Invoke(this, e.NetworkAccess == NetworkAccess.Internet);
    }
}