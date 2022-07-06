using System.Collections.Generic;
using MvvmCross.ViewModels;
using SalesTaxCalculator.Common.Models;
using SalesTaxCalculator.Validations;

namespace SalesTaxCalculator.Models;

public class RateModel : MvxNotifyPropertyChanged, IValidableModel
{
    private ValidatableObject<string> _zipCode;
    private ValidatableObject<string> _country;
    private ValidatableObject<string> _city;

    public RateModel()
    {
        _zipCode = new ValidatableObject<string>();
        _country = new ValidatableObject<string>();
        _city = new ValidatableObject<string>();
    }
    
    public ValidatableObject<string> ZipCode
    {
        get => _zipCode;
        set => SetProperty(ref _zipCode, value);
    }

    public ValidatableObject<string> Country
    {
        get => _country;
        set => SetProperty(ref _country, value);
    }

    public ValidatableObject<string> City
    {
        get => _city;
        set => SetProperty(ref _city, value);
    }

    public List<string> GetErrors()
    {
        var result = new List<string>();
            
        result.AddRange(ZipCode.Errors);
        result.AddRange(Country.Errors);
        result.AddRange(City.Errors);
            
        return result;
    }

    public bool Validate()
    {
        ZipCode.Validate(nameof(ZipCode));
        Country.Validate(nameof(Country));
        City.Validate(nameof(City));

        return ZipCode.IsValid 
               && Country.IsValid 
               && City.IsValid;
    }
}