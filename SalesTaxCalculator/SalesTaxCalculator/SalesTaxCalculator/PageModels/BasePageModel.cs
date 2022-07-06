using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SalesTaxCalculator.Common.Interfaces;

namespace SalesTaxCalculator.PageModels;

public class BasePageModel : MvxViewModel
{
    protected IMvxNavigationService NavigationService { get; }
    protected ILogger Logger { get; }
    protected IConnectivityService ConnectivityService { get; }

    public BasePageModel(
        IMvxNavigationService navigationService, 
        ILogger logger,
        IConnectivityService connectivityService)
    {
        NavigationService = navigationService;
        Logger = logger;
        ConnectivityService = connectivityService;
    }
}