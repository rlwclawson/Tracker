using SQLite.Net;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Model;

namespace Tracker.Data
{
    public class DatabaseDestinationSevice : IDestinationService
    {
        private static ISQLitePlatform Platform = new SQLite.Net.Platform.Win32.SQLitePlatformWin32();
        private string connectionString;

        private SQLiteConnection GetConnection()
        {
            SQLiteConnection connection = new SQLiteConnection(Platform, this.connectionString);
            return connection;
        }

        public DatabaseDestinationSevice(string connString)
        {
            if (string.IsNullOrWhiteSpace(connString))
            {
                throw new ArgumentNullException("connString");
            }

            this.connectionString = connString;

            // make sure DB is created  - also will validate the connectionString
            using (var connection = GetConnection())
            {
                connection.CreateTable<Destination>(CreateFlags.AutoIncPK);
            }
        }

        public ObservableCollection<Destination> GetDestinations()
        {
            //ObservableCollection<Destination> data;

            //using (var connection = GetConnection())
            //{
            //    var dests = connection.Table<Destination>().ToList();
            //    data = new ObservableCollection<Destination>(dests);
            //}

            //return data;

            return new ObservableCollection<Destination>(GetHardCoded());
        }

        private static List<Destination> GetHardCoded()
        {
            List<Destination> dests = new List<Destination>();
            dests.Add(new Destination()
            {
                DestinationDesc = "Dundas",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "North Mtn",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "South Mtn",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "12 SWS / BMEWS",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "D-Launch",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "Back beach / D-launch",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "Secret Place / North Mtn",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "ICE wall by BMEWS",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "Camp Tuto",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "Waterfall/Uvdle",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "P-MTN",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "CE Cabin",
                RequiresDetail = true
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "Mountain Meadow / North Mtn",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "C-Launch",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "Other",
                RequiresDetail = true
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "Icecaves towards Cape Atholl",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "Cape Atholl",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "Green valley",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "Station North / RDAF flights",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "CFS Alert / CAF flights",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "NASA flights",
                RequiresDetail = false
            });
            dests.Add(new Destination()
            {
                DestinationDesc = "Off defense area",
                RequiresDetail = true
            });

            return dests;
        }
    }
}
