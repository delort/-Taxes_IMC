using SalesTaxCalculator.Common.Models;

namespace SalesTaxCalculator.Models;

public class TaxDetailsModel
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

    public static implicit operator TaxDetailsModel(Tax dto) => dto == null
        ? null
        : new TaxDetailsModel
        {
            AmountToCollect = dto.AmountToCollect,
            Breakdown = dto.Breakdown,
            FreightTaxable = dto.FreightTaxable,
            HasNexus = dto.HasNexus,
            Jurisdictions = dto.Jurisdictions,
            OrderTotalAmount = dto.OrderTotalAmount,
            Rate = dto.Rate,
            Shipping = dto.Shipping,
            TaxSource = dto.TaxSource,
            TaxableAmount = dto.TaxableAmount,
        };
}