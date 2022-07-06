using Refit;
using SalesTaxCalculator.Common.Models;
using SalesTaxCalculator.Common.Models.Requests;

namespace SalesTaxCalculator.Services.ApiClients;

public interface IRatesApi
{
    [Get("/v2/rates/{zip}")]
    Task<IApiResponse<RateResponse>> GetRates(string zip, RateRequest parameters);
}