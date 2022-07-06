using SalesTaxCalculator.Common.Interfaces;
using SalesTaxCalculator.Common.Models;
using SalesTaxCalculator.Common.Models.Requests;
using SalesTaxCalculator.Services.ApiClients;

namespace SalesTaxCalculator.Services.Services;

public class TaxService : ITaxService
{
    private readonly ITaxesApi _taxesApi;

    public TaxService(ITaxesApi taxesApi)
    {
        _taxesApi = taxesApi;
    }
    public async Task<Tax> CalculateTaxes(TaxesRequest request)
    {
        var response = await _taxesApi.CalculateTaxes(request);

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return response.Content.Tax;
        }

        throw response.Error != null
            ? throw response.Error
            : throw new Exception(response.StatusCode.ToString());
    }
}