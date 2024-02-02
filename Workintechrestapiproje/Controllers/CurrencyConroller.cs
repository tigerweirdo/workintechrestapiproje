using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workintechrestapiproje.Business;

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
            try
            {
                string currencySymbol = await _currencyService.GetCurrencySymbol(currencyCode);
                return currencySymbol;
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
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
                return BadRequest(e.Message);
            }
        }
    }
}