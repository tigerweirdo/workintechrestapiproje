using Workintechrestapiproje.Domain;

namespace Workintechrestapiproje.Business
{
    public interface ICurrencyService
    {
        Task<string> GetCurrencySymbol(string currencyCode);
        Task<CurrencyResponse> GetCurrency();
    }
}