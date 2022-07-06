namespace SalesTaxCalculator.Common.Models.USA;

public class BreakdownUsa : BaseBreakdown
{
    public double CityTaxCollectable { get; set; }
    public double CityTaxRate { get; set; }
    public double CityTaxableAmount { get; set; }
    public double CountyTaxCollectable { get; set; }
    public double CountyTaxRate { get; set; }
    public double CountyTaxableAmount { get; set; }
    public double SpecialDistrictTaxCollectable { get; set; }
    public double SpecialDistrictTaxableAmount { get; set; }
    public double SpecialTaxRate { get; set; }
    public double StateTaxCollectable { get; set; }
    public double StateTaxRate { get; set; }
    public double StateTaxableAmount { get; set; }
}