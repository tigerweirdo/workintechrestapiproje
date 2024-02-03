using Workintechrestapiproje.Domain;
using Workintechrestapiproje.Domain.ApiLayer;

namespace Workintechrestapiproje.Business.Currency
{
    public interface ICurrencyService : IBaseSingletonService
    {
        Task<string> GetCurrencySymbol(string currencyCode);
        Task<CurrencyResponse> GetCurrency();
        Task<ApiLayerResponse> PostCurrencyToApiLayer(string startDate, string endDate);
    }
}