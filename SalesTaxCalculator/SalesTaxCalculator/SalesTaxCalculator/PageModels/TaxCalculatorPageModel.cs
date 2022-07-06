using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using SalesTaxCalculator.Common.Interfaces;
using SalesTaxCalculator.Models;
using SalesTaxCalculator.Resources;
using SalesTaxCalculator.Validations;

namespace SalesTaxCalculator.PageModels;

public class TaxCalculatorPageModel : BasePageModel
{
    private readonly IDialogService _dialogService;
    private readonly ITaxService _taxService;
    
    private TaxDetailsModel _taxDetailsModel;

    public TaxCalculatorPageModel(
        IDialogService dialogService,
        ITaxService taxService,
        IConnectivityService connectivityService,
        IMvxNavigationService navigationService, 
        ILogger logger) 
        : base(navigationService, logger, connectivityService)
    {
        _dialogService = dialogService;
        _taxService = taxService;

        CalculateTaxesCommand = new MvxAsyncCommand(ExecuteCalculateTaxesCommand);
        
        TaxModel = new TaxModel();
        SetValidators();
    }
    
    public TaxModel TaxModel { get; }

    public TaxDetailsModel TaxDetailsModel
    {
        get => _taxDetailsModel;
        set => SetProperty(ref _taxDetailsModel, value);
    }
    
    public IMvxCommand CalculateTaxesCommand { get; }

    private async Task ExecuteCalculateTaxesCommand()
    {
        try
        {
            if (ConnectivityService.HasInternetConnection() is false)
            {
                await _dialogService.ShowAlert(AppResources.InternetConnectionRequiredMessage);
                return;
            }
            
            if (TaxModel.Validate() is false)
            {
                await _dialogService.ShowAlert(AppResources.ValidationFailedMessage, string.Join("\n", TaxModel.GetErrors()));
                return;
            }

            TaxDetailsModel = await _taxService.CalculateTaxes(TaxModel);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex);
            await _dialogService.ShowAlert(AppResources.GeneralErrorMessage);
        }
    }

    private void SetValidators()
    {
        NotEmptyValidationRule<string> notEmptyValidationRule = new()
        {
            ValidationMessage = AppResources.RequiredFieldMessage
        };
        ZipCodeValidationRule<string> zipCodeValidationRule = new()
        {
            ValidationMessage = AppResources.ZipCodeInvalidMessage
        };
        CountryCodeValidationRule<string> countryCodeValidationRule = new()
        {
            ValidationMessage = AppResources.CountryCodeInvalidMessage
        };
        DoubleValidationRule doubleValidationRule = new()
        {
            ValidationMessage = AppResources.NumberInvalidMessage
        };
        StateValidationRule<string> stateValidationRule = new()
        {
            ValidationMessage = AppResources.StateInvalidMessage
        };

        TaxModel.ToCountry.Validations.Add(notEmptyValidationRule);
        TaxModel.ToZip.Validations.Add(notEmptyValidationRule);
        TaxModel.ToState.Validations.Add(notEmptyValidationRule);
        
        TaxModel.Amount.Validations.Add(doubleValidationRule);
        TaxModel.Shipping.Validations.Add(doubleValidationRule);
        
        TaxModel.ToCountry.Validations.Add(countryCodeValidationRule);
        TaxModel.FromCountry.Validations.Add(countryCodeValidationRule);
        
        TaxModel.ToZip.Validations.Add(zipCodeValidationRule);
        TaxModel.FromZip.Validations.Add(zipCodeValidationRule);
        
        TaxModel.ToState.Validations.Add(stateValidationRule);
        TaxModel.FromState.Validations.Add(stateValidationRule);
    }
}