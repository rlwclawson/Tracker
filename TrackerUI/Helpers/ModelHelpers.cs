using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Model;

namespace Tracker.Helpers
{
    public static class ModelHelpers
    {
        public static PartyModel FromPoco(Party party)
        {
            if (party == null) return null;

            PartyModel p = new PartyModel()
            {
                PartyCount = party.PartyCount,
                PartyRoute = party.Destination,
                Remarks = party.Remarks,
                ActualArrival = party.ActualArrival,
                EstimatedArrival = party.EstimatedArrival,
                Closed = party.Closed,
                ActualDeparture = party.ActualDeparture,
                Veh_Num = party.Veh_Num,
                PartyId = party.ID,
            };

            return p;
        }

        public static Party FromModel(PartyModel model)
        {
            if (model == null) return null;

            Party p = new Party()
            {
                // if this is a new party, don't copy the id
                ID = model.PartyId > 0 ? model.PartyId : 0,
                PartyCount = model.PartyCount,
                Destination = model.PartyRoute,
                Remarks = model.Remarks,
                ActualArrival = model.ActualArrival,
                EstimatedArrival = model.EstimatedArrival,
                Closed = model.Closed,
                ActualDeparture = model.ActualDeparture,
                Veh_Num = model.Veh_Num
            };

            return p;
        }
    }
}
