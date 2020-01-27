using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMonitor
{
    class Program
    {
        static void Main(string[] args)
        {

            // Create RBC stock and attach investors
            IStock rbc = new RBC("RBC", 100.00);

            ITradeTrigger investor1Trigger = new TradeTrigger()
            {
                TriggerName = "investor1Trigger",
                NotificationType = ReceiveNotificationType.Unlimited,
                Sensitivity = StockSensitivityType.MeetExactTargetPrice,
                TargetPrice = 120.10,
                FluctuationsRate = 0.2,
                TradeTriggerType = TradeTriggerType.BuyingStock,
                StockPriceDirectionType = StockPriceDirectionType.EitherWay

            };

            List<ITradeTrigger> investor2Triggeres = new List<ITradeTrigger>();
            investor2Triggeres.Add(new TradeTrigger()
            {
                TriggerName = "investor2Trigger1",
                NotificationType = ReceiveNotificationType.Once,
                Sensitivity = StockSensitivityType.MeetExactTargetPrice,
                TargetPrice = 120.10,
                FluctuationsRate = 0.2,
                TradeTriggerType = TradeTriggerType.BuyingStock,
                StockPriceDirectionType = StockPriceDirectionType.Dropped

            });
            investor2Triggeres.Add( new TradeTrigger()
            {
                TriggerName = "investor2Trigger2",
                NotificationType = ReceiveNotificationType.Unlimited,
                Sensitivity = StockSensitivityType.MeetAroundTargetPrice,
                TargetPrice = 120.10,
                FluctuationsRate = 0.2,
                TradeTriggerType = TradeTriggerType.SellingStock,
                StockPriceDirectionType = StockPriceDirectionType.Raised

            });

            // Create RBC stock and attach investors

            IInvestor investor1 = new Investor("investor1", investor1Trigger);
            rbc.Attach(investor1);
            rbc.Attach(new Investor("investor2", investor2Triggeres));

            // Fluctuating prices will notify investors

            rbc.Price = 120.10;
            rbc.Price = 121.00;
            rbc.Price = 120.50;
            rbc.Price = 120.10;
            rbc.Price = 119.10;
            rbc.Price = 120.10;

            //adding new investor trigger
            ITradeTrigger investor3Trigger = new TradeTrigger()
            {
                TriggerName = "investor3Trigger1",
                NotificationType = ReceiveNotificationType.Unlimited,
                Sensitivity = StockSensitivityType.MeetExactTargetPrice,
                TargetPrice = 122,
                FluctuationsRate = 0.03,
                TradeTriggerType = TradeTriggerType.BuyingStock,
                StockPriceDirectionType = StockPriceDirectionType.Raised

            };
            //attach new investor to the RBC stock investor list
            IInvestor investor3 = new Investor("investor3", investor3Trigger);
            Console.WriteLine("-------------- Investor3 attached-------------");
            rbc.Attach(investor3);

            rbc.Price = 120.10;
            rbc.Price = 122.00;
            rbc.Price = 122.50;
            rbc.Price = 120.10;
            rbc.Price = 119.00;
            rbc.Price = 120.10;
            rbc.Price = 110.10;

            rbc.Detach(investor1);
            Console.WriteLine("-------------- Investor1 detached-------------");
            rbc.Price = 112.00;
            rbc.Price = 120.10;
            rbc.Price = 122.00;


            // Create HSBC stock and attach investors
            //IStock hsbc= new HSBC("HSBC", 120.00);
            //hsbc.Attach(new Investor("investor1", investor1Trigger ));
            //hsbc.Attach(new Investor("investor2", investor2Triggeres));

            // Create Intel stock and attach investors
            //IStock intel = new Intel("Intel", 100.00);
            //intel.Attach(new Investor("investor1", investor1Trigger));
            //intel.Attach(new Investor("investor2", investor2Triggeres ));





            // Wait 

            Console.ReadKey();
        }
    }
}
