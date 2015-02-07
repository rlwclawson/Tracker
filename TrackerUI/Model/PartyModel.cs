using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracker.Helpers;

namespace Tracker.Model
{
    public enum PartyStatus
    {
        Closed,
        OK,
        Warn,
        Overdue
    }

    public class PartyModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public PartyModel()
        {
            // Set some defaults for a UI created new item - this will be overwritten by anything loaded
            this.EstimatedArrival = Round(DateTime.Now.AddHours(1));
            this.ActualDeparture = DateTime.Now;
        }

        private static DateTime Round(DateTime original)
        {
            return new DateTime(original.Year, original.Month, original.Day, original.Hour, 0, 0);
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
                this.partyId = value;
                this.RaiseChangeEvent("PartyId");
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
                this.vehNum = value;
                this.RaiseChangeEvent("Veh_Num");
            }
        }

        private int partyRoute;
        public int PartyRoute
        {
            get
            {
                return this.partyRoute;
            }
            set
            {
                this.partyRoute = value;
                this.RaiseChangeEvent("PartyRoute");
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
                this.partyCount = value;
                this.RaiseChangeEvent("PartyCount");
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
                this.actualDeparture = value;
                this.RaiseChangeEvent("ActualDeparture");
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
                this.actualArrival = value;
                this.RaiseChangeEvent("ActualArrival");
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
                this.remarks = value;
                this.RaiseChangeEvent("Remarks");
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
                this.eta = value;
                this.RaiseChangeEvent("EstimatedArrival");
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
                this.closed = value;
                this.RaiseChangeEvent("Closed");
            }
        }

        public PartyStatus Status
        {
            get
            {
                if (this.Closed) return PartyStatus.Closed;
                else if (DateTime.Now > EstimatedArrival) return PartyStatus.Overdue;
                else if (DateTime.Now > EstimatedArrival - TimeSpan.FromMinutes(Constants.Minutes_Before_Notify)) return PartyStatus.Warn;
                else return PartyStatus.OK;
            }
        }

        public void Close()
        {
            this.ActualArrival = DateTime.Now;
            this.Closed = true;
        }

        private void RaiseChangeEvent(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
                this.PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }
    }
}
