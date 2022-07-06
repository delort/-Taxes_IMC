namespace SalesTaxCalculator.Common.Models.Canada;

public class ShippingCanada : BaseShipping
{
    public double Gst { get; set; }
    public double GstTaxRate { get; set; }
    public double GstTaxableAmount { get; set; }
    public double Pst { get; set; }
    public double PstTaxRate { get; set; }
    public double PstTaxableAmount { get; set; }
    public double Qst { get; set; }
    public double QstTaxRate { get; set; }
    public double QstTaxableAmount { get; set; }
}