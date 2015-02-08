using System;
namespace Tracker.Data
{
    public interface IPartyService
    {
        bool Add(Tracker.Model.PartyModel party);
        bool DeletePartyById(int id);
        System.Collections.ObjectModel.ObservableCollection<Tracker.Model.PartyModel> GetAllActiveParties();
        Tracker.Model.PartyModel GetPartyById(int id);
        bool Update(Tracker.Model.PartyModel party);
    }
}
