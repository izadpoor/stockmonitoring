using System;
using System.Collections.Generic;
using System.Linq;

namespace StockMonitor
{
    /// <summary>
    /// The 'Subject' abstract class ( user)
    /// </summary>
    public abstract class Stock : IStock

    {
        private string _symbol;
        private double _price;
        private double _previousPrice;
        private StockPriceDirectionType _stockPriceDirection;
        private List<IInvestor> _investors = new List<IInvestor>();
        private List<INotification> _notificationHistory = new List<INotification>();
        


        // Constructor

        public Stock(string symbol, double price)
        {
            _symbol = symbol;
            _price = price;
            _previousPrice = 0;
            _stockPriceDirection = StockPriceDirectionType.Set;

        }

        public void Attach(IInvestor investor)
        {
            _investors.Add(investor);
        }

        public void Detach(IInvestor investor)
        {
            _investors.Remove(investor);
        }

        public void Notify()
        {

            foreach (IInvestor investor in _investors)
            {

                foreach (ITradeTrigger tradeTrigger in investor.TradeTriggers)
                {
                    TradeTriggerType tradeTriggerAction = getTriggerAction(tradeTrigger);

                    if (tradeTriggerAction!=TradeTriggerType.None && !hasPermissionToSendNotification(investor, tradeTrigger))
                        tradeTriggerAction = TradeTriggerType.None;

                    switch (tradeTriggerAction)
                    {
                        case TradeTriggerType.SellingStock:
                            investor.Sell(this, tradeTrigger);
                            updateNotificationHistory(investor, TradeTriggerType.SellingStock, tradeTrigger);
                            break;
                        case TradeTriggerType.BuyingStock:
                            investor.Buy(this, tradeTrigger);
                            updateNotificationHistory(investor, TradeTriggerType.BuyingStock, tradeTrigger);
                            break;
                        case TradeTriggerType.GeneralUpdate:
                            investor.Update(this, tradeTrigger);
                            updateNotificationHistory(investor, TradeTriggerType.GeneralUpdate, tradeTrigger);
                            break;
                        default:
                            break;
                    }


                }
            }


        }
        /// <summary>
        /// update the history of hit through the caller ( investors)
        /// </summary>
        /// <param name="investor"></param>
        /// <param name="sellingStock"></param>
        /// <param name="tradeTrigger"></param>
        private void updateNotificationHistory(IInvestor investor, TradeTriggerType tradeTriggerType, ITradeTrigger tradeTrigger)
        {
            var histoiry = NotificationHistory.Where(h => h.Investor == investor.InvestorName && h.TradeTriggerType == tradeTriggerType  && h.TriggerName == tradeTrigger.TriggerName).FirstOrDefault();

            if (histoiry == null)
                NotificationHistory.Add(new Notification() { Investor = investor.InvestorName, TradeTriggerType = tradeTriggerType, TriggerName = tradeTrigger.TriggerName, Counter = 1 });
            else
                histoiry.Counter = histoiry.Counter + 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tradeTrigger"></param>
        /// <returns></returns>
        TradeTriggerType getTriggerAction(ITradeTrigger tradeTrigger)
        {
            double priceMin = tradeTrigger.TargetPrice - tradeTrigger.FluctuationsRate;
            double priceMax = tradeTrigger.TargetPrice + +tradeTrigger.FluctuationsRate;

            TradeTriggerType tradeTriggerAction = TradeTriggerType.None;

            if (tradeTrigger.Sensitivity == StockSensitivityType.MeetExactTargetPrice && tradeTrigger.TargetPrice == _price)
            {
                tradeTriggerAction = getTradeTriggerActionByPriceDirection(tradeTrigger);

            }
            else if (tradeTrigger.Sensitivity == StockSensitivityType.MeetAroundTargetPrice
                && (priceMin <= _price && _price <= priceMax))
            {
                tradeTriggerAction = getTradeTriggerActionByPriceDirection(tradeTrigger);
            }

            return tradeTriggerAction;
        }

        /// <summary>
        /// check how oftne we have to send the notification throuight the caller ( investor), e.g: once, unlimited, 
        /// we can improve this by adding some more meta data in the notification interface like data, time, week,...
        /// then we could manage all diffrent senarioes like once per day m once per week , or all other combination that investor will inject by the time that
        /// set the investor in the stocket object or later attch new one...
        /// </summary>
        /// <param name="investor"></param>
        /// <param name="tradeTrigger"></param>
        /// <returns></returns>
        private bool hasPermissionToSendNotification(IInvestor investor, ITradeTrigger tradeTrigger)
        {
            if (tradeTrigger.NotificationType == ReceiveNotificationType.Unlimited)
                return true;
            else if (tradeTrigger.NotificationType == ReceiveNotificationType.Once)
            {
                if (NotificationHistory.Where(h => h.Investor == investor.InvestorName && h.TradeTriggerType == tradeTrigger.TradeTriggerType && h.TriggerName == tradeTrigger.TriggerName).FirstOrDefault() == null)
                    return true;
            }

            return false;
        }

        private TradeTriggerType getTradeTriggerActionByPriceDirection(ITradeTrigger tradeTrigger)
        {
            TradeTriggerType tradeTriggerAction;
            if (tradeTrigger.StockPriceDirectionType == StockPriceDirectionType.EitherWay || StockPriceDirection == tradeTrigger.StockPriceDirectionType)
                tradeTriggerAction = tradeTrigger.TradeTriggerType;
            else
                tradeTriggerAction = TradeTriggerType.None;
            return tradeTriggerAction;
        }


        // Gets or sets the price

        public double Price
        {
            get { return _price; }
            set

            {
                if (_price != value)
                {
                    _previousPrice = _price;
                    _price = value;
                    _stockPriceDirection = _price > _previousPrice ? StockPriceDirectionType.Raised : StockPriceDirectionType.Dropped;
                    
                    Notify();
                }
            }
        }


        // Gets the symbol

        public string Symbol
        {
            get { return _symbol; }
        }

        // Previous  closing stock price
        double PreviousPrice
        {
            get {  return _previousPrice; }
            set { _previousPrice = value; }
        }

        // Stock pricing chnage direction ( going up (raised), going down (dropped)
        StockPriceDirectionType StockPriceDirection
        {
            get { return _stockPriceDirection; }
        }

        public List<INotification> NotificationHistory
        {
            get { return _notificationHistory; }
        }
    }
}
