using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Zed.Core.DALInterfaces;
using Zed.Core.Entities;
using Zed.EntityFramework;

namespace Zed.DataAccessLayer
{
    public class CurrencyConverterRepository : ICurrencyConverterRepository
    {
        private readonly CurrencyConverterEntities _currencyConvertor;

        public CurrencyConverterRepository(DbContext currencyConvertor)
        {
            _currencyConvertor = (CurrencyConverterEntities)currencyConvertor;
        }
        
        public CurrencyRatesModel GetExchangeRates(string sourceCurrency)
        {
            var queryRates = _currencyConvertor.CurrencyRates.Where(x=>x.CurrencyFrom == sourceCurrency).FirstOrDefault();
            return MapToCurrencyRateModel(queryRates);
        }

        public CurrencyRatesModel MapToCurrencyRateModel(CurrencyRate currencyRate)
        {
            var data = new CurrencyRatesModel
            {
                CurrencyFrom = currencyRate.CurrencyFrom,
                CurrencyTo = currencyRate.CurrencyTo,
                Rate = Convert.ToDecimal(currencyRate.Rate),
                RateUpdateTime = Convert.ToDateTime(currencyRate.RateUpdateTime)
               
            };
            
            return data;
        }


        public List<CurrencyRatesModel> GetListOfCurrenciesFromDatabase()
        {
            var getlist = _currencyConvertor.CurrencyRates.Where(x => x.IsActive == true);
            return MapToCurrencyRateModelList(getlist);
        }

        public List<CurrencyRatesModel> MapToCurrencyRateModelList(IEnumerable<CurrencyRate> obj)
        {
            return obj.Select(MapToCurrencyRatesModelMapper).ToList();
        }
        public CurrencyRatesModel MapToCurrencyRatesModelMapper(CurrencyRate Objrates)
        {
            var data = new CurrencyRatesModel
            {
                CurrencyID = Objrates.CurrencyID,
                CurrencyFrom = Objrates.CurrencyFrom,
                CurrencyTo = Objrates.CurrencyTo,
                Amount = Convert.ToDecimal(Objrates.Amount),
                Rate = Convert.ToDecimal(Objrates.Rate),
                RateUpdateTime = Convert.ToDateTime(Objrates.RateUpdateTime)
            };
            return data;
        }

        public bool UpdateCurrencyRatesInDatabase(List<CurrencyRatesModel> currencyRateModelList)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    foreach (CurrencyRatesModel obj in currencyRateModelList)
                    {
                        
                        var data = from p in _currencyConvertor.CurrencyRates
                                   where p.CurrencyID == obj.CurrencyID
                                   select p;
                        
                        foreach (var objData in data)
                        {
                            objData.Rate = obj.Rate;
                            objData.RateUpdateTime = obj.RateUpdateTime;
                        }

                    }

                    _currencyConvertor.SaveChanges();
                    transaction.Complete();
                    return true;
                }
            }
            catch(Exception ex)
            {
                string err = ex.Message;
                return false;
            }


            foreach(CurrencyRatesModel obj in currencyRateModelList)
            {

            }
        }

    }
}
