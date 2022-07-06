namespace SalesTaxCalculator.Validations;

public class StateValidationRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; }

    public bool Check(T value)
    {
        if (value is null)
        {
            return true;
        }

        return value.ToString().Length == 2;
    }
}