using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Zed.Core.BusinessInterfaces;
using Zed.Core.Entities;
using Zed.Core.Resolvers;

namespace Zed.CurrencyConverter.Controllers
{
    public class v0Controller : ApiController
    {
        private readonly IServiceResolver _serviceResolver;
        private readonly ICurrencyConverterService _currencyconverterService;

        public v0Controller()
        {
            _serviceResolver = new WindsorResolver();
            _currencyconverterService = _serviceResolver.GetService<ICurrencyConverterService>();
        }

        [HttpPost]
        public async Task<IHttpActionResult> rate(string From, string amt)
        {
            decimal Amount;
            decimal.TryParse(amt, out Amount);

            try
            {
                using (var client = new WebClient())
                {
                    if (From == null)
                        From = "USD";

                    if (Amount == 0)
                        Amount = 1;

                    //var rates = _currencyconverterService.GetExchangeRates(From);

                    Task<Response> t = GetCurrencyRatesAsync(From, Amount);
                    await t;
                    
                   if(t.IsCompleted)
                   {
                       return this.Json(t.Result);
                   }

                   return NotFound();
                }
            }
            catch (Exception ex)
            {
                Response res2 = new Response
                {
                    SourceCurrency = From,
                    ConversionRate = 0.00m,
                    Amount = Amount,
                    Total = 0.00m,
                    returncode = ex.HResult,
                    err = ex.Message,
                    timestamp = ""
                };
                return this.Json(res2);
            }

            //return NotFound();
        }

        public Task<Response> GetCurrencyRatesAsync(string From, decimal Amount)
        {
            return Task.Run(() => CurrencyRates(From, Amount));
        }

        public Response CurrencyRates(string From, decimal Amount)
        {
            var rates = _currencyconverterService.GetExchangeRates(From);

            if (rates != null)
            {
                Response res = new Response
                {
                    SourceCurrency = From,
                    ConversionRate = rates.Rate,
                    Amount = Amount,
                    Total = (Amount * rates.Rate),
                    returncode = 1,
                    err = "Success",
                    timestamp = Convert.ToString(rates.RateUpdateTime.Ticks)
                };
                //return this.Json(res);
                return res;
            }
            else
            {
                Response res1 = new Response
                {
                    SourceCurrency = From,
                    ConversionRate = 0.00m,
                    Amount = Amount,
                    Total = 0.00m,
                    returncode = 1,
                    err = "Rates not available for this Currency.",
                    timestamp = ""
                };

                //return this.Json(res1);
                return res1;
            }

        }

    }
}
