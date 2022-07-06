using System.Collections.Generic;
using MvvmCross.ViewModels;
using SalesTaxCalculator.Common.Models;
using SalesTaxCalculator.Common.Models.Requests;
using SalesTaxCalculator.Validations;

namespace SalesTaxCalculator.Models;

public class TaxModel : MvxNotifyPropertyChanged, IValidableModel
{
    private ValidatableObject<string> _fromCountry;
    private ValidatableObject<string> _fromZip;
    private ValidatableObject<string> _fromState;
    private ValidatableObject<string> _toCountry;
    private ValidatableObject<string> _toZip;
    private ValidatableObject<string> _toState;
    private ValidatableObject<string> _amount;
    private ValidatableObject<string> _shipping;

    public TaxModel()
    {
        _fromCountry = new ValidatableObject<string>();
        _fromZip = new ValidatableObject<string>();
        _fromState = new ValidatableObject<string>();
        _toCountry = new ValidatableObject<string>();
        _toZip = new ValidatableObject<string>();
        _toState = new ValidatableObject<string>();
        _amount = new ValidatableObject<string>();
        _shipping = new ValidatableObject<string>();
    }

    public ValidatableObject<string> FromCountry
    {
        get => _fromCountry;
        set => SetProperty(ref _fromCountry, value);
    }

    public ValidatableObject<string> FromZip
    {
        get => _fromZip;
        set => SetProperty(ref _fromZip, value);
    }

    public ValidatableObject<string> FromState
    {
        get => _fromState;
        set => SetProperty(ref _fromState, value);
    }

    public ValidatableObject<string> ToCountry
    {
        get => _toCountry;
        set => SetProperty(ref _toCountry, value);
    }

    public ValidatableObject<string> ToZip
    {
        get => _toZip;
        set => SetProperty(ref _toZip, value);
    }

    public ValidatableObject<string> ToState
    {
        get => _toState;
        set => SetProperty(ref _toState, value);
    }

    public ValidatableObject<string> Amount
    {
        get => _amount;
        set => SetProperty(ref _amount, value);
    }

    public ValidatableObject<string> Shipping
    {
        get => _shipping;
        set => SetProperty(ref _shipping, value);
    }

    public List<string> GetErrors()
    {
        var result = new List<string>();

        result.AddRange(FromCountry.Errors);
        result.AddRange(FromZip.Errors);
        result.AddRange(FromState.Errors);
        result.AddRange(ToCountry.Errors);
        result.AddRange(ToZip.Errors);
        result.AddRange(ToState.Errors);
        result.AddRange(Amount.Errors);
        result.AddRange(Shipping.Errors);

        return result;
    }

    public bool Validate()
    {
        FromCountry.Validate(nameof(FromCountry));
        FromZip.Validate(nameof(FromZip));
        FromState.Validate(nameof(FromState));
        ToCountry.Validate(nameof(ToCountry));
        ToZip.Validate(nameof(ToZip));
        ToState.Validate(nameof(ToState));
        Amount.Validate(nameof(Amount));
        Shipping.Validate(nameof(Shipping));

        return FromCountry.IsValid
               && FromZip.IsValid
               && FromState.IsValid
               && ToCountry.IsValid
               && ToZip.IsValid
               && ToState.IsValid
               && Amount.IsValid
               && Shipping.IsValid;
    }

    public static implicit operator TaxesRequest(TaxModel model) => model == null
        ? null
        : new TaxesRequest
        {
            FromCountry = model.FromCountry.Value,
            FromZip = model.FromZip.Value,
            FromState = model.FromState.Value,
            ToCountry = model.ToCountry.Value,
            ToZip = model.ToZip.Value,
            ToState = model.ToState.Value,
            Amount = double.Parse(model.Amount.Value),
            Shipping = double.Parse(model.Shipping.Value)
        };
}