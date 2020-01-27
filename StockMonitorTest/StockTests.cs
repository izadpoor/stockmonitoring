using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMonitor.Tests
{
    [TestClass()]
    public class StockTests
    {
        //[TestMethod()]
        //public void StockTest()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void Test_RBC_Investor1_Get_Notification_If_Price_Drop_When_MeetExactTargetPrice()
        {

            IStock rbc = new RBC("RBC", 100.00);

            // request a specefic trigger once price dropped for exact target price
            ITradeTrigger investor1Trigger = new TradeTrigger()
            {
                NotificationType = ReceiveNotificationType.Once,
                TargetPrice = 120.10,
                Sensitivity = StockSensitivityType.MeetExactTargetPrice,
                FluctuationsRate = 0.2,
                TradeTriggerType = TradeTriggerType.SellingStock,
                StockPriceDirectionType = StockPriceDirectionType.Dropped

            };

            IInvestor investor1 = new Investor("investor1", investor1Trigger);
            rbc.Attach(investor1);

            // Stock price getting change by users 

            rbc.Price = 120.10;  
            rbc.Price = 122.00;  
            rbc.Price = 120.10;  
            List<INotification> alerts = rbc.NotificationHistory.Where(h => h.Investor == investor1.InvestorName && h.TradeTriggerType == investor1Trigger.TradeTriggerType).ToList();

            Assert.IsNotNull(alerts);
            Assert.IsTrue(alerts.Count == 1);
            Assert.AreEqual(alerts.FirstOrDefault().Counter, 1);
            Assert.AreEqual(alerts.FirstOrDefault().TradeTriggerType, TradeTriggerType.SellingStock);
            Assert.AreNotEqual(alerts.FirstOrDefault().TradeTriggerType, TradeTriggerType.BuyingStock);
            


        }


        [TestMethod()]
        public void Test_RBC_Investor1_Get_Notification_3times_Either_Price_Raise_Or_Drop_When_MeetExactTargetPrice()
        {

            IStock rbc = new RBC("RBC", 100.00);

            // request a specefic trigger once price dropped for exact target price
            ITradeTrigger investor1Trigger = new TradeTrigger()
            {
                NotificationType = ReceiveNotificationType.Unlimited,
                TargetPrice = 120.10,
                Sensitivity = StockSensitivityType.MeetExactTargetPrice,
                FluctuationsRate = 0.2,
                TradeTriggerType = TradeTriggerType.SellingStock,
                StockPriceDirectionType = StockPriceDirectionType.EitherWay  

            };

            IInvestor investor1 = new Investor("investor1", investor1Trigger);
            rbc.Attach(investor1);

            // Stock price getting change by users 

            rbc.Price = 120.10;  
            rbc.Price = 122.00;  
            rbc.Price = 120.10;  
            rbc.Price = 150.10;  
            rbc.Price = 120.10;  
            List<INotification> alerts = rbc.NotificationHistory.Where(h => h.Investor == investor1.InvestorName && h.TradeTriggerType == investor1Trigger.TradeTriggerType).ToList();

            Assert.IsNotNull(alerts);
            Assert.IsTrue(alerts.Count == 1);
            Assert.AreEqual(alerts.FirstOrDefault().Counter, 3);
            Assert.AreEqual(alerts.FirstOrDefault().TradeTriggerType, TradeTriggerType.SellingStock);
            Assert.AreNotEqual(alerts.FirstOrDefault().TradeTriggerType, TradeTriggerType.BuyingStock);



        }


        [TestMethod()]
        public void Test_RBC_Investor1_Get_Notification_3times_Either_Raise_Or_Drop_Price_MeetAroundTargetPrice()
        {

            IStock rbc = new RBC("RBC", 100.00);

            // request a specefic trigger once price dropped for exact target price
            ITradeTrigger investor1Trigger = new TradeTrigger()
            {
                NotificationType = ReceiveNotificationType.Unlimited,
                TargetPrice = 120.10,
                Sensitivity = StockSensitivityType.MeetAroundTargetPrice,
                FluctuationsRate = 0.2,
                TradeTriggerType = TradeTriggerType.SellingStock,
                StockPriceDirectionType = StockPriceDirectionType.EitherWay

            };

            IInvestor investor1 = new Investor("investor1", investor1Trigger);
            rbc.Attach(investor1);

            // Stock price getting change by users 

            rbc.Price = 120.00;
            rbc.Price = 121.20;
            rbc.Price = 119.10;
            rbc.Price = 120.19;
            rbc.Price = 120.08;

            List<INotification> alerts = rbc.NotificationHistory.Where(h => h.Investor == investor1.InvestorName && h.TradeTriggerType == investor1Trigger.TradeTriggerType).ToList();

            Assert.IsNotNull(alerts);
            Assert.IsTrue(alerts.Count == 1);
            Assert.AreEqual(alerts.FirstOrDefault().Counter, 3);
            Assert.AreEqual(alerts.FirstOrDefault().TradeTriggerType, TradeTriggerType.SellingStock);
            Assert.AreNotEqual(alerts.FirstOrDefault().TradeTriggerType, TradeTriggerType.BuyingStock);



        }


        [TestMethod()]
        public void Test_RBC_Investor1_Get_Notification_1time_Either_Raise_Or_Drop_Price_MeetAroundTargetPrice()
        {

            IStock rbc = new RBC("RBC", 100.00);

            // request a specefic trigger once price dropped for exact target price
            ITradeTrigger investor1Trigger = new TradeTrigger()
            {
                NotificationType = ReceiveNotificationType.Once,
                TargetPrice = 120.10,
                Sensitivity = StockSensitivityType.MeetAroundTargetPrice,
                FluctuationsRate = 0.2,
                TradeTriggerType = TradeTriggerType.SellingStock,
                StockPriceDirectionType = StockPriceDirectionType.EitherWay

            };

            IInvestor investor1 = new Investor("investor1", investor1Trigger);
            rbc.Attach(investor1);

            // Stock price getting change by users 

            rbc.Price = 120.00;
            rbc.Price = 121.20;
            rbc.Price = 119.10;
            rbc.Price = 120.19;
            rbc.Price = 120.08;

            List<INotification> alerts = rbc.NotificationHistory.Where(h => h.Investor == investor1.InvestorName && h.TradeTriggerType == investor1Trigger.TradeTriggerType).ToList();

            Assert.IsNotNull(alerts);
            Assert.IsTrue(alerts.Count == 1);
            Assert.AreEqual(alerts.FirstOrDefault().Counter, 1);
            Assert.AreEqual(alerts.FirstOrDefault().TradeTriggerType, TradeTriggerType.SellingStock);
            Assert.AreNotEqual(alerts.FirstOrDefault().TradeTriggerType, TradeTriggerType.BuyingStock);



        }


        [TestMethod()]
        public void Test_RBC_Investor1_And_Investor2_Get_Notification_2times_Each_If_Price_Raise_Or_Drop_To_MeetAroundTargetPrice()
        {

            IStock rbc = new RBC("RBC", 100.00);

            //rule to get notification
            ITradeTrigger investor1Trigger = new TradeTrigger()
            {
                NotificationType = ReceiveNotificationType.Unlimited,
                TargetPrice = 120.10,
                Sensitivity = StockSensitivityType.MeetAroundTargetPrice,
                FluctuationsRate = 0.2,
                TradeTriggerType = TradeTriggerType.SellingStock,
                StockPriceDirectionType = StockPriceDirectionType.EitherWay

            };

            IInvestor investor1 = new Investor("investor1", investor1Trigger);
            rbc.Attach(investor1);

            //rule to get notification
            ITradeTrigger investor2Trigger = new TradeTrigger()
            {
                NotificationType = ReceiveNotificationType.Unlimited,
                TargetPrice = 120.10,
                Sensitivity = StockSensitivityType.MeetAroundTargetPrice,
                FluctuationsRate = 0.2,
                TradeTriggerType = TradeTriggerType.SellingStock,
                StockPriceDirectionType = StockPriceDirectionType.EitherWay

            };

            IInvestor investor2 = new Investor("investor2", investor2Trigger);
            rbc.Attach(investor2);


            // Stock price getting change by users 

            rbc.Price = 120.00;
            rbc.Price = 121.20;
            rbc.Price = 119.10;
            rbc.Price = 120.19;

            List<INotification> investor1_notificationList = rbc.NotificationHistory.Where(h => h.Investor == investor1.InvestorName && h.TradeTriggerType == investor1Trigger.TradeTriggerType).ToList();
            List<INotification> investor2_notificationList = rbc.NotificationHistory.Where(h => h.Investor == investor2.InvestorName && h.TradeTriggerType == investor2Trigger.TradeTriggerType).ToList();

            

            Assert.IsNotNull(investor1_notificationList);
            Assert.IsNotNull(investor2_notificationList);
            Assert.IsTrue(investor1_notificationList.Count == 1);
            Assert.IsTrue(investor2_notificationList.Count == 1);

            Assert.AreEqual(investor1_notificationList.FirstOrDefault().Counter, 2);
            Assert.AreEqual(investor2_notificationList.FirstOrDefault().Counter, 2);
            
            Assert.AreEqual(investor1_notificationList.FirstOrDefault().TradeTriggerType, TradeTriggerType.SellingStock);
            Assert.AreEqual(investor2_notificationList.FirstOrDefault().TradeTriggerType, TradeTriggerType.SellingStock);

            Assert.AreNotEqual(investor1_notificationList.FirstOrDefault().TradeTriggerType, TradeTriggerType.BuyingStock);
            Assert.AreNotEqual(investor2_notificationList.FirstOrDefault().TradeTriggerType, TradeTriggerType.BuyingStock);



        }


        [TestMethod()]
        public void Test_RBC_Investor1_And_Investor2_Get_Notification_Only_1_time_Each_If_Price_Raise_Or_Drop_To_MeetAroundTargetPrice()
        {

            IStock rbc = new RBC("RBC", 100.00);

            //rule to get notification
            ITradeTrigger investor1Trigger = new TradeTrigger()
            {
                NotificationType = ReceiveNotificationType.Once,
                TargetPrice = 120.10,
                Sensitivity = StockSensitivityType.MeetAroundTargetPrice,
                FluctuationsRate = 0.2,
                TradeTriggerType = TradeTriggerType.SellingStock,
                StockPriceDirectionType = StockPriceDirectionType.EitherWay

            };

            IInvestor investor1 = new Investor("investor1", investor1Trigger);
            rbc.Attach(investor1);

            //rule to get notification
            ITradeTrigger investor2Trigger = new TradeTrigger()
            {
                NotificationType = ReceiveNotificationType.Once,
                TargetPrice = 120.10,
                Sensitivity = StockSensitivityType.MeetAroundTargetPrice,
                FluctuationsRate = 0.2,
                TradeTriggerType = TradeTriggerType.SellingStock,
                StockPriceDirectionType = StockPriceDirectionType.EitherWay

            };

            IInvestor investor2 = new Investor("investor2", investor2Trigger);
            rbc.Attach(investor2);


            // Stock price getting change by users 

            rbc.Price = 120.00;
            rbc.Price = 121.20;
            rbc.Price = 119.10;
            rbc.Price = 120.19;

            List<INotification> investor1_notificationList = rbc.NotificationHistory.Where(h => h.Investor == investor1.InvestorName && h.TradeTriggerType == investor1Trigger.TradeTriggerType).ToList();
            List<INotification> investor2_notificationList = rbc.NotificationHistory.Where(h => h.Investor == investor2.InvestorName && h.TradeTriggerType == investor2Trigger.TradeTriggerType).ToList();



            Assert.IsNotNull(investor1_notificationList);
            Assert.IsNotNull(investor2_notificationList);
            Assert.IsTrue(investor1_notificationList.Count == 1);
            Assert.IsTrue(investor2_notificationList.Count == 1);

            Assert.AreEqual(investor1_notificationList.FirstOrDefault().Counter, 1);
            Assert.AreEqual(investor2_notificationList.FirstOrDefault().Counter, 1);

            Assert.AreEqual(investor1_notificationList.FirstOrDefault().TradeTriggerType, TradeTriggerType.SellingStock);
            Assert.AreEqual(investor2_notificationList.FirstOrDefault().TradeTriggerType, TradeTriggerType.SellingStock);

            Assert.AreNotEqual(investor1_notificationList.FirstOrDefault().TradeTriggerType, TradeTriggerType.BuyingStock);
            Assert.AreNotEqual(investor2_notificationList.FirstOrDefault().TradeTriggerType, TradeTriggerType.BuyingStock);



        }


        [TestMethod()]
        public void Test_RBC_Investor1_Get_1_Notification_Investor2_Get_2_Notification_If_Price_Raise_Or_Drop_To_MeetAroundTargetPrice()
        {

            IStock rbc = new RBC("RBC", 100.00);

            //rule to get notification
            ITradeTrigger investor1Trigger = new TradeTrigger()
            {
                NotificationType = ReceiveNotificationType.Once,
                TargetPrice = 120.10,
                Sensitivity = StockSensitivityType.MeetAroundTargetPrice,
                FluctuationsRate = 0.2,
                TradeTriggerType = TradeTriggerType.SellingStock,
                StockPriceDirectionType = StockPriceDirectionType.EitherWay

            };

            IInvestor investor1 = new Investor("investor1", investor1Trigger);
            rbc.Attach(investor1);

            //rule to get notification
            ITradeTrigger investor2Trigger = new TradeTrigger()
            {
                NotificationType = ReceiveNotificationType.Unlimited,
                TargetPrice = 120.10,
                Sensitivity = StockSensitivityType.MeetAroundTargetPrice,
                FluctuationsRate = 0.2,
                TradeTriggerType = TradeTriggerType.SellingStock,
                StockPriceDirectionType = StockPriceDirectionType.EitherWay

            };

            IInvestor investor2 = new Investor("investor2", investor2Trigger);
            rbc.Attach(investor2);


            // Stock price getting change by users 

            rbc.Price = 120.00;
            rbc.Price = 121.20;
            rbc.Price = 119.10;
            rbc.Price = 120.19;

            List<INotification> investor1_notificationList = rbc.NotificationHistory.Where(h => h.Investor == investor1.InvestorName && h.TradeTriggerType == investor1Trigger.TradeTriggerType).ToList();
            List<INotification> investor2_notificationList = rbc.NotificationHistory.Where(h => h.Investor == investor2.InvestorName && h.TradeTriggerType == investor2Trigger.TradeTriggerType).ToList();



            Assert.IsNotNull(investor1_notificationList);
            Assert.IsNotNull(investor2_notificationList);
            Assert.IsTrue(investor1_notificationList.Count == 1);
            Assert.IsTrue(investor2_notificationList.Count == 1);

            Assert.AreEqual(investor1_notificationList.FirstOrDefault().Counter, 1);
            Assert.AreEqual(investor2_notificationList.FirstOrDefault().Counter, 2);

            Assert.AreEqual(investor1_notificationList.FirstOrDefault().TradeTriggerType, TradeTriggerType.SellingStock);
            Assert.AreEqual(investor2_notificationList.FirstOrDefault().TradeTriggerType, TradeTriggerType.SellingStock);

            Assert.AreNotEqual(investor1_notificationList.FirstOrDefault().TradeTriggerType, TradeTriggerType.BuyingStock);
            Assert.AreNotEqual(investor2_notificationList.FirstOrDefault().TradeTriggerType, TradeTriggerType.BuyingStock);



        }


        [TestMethod()]
        public void Test_RBC_Investor1_One_Trigger_Get_1_Notification_Investor2_Has_2Trigger_Get_Diffrent_Notification_for_Each_trigger()
        {

            IStock rbc = new RBC("RBC", 100.00);

            //rule to get notification
            ITradeTrigger investor1Trigger = new TradeTrigger()
            {
                TriggerName = "investor1_trigger1",
                NotificationType = ReceiveNotificationType.Once,
                TargetPrice = 120.10,
                Sensitivity = StockSensitivityType.MeetAroundTargetPrice,
                FluctuationsRate = 0.2,
                TradeTriggerType = TradeTriggerType.SellingStock,
                StockPriceDirectionType = StockPriceDirectionType.EitherWay

            };

            IInvestor investor1 = new Investor("investor1", investor1Trigger);
            rbc.Attach(investor1);

            //rule to get notification
            ITradeTrigger investor2Trigger1 = new TradeTrigger()
            {
                TriggerName = "investor2_trigger1",
                NotificationType = ReceiveNotificationType.Unlimited,
                TargetPrice = 120.10,
                Sensitivity = StockSensitivityType.MeetAroundTargetPrice,
                FluctuationsRate = 0.2,
                TradeTriggerType = TradeTriggerType.SellingStock,
                StockPriceDirectionType = StockPriceDirectionType.EitherWay

            };
            ITradeTrigger investor2Trigger2 = new TradeTrigger()
            {
                TriggerName = "investor2_trigger2",
                NotificationType = ReceiveNotificationType.Once,
                TargetPrice = 119.10,
                Sensitivity = StockSensitivityType.MeetExactTargetPrice,
                FluctuationsRate = 0.2,
                TradeTriggerType = TradeTriggerType.SellingStock,
                StockPriceDirectionType = StockPriceDirectionType.EitherWay

            };
            //adding list of 
            IInvestor investor2 = new Investor("investor2", new List<ITradeTrigger> { investor2Trigger1, investor2Trigger2 });
            rbc.Attach(investor2);
            


            // Stock price getting change by users 

            rbc.Price = 120.00;
            rbc.Price = 121.20;
            rbc.Price = 119.10;
            rbc.Price = 120.19;
            rbc.Price = 122.19;
            rbc.Price = 119.10;
            rbc.Price = 120.10;

            List<INotification> investor1_notificationList = rbc.NotificationHistory.Where(h => h.Investor == investor1.InvestorName && h.TradeTriggerType == investor1Trigger.TradeTriggerType).ToList();
            List<INotification> investor2_trigger1_notificationList = rbc.NotificationHistory.Where(h => h.Investor == investor2.InvestorName && h.TradeTriggerType == investor2Trigger1.TradeTriggerType && h.TriggerName == investor2Trigger1.TriggerName).ToList();
            List<INotification> investor2_trigger2_notificationList = rbc.NotificationHistory.Where(h => h.Investor == investor2.InvestorName && h.TradeTriggerType == investor2Trigger2.TradeTriggerType && h.TriggerName == investor2Trigger2.TriggerName).ToList();



            Assert.IsNotNull(investor1_notificationList);
            Assert.IsNotNull(investor2_trigger1_notificationList);
            Assert.IsNotNull(investor2_trigger2_notificationList);
            Assert.IsTrue(investor1_notificationList.Count == 1);
            Assert.IsTrue(investor2_trigger1_notificationList.Count == 1);
            Assert.IsTrue(investor2_trigger2_notificationList.Count == 1);

            Assert.AreEqual(investor1_notificationList.FirstOrDefault().Counter, 1);
            Assert.AreEqual(investor2_trigger1_notificationList.FirstOrDefault().Counter, 3);
            Assert.AreEqual(investor2_trigger2_notificationList.FirstOrDefault().Counter, 1);


        }


    }
}