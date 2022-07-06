using SalesTaxCalculator.Common.Models;

namespace SalesTaxCalculator.Common.Interfaces;

public interface IRateService
{
    Task<Rate> GetRates(string zipCode, string country, string city = null);
}