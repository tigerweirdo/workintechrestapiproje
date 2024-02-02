using Workintechrestapiproje.Domain;
using Workintechrestapiproje.Domain.ApiLayer;

namespace Workintechrestapiproje.Business
{
    public interface ICurrencyService
    {
        Task<CurrencyResponse> GetCurrency();
        Task<string> GetCurrencySymbol(string currencyCode);
        Task<ApiLayerResponse> PostCurrencyToApiLayer(string startDate, string endDate);
    }
}