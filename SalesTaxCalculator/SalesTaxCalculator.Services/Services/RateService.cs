using Refit;
using SalesTaxCalculator.Common.Interfaces;
using SalesTaxCalculator.Common.Models;
using SalesTaxCalculator.Services.ApiClients;

namespace SalesTaxCalculator.Services.Services;

public class RateService : IRateService
{
    private readonly IRatesApi _ratesApi;

    public RateService(IRatesApi ratesApi)
    {
        _ratesApi = ratesApi;
    }
    
    public async Task<Rate> GetRates(string zipCode, string country, string city = null)
    {
        var response = await _ratesApi.GetRates(zipCode, new()
        {
            Country = country,
            City = city
        });
        
        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return response.Content.Rate;
        }

        throw response.Error != null
            ? throw response.Error
            : throw new Exception(response.StatusCode.ToString());
    }
}