using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using SalesTaxCalculator.Common.Interfaces;
using Xamarin.Forms;

namespace SalesTaxCalculator.PageModels;

public class MainPageModel : BasePageModel
{
    private bool _initialized;

    public MainPageModel(
        IConnectivityService connectivityService,
        IMvxNavigationService navigationService, 
        ILogger logger) 
        : base(navigationService, logger, connectivityService)
    {
    }
    
    public override void ViewAppeared()
    {
        if (_initialized) return;
        _initialized = true;
            
        Device.BeginInvokeOnMainThread(async () =>
        { 
            var tasks = new List<Task>
            {
                NavigationService.Navigate<TaxCalculatorPageModel>(),
                NavigationService.Navigate<RateCalculatorPageModel>(),
            };

            await Task.WhenAll(tasks);
        });
    }
    
    //todo delete it after use in proper page models
    // public override async void ViewAppeared()
    // {
    //         var rates = await _ratesApi.GetRates("90404", new()
    //         {
    //             Country = "US",
    //             City = "Santa Monica"
    //         });
    //
    //         var taxes = await _taxesApi.CalculateTaxes(new()
    //         {
    //             FromCountry = "US" ,
    //             FromZip = "07001",
    //             FromState = "NJ",
    //             ToCountry = "US",
    //             ToZip = "07446",
    //             ToState = "NJ",
    //             Amount = 16.5,
    //             Shipping = 1.5,
    //             LineItems = new List<LineRequest>
    //             {
    //                 new()
    //                 {
    //                     Quantity = 1,
    //                     UnitPrice = 15.0,
    //                     ProductTaxCode = "31000"
    //                 }
    //             }
    //         });
    // }
}