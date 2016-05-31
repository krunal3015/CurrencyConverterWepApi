using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using Zed.Core.BusinessInterfaces;
using Zed.Core.Entities;
using Zed.Core.Resolvers;

namespace Zed.CurrencyConverter.Controllers
{
    public class UpdateRatesController : ApiController
    {
        private readonly IServiceResolver _serviceResolver;
        private readonly ICurrencyConverterService _currencyconverterService;

        public UpdateRatesController()
        {
            _serviceResolver = new WindsorResolver();
            _currencyconverterService = _serviceResolver.GetService<ICurrencyConverterService>();
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateCurrencyRate()
        {
            try
            {
                Task<List<CurrencyRatesModel>> t = UpdateCurrencyRateAsync();
                await t;

                if(t.IsCompleted)
                {
                    //bool updated = _currencyconverterService.UpdateCurrencyRatesInDatabase(t.Result);
                    Task<bool> t1 = UpdateRateListInDatabase(t.Result);
                    await t1;

                    if(t1.IsCompleted)
                    {
                        return Ok();
                    }
                }
                return Ok();                
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return NotFound();
            }

        }

        public Task<List<CurrencyRatesModel>> UpdateCurrencyRateAsync()
        {
            return Task.Run(() => UpdateExchangeRates());
        }
        public List<CurrencyRatesModel> UpdateExchangeRates()
        {
            List<CurrencyRatesModel> currencyRateModelList = new List<CurrencyRatesModel>();
            var rateList = _currencyconverterService.GetListOfCurrenciesFromDatabase();

            foreach (CurrencyRatesModel obj in rateList)
            {
                //Grab your values and build your Web Request to the API
                string apiURL = String.Format("https://www.google.com/finance/converter?a={0}&from={1}&to={2}&meta={3}", obj.Amount, obj.CurrencyFrom, obj.CurrencyTo, Guid.NewGuid().ToString());

                //Make your Web Request and grab the results
                var request = WebRequest.Create(apiURL);

                //Get the Response
                var streamReader = new StreamReader(request.GetResponse().GetResponseStream(), System.Text.Encoding.ASCII);

                //Grab your converted value (ie 2.45 USD)
                string result = Regex.Matches(streamReader.ReadToEnd(), "<span class=\"?bld\"?>([^<]+)</span>")[0].Groups[1].Value;
                result = result.Remove(result.Length - 3);

                CurrencyRatesModel currencyrate = new CurrencyRatesModel
                {
                    CurrencyID = obj.CurrencyID,
                    CurrencyFrom = obj.CurrencyFrom,
                    Rate = Convert.ToDecimal(result),
                    RateUpdateTime = DateTime.Now
                };
                currencyRateModelList.Add(currencyrate);
            }

            return currencyRateModelList;
        }

        public Task<bool> UpdateRateListInDatabase(List<CurrencyRatesModel> currencyRatesModelList)
        {
            return Task.Run(() => UpdateRatesInDatabase(currencyRatesModelList));
        }
        public bool UpdateRatesInDatabase(List<CurrencyRatesModel> currencyRatesModelList)
        {
            bool updated = _currencyconverterService.UpdateCurrencyRatesInDatabase(currencyRatesModelList);
            return updated;
        }

    }
}
