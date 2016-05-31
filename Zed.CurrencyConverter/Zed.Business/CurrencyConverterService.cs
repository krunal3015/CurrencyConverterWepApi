using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zed.Core.BusinessInterfaces;
using Zed.Core.DALInterfaces;
using Zed.Core.Entities;

namespace Zed.Business
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        private readonly ICurrencyConverterRepository _currencyconverterRepository;

        public CurrencyConverterService(ICurrencyConverterRepository currencyconverterRepository)
        {
            _currencyconverterRepository = currencyconverterRepository;
        }

        public CurrencyRatesModel GetExchangeRates(string sourceCurrency)
        {
            return _currencyconverterRepository.GetExchangeRates(sourceCurrency);
        }

        public List<CurrencyRatesModel> GetListOfCurrenciesFromDatabase()
        {
            return _currencyconverterRepository.GetListOfCurrenciesFromDatabase();
        }

        public bool UpdateCurrencyRatesInDatabase(List<CurrencyRatesModel> currencyRateModelList)
        {
            return _currencyconverterRepository.UpdateCurrencyRatesInDatabase(currencyRateModelList);
        }
    }
}
