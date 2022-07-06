using System;
using SalesTaxCalculator.Common.Models;

namespace SalesTaxCalculator.Models
{
    public class RateDetailsModel
    {
        public string City { get; set; }
        public string CityRate { get; set; }
        public string CombinedDistrictRate { get; set; }
        public string CombinedRate { get; set; }
        public string Country { get; set; }
        public string CountryRate { get; set; }
        public string County { get; set; }
        public string CountyRate { get; set; }
        public bool FreightTaxable { get; set; }
        public string State { get; set; }
        public string StateRate { get; set; }
        public string Zip { get; set; }

        public static implicit operator RateDetailsModel(Rate dto) => dto == null
            ? null
            : new RateDetailsModel
            {
                City = dto.City,
                CityRate = dto.CityRate,
                CombinedDistrictRate = dto.CombinedDistrictRate,
                CombinedRate = dto.CombinedRate,
                Country = dto.Country,
                CountryRate = dto.CountryRate,
                County = dto.County,
                CountyRate = dto.CountyRate,
                FreightTaxable = dto.FreightTaxable,
                State = dto.State,
                StateRate = dto.StateRate,
                Zip = dto.Zip,
            };
    }
}

