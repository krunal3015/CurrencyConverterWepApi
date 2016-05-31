using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zed.Core.Entities
{
    public class Response
    {
        public string SourceCurrency {get; set;}
        public decimal ConversionRate {get; set;}
        public decimal Amount {get; set;}
        public decimal Total {get; set;}
        public int returncode {get; set;}
        public string err {get; set;}
        public string timestamp { get; set; }
    }
}
