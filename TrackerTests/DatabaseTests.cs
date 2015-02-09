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
using SQLite.Net.Interop;

namespace TrackerTests
{
    [TestClass]
    public class DatabaseTests
    {
        private DatabasePartyService DB;

        [ClassInitialize]
        private static void Init()
        {
            
        }

        [TestInitialize]
        public void TestInit()
        {
            DB = new DatabasePartyService(Constants.ActiveStoreageDBConnection);
            Assert.IsNotNull(DB);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ISQLitePlatform Platform = new SQLite.Net.Platform.Win32.SQLitePlatformWin32();
            using (var connection = new SQLiteConnection(Platform, Constants.ActiveStoreageDBConnection))
            {
                // clean up the DB
                connection.DeleteAll<Party>();
            }
        }

        [TestMethod]
        public void VerifyDbOperations()
        {
            var pm = GetModel(remarks: "try this", vehId: "def");

            // create
            var newParty = DB.Add(pm);
            Assert.IsTrue(newParty);

            // read
            var p = DB.GetPartyById(pm.PartyId);
            Assert.IsNotNull(p);
            Assert.AreEqual(p.Veh_Num, pm.Veh_Num);

            // update
            pm.EstimatedArrival = DateTime.Now.AddHours(6);
            DB.Update(pm);
            var p_udpated = DB.GetPartyById(pm.PartyId);
            Assert.IsNotNull(p_udpated);

            Assert.AreEqual(pm.EstimatedArrival.Year, p_udpated.EstimatedArrival.Year);
            Assert.AreEqual(pm.EstimatedArrival.Month, p_udpated.EstimatedArrival.Month);
            Assert.AreEqual(pm.EstimatedArrival.Day, p_udpated.EstimatedArrival.Day);
            Assert.AreEqual(pm.EstimatedArrival.Hour, p_udpated.EstimatedArrival.Hour);
            Assert.AreEqual(pm.EstimatedArrival.Minute, p_udpated.EstimatedArrival.Minute);
            Assert.AreEqual(pm.EstimatedArrival.Second, p_udpated.EstimatedArrival.Second);

            // delete
            bool delResult = DB.DeletePartyById(pm.PartyId);
            Assert.IsTrue(delResult);
        }

        [TestMethod]
        public void TestGetAll()
        {
            for (int i = 0; i < 10; i++)
            {
                var mod = GetModel(vehId: i.ToString());
                Assert.IsTrue(DB.Add(mod));
            }

            var allParties = DB.GetAllActiveParties();
            Assert.AreEqual(allParties.Count, 10);

            var pm = allParties[0];

            pm.Close();

            DB.Update(pm);

            var nowParties = DB.GetAllActiveParties();
            Assert.AreEqual(nowParties.Count, 9);

        }

        private static PartyModel GetModel(string vehId = "abc", int partyCount = 3, string partyDestination = "A destination", string remarks = "", bool closed = false)
        {
            var pm = new PartyModel( )
            {
                ActualDeparture = DateTime.Now,
                Closed = closed,
                EstimatedArrival = DateTime.Now.AddHours(1),
                PartyCount = partyCount,
                Destination = partyDestination,
                ActualArrival = DateTime.MinValue,
                Remarks = remarks,
                Veh_Num = vehId
            };

            return pm;
        }
    }
}
