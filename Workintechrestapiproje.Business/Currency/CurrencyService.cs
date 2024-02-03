using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Workintechrestapiproje.Domain;
using Workintechrestapiproje.Domain.ApiLayer;
using Workintechrestapiproje.Domain.Exceptions;
using Serilog;

namespace Workintechrestapiproje.Business.Currency
{
    public class CurrencyService : BaseSingletonService, ICurrencyService
{
    private readonly ILogger<CurrencyService> logger;
    private readonly HttpClient client;

    public CurrencyService(ILogger<CurrencyService> _logger)
    {
        client = new HttpClient();
        logger = _logger;
    }

    public async Task<string> GetCurrencySymbol(string currencyCode)
    {
        logger.LogInformation("MS---Service Katmanından GetCurrencySymbol methodu çağrıldı");
        Log.Information("Service Katmanından GetCurrencySymbol methodu çağrıldı");
        Log.Information("currencyCode: {currencyCode}", currencyCode);
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
        Log.Information("Service Katmanından GetCurrency methodu çağrıldı");
        var responseString = await client.GetStringAsync("https://api.freecurrencyapi.com/v1/latest?apikey=fca_live_3gDOPySwd23vHYmBP18cg3c2VcFOjkIxO7S4MtDO&currencies=EUR%2CUSD%2CCAD%2CTRY");

        var response = JsonSerializer.Deserialize<CurrencyRoot>(responseString);
        return response.data ?? new CurrencyResponse();
    }

    public async Task<ApiLayerResponse> PostCurrencyToApiLayer(string startDate, string endDate)
    {
        Log.Information("Service Katmanından PostCurrencyToApiLayer methodu çağrıldı");
        if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
        {
            Log.Error("StartDate or enddate null");
            throw new ArgumentException("Başlangıç ve bitiş tarihleri boş olamaz");
        }

        try
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("apikey", "04k2b6eIXMqOE1l1FLM4fWrVOqMmdc7e");
            var responseString = await client.GetStringAsync($"https://api.apilayer.com/currency_data/change?start_date={startDate}&end_date={endDate}");

            var todayStart = "2024-01-24";
            var responseStringToday = await client.GetStringAsync($"https://api.apilayer.com/currency_data/change?start_date={todayStart}&end_date={todayStart}");

            var response = JsonSerializer.Deserialize<ApiLayerResponse>(responseString);
            var responseToday = JsonSerializer.Deserialize<ApiLayerResponse>(responseStringToday);

            Log.Warning("response: {@response}", response);
            Log.Warning("responseToday: {@responseToday}", responseToday);

            if (response == null || responseToday == null)
                throw new ArgumentException("ApiLayer'a istek atılırken hata oluştu");

            response.todayDiff = Math.Round(responseToday.quotes.USDTRY.start_rate - response.quotes.USDTRY.end_rate, 4);
            return response ?? new ApiLayerResponse();
        }
        catch (Exception e)
        {
            Log.Error(e.Message, e);
            throw new CustomException("API çağrılırken hata oluştu");
        }

    }
}
}