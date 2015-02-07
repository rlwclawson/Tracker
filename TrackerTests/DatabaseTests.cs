using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Tracker;
using SQLite;
using SQLite.Net;
using SQLite.Net.Platform.Win32;
using Tracker.Data;
using Tracker.Helpers;
using Tracker.Model;

namespace TrackerTests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void VerifyDbInsert()
        {
            var pm = new PartyModel()
            {
                ActualDeparture = DateTime.Now,
                Closed = false,
                EstimatedArrival = DateTime.Now.AddHours(1),
                PartyCount = 3,
                PartyRoute = 1,
                ActualArrival = DateTime.MinValue,
                Remarks = "try this",
                Veh_Num = "def"
            };

            var newParty = Database.Add(pm);

            Assert.IsTrue(newParty > 0);

            var p = Database.GetById(newParty);

            Assert.IsNotNull(p);

            Assert.AreEqual(p.Veh_Num, pm.Veh_Num);

        }
    }
}
