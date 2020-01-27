using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMonitor
{
    public class TradeTrigger : ITradeTrigger
    {

        public string TriggerName { get; set; }
        public StockSensitivityType Sensitivity { get;set; }
        public double FluctuationsRate { get; set; }
        public double TargetPrice { get; set; }
        public TradeTriggerType TradeTriggerType { get; set; }
        public ReceiveNotificationType NotificationType { get; set; }
        public StockPriceDirectionType StockPriceDirectionType { get; set ; }
        
    }
}
