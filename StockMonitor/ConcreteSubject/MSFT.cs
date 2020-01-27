using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMonitor
{
    public class MSFT : Stock
    {
        public MSFT(string symbol, double price)
          : base(symbol, price)
        {
        }
    }
}
