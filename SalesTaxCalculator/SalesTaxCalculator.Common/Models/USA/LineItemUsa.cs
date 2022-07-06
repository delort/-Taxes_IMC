namespace SalesTaxCalculator.Common.Models.USA;

public class LineItemUsa : BaseLineItem
{
    public double CityAmount { get; set; }
    public double CityTaxRate { get; set; }
    public double CityTaxableAmount { get; set; }
    public double CountyAmount { get; set; }
    public double CountyTaxRate { get; set; }
    public double CountyTaxableAmount { get; set; }
    public double SpecialDistrictAmount { get; set; }
    public double SpecialDistrictTaxableAmount { get; set; }
    public double SpecialTaxRate { get; set; }
    public double StateAmount { get; set; }
    public double StateSalesTaxRate { get; set; }
    public double StateTaxableAmount { get; set; }
}