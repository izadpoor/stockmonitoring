using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMonitor
{

    public class Notification : INotification
    {
        public string TriggerName { get; set; }
        public string Investor { get; set; }
        public TradeTriggerType TradeTriggerType { get; set; }
        public double StockPrice { get; set; }
        public int Counter { get; set; }
        

    }
}
