namespace SalesTaxCalculator.Common.Models;

public class BaseLineItem
{
    public double CombinedTaxRate { get; set; }
    public string Id { get; set; }
    public double TaxCollectable { get; set; }
    public double TaxableAmount { get; set; }
}