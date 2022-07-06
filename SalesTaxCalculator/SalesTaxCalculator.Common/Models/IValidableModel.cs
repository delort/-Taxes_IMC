namespace SalesTaxCalculator.Common.Models;

public interface IValidableModel
{
    List<string> GetErrors();
    bool Validate();
}