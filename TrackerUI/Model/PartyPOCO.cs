
using SQLite.Net.Attributes;
using System;

namespace Tracker.Model
{
    /// <summary>
    /// Party info POCO
    /// </summary>
    public class Party
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Veh_Num { get; set; }
        public string Destination { get; set; }
        public int PartyCount { get; set; }

        public DateTime ActualDeparture { get; set; }
        
        [Indexed]
        public DateTime EstimatedArrival { get; set; }
        public DateTime ActualArrival { get; set; }

        public string Remarks { get; set; }
        public bool Closed { get; set; }
    }

    /// <summary>
    /// Destination info POCO
    /// </summary>
    public class Destination
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string DestinationDesc { get; set; }
        public string Notes { get; set; }
        public bool RequiresDetail { get; set; }
    }
}
