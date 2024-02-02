using System.Text.Json;
using Workintechrestapiproje.Domain;


namespace Workintechrestapiproje.Business;

public class CurrencyService : ICurrencyService
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<string> GetCurrencySymbol(string currencyCode)
    {
        return currencyCode switch
        {
            "USD" => "$",
            "EUR" => "€",
            "GBP" => "£",
            _ => throw new ArgumentException(message: "invalid currency code", paramName: nameof(currencyCode)),
        };
    }

    public async Task<CurrencyResponse> GetCurrency()
    {
        var responseString = await client.GetStringAsync("https://api.freecurrencyapi.com/v1/latest?apikey=fca_live_3gDOPySwd23vHYmBP18cg3c2VcFOjkIxO7S4MtDO&currencies=EUR%2CUSD%2CCAD%2CTRY");

        var response = JsonSerializer.Deserialize<CurrencyRoot>(responseString);
        return response.data ?? new CurrencyResponse();
    }
}