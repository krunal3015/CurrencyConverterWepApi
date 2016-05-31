using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Zed.Core.BusinessInterfaces;
using Zed.Core.Entities;
using Zed.Core.Resolvers;

namespace Zed.CurrencyConverter.Controllers
{
    public class v1Controller : ApiController
    {
         private readonly IServiceResolver _serviceResolver;
        private readonly ICurrencyConverterService _currencyconverterService;

        public v1Controller()
        {
            _serviceResolver = new WindsorResolver();
            _currencyconverterService = _serviceResolver.GetService<ICurrencyConverterService>();
        }

        //[HttpPost]
        //public JObject rate(dynamic model)
        //{
        //    JObject p = new JObject();
        //    CurrencyRatesModel currencyRateModel = JsonConvert.DeserializeObject<CurrencyRatesModel>(model.ToString());

        //    try
        //    {
        //        using (var client = new WebClient())
        //        {
        //            if (currencyRateModel.CurrencyFrom.Trim() == string.Empty)
        //                currencyRateModel.CurrencyFrom = "USD";

        //            if (currencyRateModel.Amount == null)
        //                currencyRateModel.Amount = 1;

        //            var rates = _currencyconverterService.GetExchangeRates(currencyRateModel.CurrencyFrom);

        //            if (rates != null)
        //            {
        //                return p = JObject.FromObject(new
        //                {
        //                    SourceCurrency = currencyRateModel.CurrencyFrom,
        //                    ConversionRate = rates.Rate,
        //                    Amount = currencyRateModel.Amount,
        //                    Total = (currencyRateModel.Amount * rates.Rate),
        //                    returncode = 1,
        //                    err = "Success",
        //                    timestamp = rates.RateUpdateTime.Ticks
        //                });
        //            }
        //            else
        //            {
        //                return p = JObject.FromObject(new
        //                {
        //                    SourceCurrency = currencyRateModel.CurrencyFrom,
        //                    ConversionRate = "",
        //                    Amount = currencyRateModel.Amount,
        //                    Total = "",
        //                    returncode = 1,
        //                    err = "Success, but rates not available for this Currency.",
        //                    timestamp = ""
        //                });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return p = JObject.FromObject(new
        //        {
        //            SourceCurrency = currencyRateModel.CurrencyFrom,
        //            ConversionRate = "",
        //            Amount = currencyRateModel.Amount,
        //            Total = "",
        //            returncode = ex.HResult,
        //            err = ex.Message,
        //            timestamp = ""
        //        });
        //    }

        //}


        //[HttpPost]
        //public IHttpActionResult rate(string From, string amt)
        //{
        //    decimal Amount;
        //    decimal.TryParse(amt, out Amount);
            
        //    try
        //    {
        //        using (var client = new WebClient())
        //        {
        //            if (From == null)
        //                From = "USD";

        //            if (Amount == 0)
        //                Amount = 1;

        //            var rates = _currencyconverterService.GetExchangeRates(From);

        //            if(rates!=null)
        //            {
        //                Response res = new Response
        //                {
        //                    SourceCurrency = From,
        //                    ConversionRate = rates.Rate,
        //                    Amount = Amount,
        //                    Total = (Amount * rates.Rate),
        //                    returncode = 1,
        //                    err = "Success",
        //                    timestamp = Convert.ToString(rates.RateUpdateTime.Ticks)
        //                };

        //                return this.Json(res);
        //            } 
        //            else
        //            {
        //                Response res1 = new Response
        //                {
        //                     SourceCurrency = From,
        //                    ConversionRate = 0.00m,
        //                    Amount = Amount,
        //                    Total = 0.00m,
        //                    returncode = 1,
        //                    err = "Success, but rates not available for this Currency.",
        //                    timestamp = ""
        //                };

        //                return this.Json(res1);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response res1 = new Response
        //        {
        //            SourceCurrency = From,
        //            ConversionRate = 0.00m,
        //            Amount = Amount,
        //            Total = 0.00m,
        //            returncode = ex.HResult,
        //            err = ex.Message,
        //            timestamp = ""
        //        };
        //    }

        //    return NotFound();
        //}

    }
}
