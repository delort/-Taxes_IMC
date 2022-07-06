namespace SalesTaxCalculator.Common.Models;

public class BaseBreakdown
{
    public double CombinedTaxRate { get; set; }
    public List<BaseLineItem> LineItems { get; set; }
    public BaseShipping Shipping { get; set; }
    public double TaxCollectable { get; set; }
    public double TaxableAmount { get; set; }
}