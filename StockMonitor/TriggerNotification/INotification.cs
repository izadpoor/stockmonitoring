using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMonitor
{
    public interface INotification
    {
        string TriggerName { get; set; }
        string Investor { get; set; }
        TradeTriggerType TradeTriggerType { get; set; }
        double StockPrice { get; set; }
        int Counter { get; set; }
        
    }
}
