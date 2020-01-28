using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMonitor
{
    public interface IStock
    {
        void Attach(IInvestor investor);
        void Detach(IInvestor investor);
        void Notify();
        double Price { get; set; }
        string Symbol { get;  }
        List<INotification> NotificationHistory { get; }

    }
}
