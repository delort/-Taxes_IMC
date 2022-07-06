namespace SalesTaxCalculator.Common.Models.Requests;

public class LineRequest
{
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public string ProductTaxCode { get; set; }
}