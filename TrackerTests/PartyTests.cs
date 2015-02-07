using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tracker;
using System.Threading;
using Tracker.Model;

namespace TrackerTests
{
    [TestClass]
    public class PartyTests
    {
        [TestMethod]
        public void Create()
        {
            PartyModel p = new PartyModel(){
                Veh_Num = "demo", 
                ActualArrival = DateTime.Now.AddMinutes(30), 
                EstimatedArrival = DateTime.Now.AddMinutes(60)
            };

            Assert.IsNotNull(p);
            Assert.IsFalse(p.Status == PartyStatus.Warn);
            Assert.IsFalse(p.Status == PartyStatus.Overdue);
        }

        [TestMethod]
        public void TestWarnAndOverdue()
        {
            PartyModel p = new PartyModel()
            {
                Veh_Num = "demo",
                ActualArrival = DateTime.Now,
                EstimatedArrival = DateTime.Now.AddSeconds(5)
            };

            Assert.IsTrue(p.Status == PartyStatus.Warn);

            Thread.Sleep(6000);

            Assert.IsTrue(p.Status == PartyStatus.Overdue);
        }

        [TestMethod]
        public void TestWarnAndUpdate()
        {
            PartyModel p = new PartyModel()
            {
                Veh_Num = "demo",
                ActualArrival = DateTime.Now,
                EstimatedArrival = DateTime.Now.AddMinutes(5)
            };

            Assert.IsTrue(p.Status == PartyStatus.Warn);

            p.EstimatedArrival = DateTime.Now.AddHours(1);

            Assert.IsFalse(p.Status == PartyStatus.Warn);
        }

    }
}
