using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMonitor
{
    public class Intel : Stock
    {
        public Intel(string symbol, double price)
          : base(symbol, price)
        {
        }
    }
}
