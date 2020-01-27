using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMonitor
{
    public class RBC : Stock
    {
        public RBC(string symbol, double price)
          : base(symbol, price)
        {
        }
    }
}
