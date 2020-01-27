using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMonitor
{
    public class Investor : IInvestor
    {
        private string _name;
        private List<ITradeTrigger> _tradTriggers = new List<ITradeTrigger>();
        private Stock _stock;

        /// <summary>
        ///  Constructor to add only one trigger
        /// </summary>
        /// <param name="name">investor name</param>
        /// <param name="tradeTrigger">how to get notification(rule)</param>
        public Investor(string name, ITradeTrigger tradeTrigger)
        {
            this._name = name;
            this._tradTriggers.Add(tradeTrigger);
        }

        /// <summary>
        /// Constructor to set list of triggers based on diffrent criteria to get notification eg. at drop sell or buy, at raise only buy,...
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tradTriggers"> list of trigger notification logic...</param>
        public Investor(string name, List<ITradeTrigger> tradTriggers)
        {
            this._name = name;
            this._tradTriggers.AddRange(tradTriggers);
        }

        /// <summary>
        /// notify just in generic mode to receive not specefic action
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="tradeTrigger"></param>
        public void Update(IStock stock, ITradeTrigger tradeTrigger)
        {
            printMessage(stock, tradeTrigger);
        }

       
        /// <summary>
        /// only notify by Stock once the selling action meet
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="tradeTrigger"></param>
        public void Sell(IStock stock, ITradeTrigger tradeTrigger)
        {
            printMessage(stock, tradeTrigger);

        }

        /// <summary>
        /// only notify by Stock once the buying action meet
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="tradeTrigger"></param>
        public void Buy(IStock stock, ITradeTrigger tradeTrigger)
        {
            printMessage(stock, tradeTrigger);
        }

        private void printMessage(IStock stock, ITradeTrigger tradeTrigger)
        {
            Console.WriteLine("Notified ({0}) {1} - {2} of {3}'s " +
              "change to {4:C} ==>{5}", tradeTrigger.TradeTriggerType, _name, tradeTrigger.TriggerName, stock.Symbol, stock.Price, DateTime.Now.ToLocalTime());
            
        }

        public bool AddTrigger(ITradeTrigger tradeTrigger)
        {
            _tradTriggers.Add(tradeTrigger);
            return true;
        }

        public bool RemoveTrigger(ITradeTrigger tradeTrigger)
        {
            _tradTriggers.Remove(tradeTrigger);
            return true;
        }

        // Gets or sets the stock

        public string InvestorName
        {
            get { return _name; }
            set { _name = value; }
        }
        public Stock Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }

        public List<ITradeTrigger> TradeTriggers
        {
            get { return _tradTriggers; }
            set { _tradTriggers = value; }

        }

    }
}
