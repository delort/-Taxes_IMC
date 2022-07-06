namespace SalesTaxCalculator.Common.Interfaces;

public interface IDialogService
{
    Task ShowAlert(string title);
    Task ShowAlert(string title, string message);
    Task ShowAlert(string title, string message, string okButtonText);
}