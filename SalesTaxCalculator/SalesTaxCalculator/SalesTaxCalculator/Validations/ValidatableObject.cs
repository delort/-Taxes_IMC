using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MvvmCross.ViewModels;

namespace SalesTaxCalculator.Validations;

public class ValidatableObject<T> : MvxNotifyPropertyChanged
{
    public event EventHandler ValueChanged;

    private readonly List<IValidationRule<T>> _validations;
    private MvxObservableCollection<string> _errors;
    private T _value;
    private bool _isValid;

    public List<IValidationRule<T>> Validations => _validations;

    public MvxObservableCollection<string> Errors
    {
        get => _errors;
        set => SetProperty(ref _errors, value);
    }

    public T Value
    {
        get => _value;
        set => SetProperty(ref _value, value, OnValueChanged);
    }

    public bool IsValid
    {
        get => _isValid;
        set => SetProperty(ref _isValid, value);
    }

    public ValidatableObject()
    {
        _isValid = true;
        Errors = new MvxObservableCollection<string>();
        _validations = new List<IValidationRule<T>>();
    }

    public bool Validate(string propertyName = null)
    {
        Errors.Clear();
        var separator = string.IsNullOrEmpty(propertyName) ? string.Empty : ": ";

        var errors = _validations
            .Where(v => !v.Check(Value))
            .Select(v => $"{propertyName}{separator}{v.ValidationMessage}");

        Errors.AddRange(errors.ToList());
        IsValid = !Errors.Any();
        RaisePropertyChanged(nameof(Errors));
        return IsValid;
    }

    protected virtual void OnValueChanged()
    {
        ValueChanged?.Invoke(this, EventArgs.Empty);
    }

    public static string GetFirsValidationError(params ValidatableObject<T>[] props)
        => props.FirstOrDefault(vo => vo.Errors.Any())?.Errors.FirstOrDefault();
}