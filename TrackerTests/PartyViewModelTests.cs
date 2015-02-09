using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using Tracker.Data;
using Tracker.Model;
using Tracker.ViewModel;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace TrackerTests
{
    [TestClass]
    public class PartyViewModelTests
    {
        IPartyService data;
        IDestinationService destinations;

        [TestInitialize]
        public void TestInit()
        {
            Random r = new Random();

            this.data = new TestDataService();
            for (int i = 0; i < 10; i++)
            {
                this.data.Add(new PartyModel()
                {
                    PartyId = i,
                    Veh_Num = "veh " + i,
                    ActualDeparture = DateTime.Now,
                    EstimatedArrival = DateTime.Now.AddHours(r.Next(10)),
                    PartyCount = r.Next(8),
                    Destination = r.Next(10).ToString(),
                    Remarks = string.Format("This is party {0}, departed at {1}", "veh " + i, DateTime.Now),
                    Closed = false
                });
            }

            this.destinations = new TestDestinationService();

            this.changedProperty = string.Empty;
        }

        [TestMethod]
        public void TestConstructor()
        {
            PartiesViewModel vm = new PartiesViewModel(this.data, this.destinations);
            Assert.IsNotNull(vm);
        }

        [TestMethod]
        public void TestUpdateSelectedParty()
        {
            PartiesViewModel vm = new PartiesViewModel(this.data, this.destinations);
            vm.PropertyChanged += vm_PropertyChanged;

            vm.SelectedParty = vm.Parties[0];

            Assert.AreEqual("SelectedParty", this.changedProperty);
        }

        [TestMethod]
        public void TestUpdate()
        {
            PartiesViewModel vm = new PartiesViewModel(this.data, this.destinations);
            //vm.Parties.CollectionChanged += Parties_CollectionChanged;
            vm.Parties.CollectionChanged += (o, e) =>
            {
                Assert.AreEqual(e.Action, NotifyCollectionChangedAction.Replace);
                
            };

            vm.Parties[0].Close();

            // ??
        }

        private string changedProperty;

        void vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.changedProperty = e.PropertyName;
        }

        private class TestDestinationService : IDestinationService
        {
            public ObservableCollection<Destination> GetDestinations()
            {
                Random r = new Random();

                var sites = new ObservableCollection<Destination>();

                for (int i = 0; i < 10; i++)
                {
                    sites.Add(new Destination()
                    {
                        DestinationDesc = "Site " + i,
                        ID = i,
                        RequiresDetail = r.Next() % 5 == 0,
                        Notes = string.Format("I am a site")
                    });
                }

                return sites;
            }
        }

        private class TestDataService : IPartyService
        {
            Dictionary<int, PartyModel> parties = new Dictionary<int, PartyModel>();

            public bool Add(PartyModel party)
            {
                this.parties.Add(party.PartyId, party);
                return true;
            }

            public bool DeletePartyById(int id)
            {
                return this.parties.Remove(id);
            }

            public ObservableCollection<PartyModel> GetAllActiveParties()
            {
                return new ObservableCollection<PartyModel>(this.parties.Values.Where(pm => pm.Closed == false));
            }

            public PartyModel GetPartyById(int id)
            {
                return this.parties[id];
            }

            public bool Update(PartyModel party)
            {
                this.parties[party.PartyId] = party;
                return true; // can't fail unless it throws
            }
        }
    }
}
