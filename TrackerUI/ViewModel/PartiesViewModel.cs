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
            this.AddPartyCommand = new RelayCommand(DoSave);
        }

        public RelayCommand CheckinParty { get; set; }

        public RelayCommand AddPartyCommand { get; set; }

        void Checkin(object param)
        {
            this.SelectedParty.Close();
        }

        void DoSave(object param)
        {
            
        }
    }
}
