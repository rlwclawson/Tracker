using System;
using Tracker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tracker.ViewModel;
using Tracker.Model;

namespace TrackerTests
{
    [TestClass]
    public class PartiesTests
    {
        [TestMethod]
        public void Init()
        {
            PartiesViewModel p = new PartiesViewModel();

            PartyModel pm = new PartyModel()
            {
                Veh_Num = "demo",
                ActualArrival = DateTime.Now,
                EstimatedArrival = DateTime.Now.AddMinutes(1)
            };

            p.Parties.Add(pm);

            Assert.AreEqual(1, p.Parties.Count);
        }
    }
}
