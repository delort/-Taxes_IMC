namespace SalesTaxCalculator.Validations;

public class NotEmptyValidationRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; }

    public bool Check(T value)
    {
        if (value == null)
        {
            return false;
        }

        return !string.IsNullOrWhiteSpace(value.ToString());
    }
}