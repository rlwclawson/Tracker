
namespace Tracker.Model
{
    using GalaSoft.MvvmLight;
    using System;
    using System.Threading;
    using Tracker.Helpers;

    public enum PartyStatus
    {
        Closed,
        OK,
        Warn,
        Overdue
    }

    public class PartyModel : ObservableObject
    {
        Timer updateStatus;

        public PartyModel()
        {
            // Set some defaults for a UI created new item - this will be overwritten by anything loaded
            // set field to avoid raising property changed events

            this.isDirty = false;
            this.eta = Round(DateTime.Now.AddHours(2));
            this.actualDeparture = DateTime.Now;
            this.partyId = -1;

            // i dunno about this, but I can't think of anything better right now.
            updateStatus = new Timer((e) => { this.RaisePropertyChanged(() => Status); }, this, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
        }

        private static DateTime Round(DateTime original)
        {
            return new DateTime(original.Year, original.Month, original.Day, original.Minute > 30 ? original.Hour + 1 : original.Hour , 0, 0);
        }

        private bool isDirty;
        public bool IsDirty
        {
            get
            {
                return this.isDirty;
            }
            set
            {
                Set(() => IsDirty, ref this.isDirty, value);
            }
        }

        private int partyId;
        public int PartyId
        {
            get
            {
                return this.partyId;
            }
            set
            {
                if (Set(() => PartyId, ref this.partyId, value))
                {
                    this.IsDirty = true;
                }
            }
        }

        private string vehNum;
        public string Veh_Num
        {
            get
            {
                return this.vehNum;
            }
            set
            {
                if (Set(() => Veh_Num, ref this.vehNum, value))
                {
                    this.IsDirty = true;
                }
            }
        }

        private string destination;
        public string Destination
        {
            get
            {
                return this.destination;
            }
            set
            {
                if (Set(() => this.Destination, ref this.destination, value))
                {
                    this.IsDirty = true;
                }
            }
        }

        private int partyCount;
        public int PartyCount
        {
            get
            {
                return this.partyCount;
            }
            set
            {
                if (Set(() => this.PartyCount, ref this.partyCount, value))
                {
                    this.IsDirty = true;
                }
            }
        }

        private DateTime actualDeparture;
        public DateTime ActualDeparture
        {
            get
            {
                return this.actualDeparture;
            }
            set
            {
                if (Set(() => this.ActualDeparture, ref this.actualDeparture, value))
                {
                    this.IsDirty = true;
                }
            }
        }

        private DateTime actualArrival;
        public DateTime ActualArrival
        {
            get
            {
                return this.actualArrival;
            }
            set
            {
                if (Set(() => this.ActualArrival, ref this.actualArrival, value))
                {
                    this.IsDirty = true;
                }
            }
        }

        private string remarks;
        public string Remarks
        {
            get
            {
                return this.remarks;
            }
            set
            {
                if (Set(() => this.Remarks, ref this.remarks, value))
                {
                    this.IsDirty = true;
                }
            }
        }

        private DateTime eta;
        public DateTime EstimatedArrival
        {
            get
            {
                return this.eta;
            }
            set
            {
                if (Set(() => this.EstimatedArrival, ref this.eta, value))
                {
                    this.IsDirty = true;
                    this.RaisePropertyChanged(() => Status);
                }
            }
        }

        private bool closed;
        public bool Closed
        {
            get
            {
                return this.closed;
            }
            set
            {
                if (Set(() => this.Closed, ref this.closed, value))
                {
                    this.IsDirty = true;
                    this.RaisePropertyChanged(() => Status);
                }
            }
        }

        public PartyStatus Status
        {
            get
            {
                if (this.Closed) return PartyStatus.Closed;
                else if (DateTime.Now > EstimatedArrival + TimeSpan.FromMinutes(Constants.Minutes_After_Alarm)) return PartyStatus.Overdue;
                else if (DateTime.Now > EstimatedArrival - TimeSpan.FromMinutes(Constants.Minutes_Before_Notify)) return PartyStatus.Warn;
                else return PartyStatus.OK;
            }
        }

        public void Close()
        {
            this.ActualArrival = DateTime.Now;
            if (string.IsNullOrWhiteSpace(this.remarks))
            {
                this.Remarks = string.Format("Party returned at {0:f}", this.ActualArrival); 
            }
            else
            {
                this.Remarks += string.Format(" Party returned at {0:f}", this.ActualArrival);
            }

            this.Closed = true;
        }
    }
}
