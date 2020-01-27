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

    public interface IInvestor
    {
        string InvestorName { get; set; }
        void Update(IStock stock, ITradeTrigger tradeTrigger);
        void Sell(IStock stock, ITradeTrigger tradeTrigger);
        void Buy(IStock stock, ITradeTrigger tradeTrigger);
        List<ITradeTrigger> TradeTriggers { get; }
        bool AddTrigger(ITradeTrigger tradeTrigger);

        bool RemoveTrigger(ITradeTrigger tradeTrigger);
        

    }
}
