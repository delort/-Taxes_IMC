namespace SalesTaxCalculator.Common.Models.Requests;

public class RateRequest
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
}