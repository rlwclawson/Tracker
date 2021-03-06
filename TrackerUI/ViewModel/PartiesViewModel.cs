﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
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
using System.Windows.Threading;
using Tracker.Data;
using Tracker.Helpers;
using Tracker.Model;

namespace Tracker.ViewModel
{
    public class PartiesViewModel : ViewModelBase 
    {
        private IPartyService dataService;
        private IDestinationService destService;

        [PreferredConstructor]
        public PartiesViewModel() : 
            this(
            (IsInDesignModeStatic ? (IPartyService) new DesignPartyService() : new DatabasePartyService(Constants.ActiveStoreageDBConnection)), 
            new DatabaseDestinationSevice(Constants.ActiveStoreageDBConnection)
            ) { }

        public PartiesViewModel(IPartyService dataService, IDestinationService destinationService)
        {
            if (dataService == null) throw new ArgumentNullException("dataService");
            if (dataService == null) throw new ArgumentNullException("destinationService");

            this.destService = destinationService;
            this.dataService = dataService;

            this.Parties = dataService.GetAllActiveParties();
            this.Destinations = new ObservableCollection<string>(destinationService.GetDestinations().Select(d => d.DestinationDesc));
        }

        public ObservableCollection<PartyModel> Parties
        {
            get;
            private set;
        }

        public ObservableCollection<String> Destinations
        {
            get;
            private set;
        }

        public string AlertSoundPath
        {
            get { return Constants.Alarm_Sound; }
        }

        private PartyModel selectedParty;
        public PartyModel SelectedParty
        {
            get { return this.selectedParty; }
            set
            {
               
                // update the embedded dependancy object
                if (Set(() => SelectedParty, ref selectedParty, value))
                {
                    this.SaveCommand.RaiseCanExecuteChanged();
                    this.DeleteCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private RelayCommand<PartyModel> checkinCommand;
        public RelayCommand<PartyModel> CheckinParty
        {
            get
            {
                this.SaveCommand.RaiseCanExecuteChanged();

                return this.checkinCommand ?? (
                    this.checkinCommand = new RelayCommand<PartyModel>(
                        party =>
                        {
                            party.Close();

                            this.SaveCommand.RaiseCanExecuteChanged();
                            this.DeleteCommand.RaiseCanExecuteChanged();
                        }));
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return this.addCommand ?? (
                    this.addCommand = new RelayCommand(
                    () =>
                    {
                        this.Parties.Add(new PartyModel());
                    }));
            }
        }
        private RelayCommand<PartyModel> deleteCommand;
        public RelayCommand<PartyModel> DeleteCommand
        {
            get
            {
                return this.deleteCommand ?? (
                    this.deleteCommand = new RelayCommand<PartyModel>(
                        party =>
                        {
                            if (party.PartyId >= 0)
                            {
                                // this is a delete - remove from the DB
                                dataService.DeletePartyById(party.PartyId);
                            }

                            // update the UI
                            Parties.Remove(party);
                        },
                        party =>
                        {
                            if (party == null || SelectedParty == null)
                            {
                                return false;
                            }

                            return true;
                        }));
            }
        }

        // TODO: can this be updated to save just the edited row?
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (
                    this.saveCommand = new RelayCommand(
                        () =>
                        {
                            foreach (var p in Parties)
                            {
                                if (p.IsDirty)
                                {
                                    p.IsDirty = false;

                                    if (p.PartyId >= 0)
                                    {
                                        this.dataService.Update(p);
                                    }
                                    else
                                    {
                                        this.dataService.Add(p);
                                    }
                                }
                            }

                            // reset all the selections 
                            this.SelectedParty = null;
                        },
                        () => 
                        {
                            return true;
                        //    foreach (var p in Parties)
                        //    {
                        //        if (p.IsDirty) return true;
                        //    }

                        //    return false;
                        }
                        ));
            }
        }
    }
}
