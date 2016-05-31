using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zed.Core.Entities;

namespace Zed.Core.BusinessInterfaces
{
    public interface ICurrencyConverterService
    {
        CurrencyRatesModel GetExchangeRates(string sourceCurrency);
        List<CurrencyRatesModel> GetListOfCurrenciesFromDatabase();
        bool UpdateCurrencyRatesInDatabase(List<CurrencyRatesModel> currencyRateModelList);
    }
}
