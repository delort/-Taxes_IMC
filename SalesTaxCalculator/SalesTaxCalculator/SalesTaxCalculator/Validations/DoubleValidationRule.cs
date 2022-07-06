namespace SalesTaxCalculator.Validations;

public class DoubleValidationRule : IValidationRule<string>
{
    public string ValidationMessage { get; set; }

    public bool Check(string value)
    {
        return double.TryParse(value, out _);
    }
}