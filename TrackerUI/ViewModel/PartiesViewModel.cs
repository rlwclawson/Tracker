using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tracker.Data;
using Tracker.Helpers;
using Tracker.Model;

namespace Tracker.ViewModel
{
    public class PartiesViewModel : DependencyObject
    {
        // this needs to be injectable or you can't test this
        private static Database database = new Database(Constants.ActiveStoreageDBConnection);


        public static readonly DependencyProperty PartiesProperty =
            DependencyProperty.Register("Parties", typeof(ObservableCollection<PartyModel>), typeof(PartiesViewModel), new UIPropertyMetadata(null));

        public ObservableCollection<PartyModel> Parties
        {
            get { return (ObservableCollection<PartyModel>)GetValue(PartiesProperty); }
            set { SetValue(PartiesProperty, value); }
        }

        public static readonly DependencyProperty SelectedPartyProperty =
            DependencyProperty.Register("SelectedParty", typeof(PartyModel), typeof(PartiesViewModel), new UIPropertyMetadata(null));

        public PartyModel SelectedParty
        {
            get { return (PartyModel)GetValue(SelectedPartyProperty); }
            set { SetValue(PartiesProperty, value); }
        }

        public PartiesViewModel()
        {
            Parties = database.GetAllActiveParties();
            this.CheckinParty = new RelayCommand(Checkin);
            this.AddPartyCommand = new RelayCommand<Party>(DoSave);
            this.DeleteCommand = new RelayCommand(DoDelete);
			this.SaveCommand = new RelayCommand<Party>(DoSave);
        }

        public RelayCommand CheckinParty { get; set; }
        public RelayCommand<Party> AddPartyCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
		public RelayCommand<Party> SaveCommand { get; set; }

        void Checkin(object param)
        {
            this.SelectedParty.Close();
        }

        void DoSave(Party party)
        {
            if (SelectedParty.PartyId >= 0)
            {
                // this is an update
                database.Update(SelectedParty);
            }
            else
            {
                // this is new
                database.Add(SelectedParty);
            }
        }

        void DoDelete(object param)
        {
            if (SelectedParty.PartyId >= 0)
            {
                // this is a delete
                database.DeletePartyById(SelectedParty.PartyId);
            }
            else
            {
                // this is a cancel
                Parties.Remove(SelectedParty);
            }
        }
    }
}
