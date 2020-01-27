using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMonitor
{
    public class HSBC : Stock
    {
        public HSBC(string symbol, double price)
          : base(symbol, price)
        {
        }
    }
}
