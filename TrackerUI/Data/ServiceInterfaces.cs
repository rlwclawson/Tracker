using System;
using System.Collections.ObjectModel;
using Tracker.Model;
namespace Tracker.Data
{
    public interface IPartyService
    {
        bool Add(PartyModel party);
        bool DeletePartyById(int id);
        ObservableCollection<PartyModel> GetAllActiveParties();
        PartyModel GetPartyById(int id);
        bool Update(PartyModel party);
    }

    public interface IDestinationService
    {
        ObservableCollection<Destination> GetDestinations();
    }
}
