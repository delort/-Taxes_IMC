using SalesTaxCalculator.Common.Models;
using SalesTaxCalculator.Common.Models.Requests;

namespace SalesTaxCalculator.Common.Interfaces;

public interface ITaxService
{
    Task<Tax> CalculateTaxes(TaxesRequest request);
}