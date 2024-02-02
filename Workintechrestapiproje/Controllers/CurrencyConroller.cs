using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workintechrestapiproje.Business;
using Workintechrestapiproje.Domain.ApiLayer;

namespace Workintechrestapiproje.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly Business.ICurrencyService _currencyService;

        public CurrencyController(Business.ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("{currencyCode}")]

        public async Task<ActionResult<string>> GetCurrencySymbol(string currencyCode)
        {

            if (string.IsNullOrEmpty(currencyCode))
            {
                throw new NullReferenceException();
            }

            try
            {
                string currencySymbol = await _currencyService.GetCurrencySymbol(currencyCode);
                return currencySymbol;
            }
            catch (System.Exception e)
            {
                return BadRequest("Hata oluştu" + e.Message);
            }
        }


        [HttpGet]
        public async Task<ActionResult<Domain.CurrencyResponse>> GetCurrency()
        {
            try
            {
                Domain.CurrencyResponse currencyResponse = await _currencyService.GetCurrency();
                return currencyResponse;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiLayerResponse>> PostCurrencyToApiLayer(string startDate, string endDate)
        {

            ApiLayerResponse apiLayerResponse = await _currencyService.PostCurrencyToApiLayer(startDate, endDate);
            return apiLayerResponse;

        }
    }
}