namespace SalesTaxCalculator.Common.Models.Requests;

public class TaxesRequest
{
    public string FromCountry { get; set; }
    public string FromZip { get; set; }
    public string FromState { get; set; }
    public string ToCountry { get; set; }
    public string ToZip { get; set; }
    public string ToState { get; set; }
    public double Amount { get; set; }
    public double Shipping { get; set; }
    public List<LineRequest> LineItems { get; set; }
}