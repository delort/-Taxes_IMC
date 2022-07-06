namespace SalesTaxCalculator.Common.Models;

public class TaxResponse
{
    public Tax Tax { get; set; }
}
public class Tax
{
    public double AmountToCollect { get; set; }
    public BaseBreakdown Breakdown { get; set; }
    public bool FreightTaxable { get; set; }
    public bool HasNexus { get; set; }
    public BaseJurisdictions Jurisdictions { get; set; }
    public double OrderTotalAmount { get; set; }
    public double Rate { get; set; }
    public double Shipping { get; set; }
    public string TaxSource { get; set; }
    public double TaxableAmount { get; set; }
}