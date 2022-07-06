namespace SalesTaxCalculator.Validations;

public class CountryCodeValidationRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; }

    public bool Check(T value)
    {
        return value is null || value.ToString() switch
        {
            "US" => true,
            "CA" => true,
            _ => false
        };
    }
}