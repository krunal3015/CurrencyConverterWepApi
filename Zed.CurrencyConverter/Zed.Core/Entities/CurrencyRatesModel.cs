using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zed.Core.Entities
{
    public class CurrencyRatesModel
    {
        public int CurrencyID { get; set; }
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public decimal? Amount { get; set; }
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
        public DateTime RateUpdateTime { get; set; }

    }
}
