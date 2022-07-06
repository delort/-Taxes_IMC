namespace SalesTaxCalculator.Common.Models;

public class BaseShipping
{
    public double CombinedTaxRate { get; set; }
    public double TaxCollectable { get; set; }
    public double TaxableAmount { get; set; }
}