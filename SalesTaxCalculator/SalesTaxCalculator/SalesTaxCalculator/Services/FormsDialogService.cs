using System.Threading.Tasks;
using SalesTaxCalculator.Common.Interfaces;
using SalesTaxCalculator.Resources;
using Xamarin.Forms;

namespace SalesTaxCalculator.Services;

public class FormsDialogService : IDialogService
{
    private readonly Application _application;

    public FormsDialogService(Application application)
    {
        _application = application;
    }
    
    public Task ShowAlert(string title) => ShowAlert(title, null);

    public Task ShowAlert(string title, string message) => ShowAlert(title, message, AppResources.OkButton);

    public Task ShowAlert(string title, string message, string okButtonText) 
        => _application.MainPage.DisplayAlert(title, message, okButtonText);
}