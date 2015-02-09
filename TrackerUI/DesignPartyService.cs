using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Data;
using Tracker.Model;

namespace Tracker
{
    public class DesignPartyService : IPartyService
    {
        List<PartyModel> parties = new List<PartyModel>();

        public DesignPartyService()
        {
            Random r = new Random();

            this.parties = new List<PartyModel>();

            for (int i = 0; i < 10; i++)
            {
                parties.Add(new PartyModel()
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
        }

        public bool Add(Model.PartyModel party)
        {
            throw new NotImplementedException();
        }

        public bool DeletePartyById(int id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Model.PartyModel> GetAllActiveParties()
        {
            return new ObservableCollection<PartyModel>(this.parties);
        }

        public Model.PartyModel GetPartyById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Model.PartyModel party)
        {
            throw new NotImplementedException();
        }
    }
}
