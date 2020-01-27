using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMonitor
{
    /// <summary>

    /// The 'Observer' interface

    /// </summary>
    public interface ITradeTrigger
    {
        string TriggerName { get; set; }
        StockPriceDirectionType StockPriceDirectionType { get; set; }
        double TargetPrice { get; set; }
        StockSensitivityType Sensitivity { get; set; }
        double FluctuationsRate { get; set; }
        
        TradeTriggerType TradeTriggerType { get; set; }
        ReceiveNotificationType NotificationType { get; set; }


    }
}
