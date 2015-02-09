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
            ObservableCollection<Destination> data;

            using (var connection = GetConnection())
            {
                var dests = connection.Table<Destination>().ToList();
                data = new ObservableCollection<Destination>(dests);
            }

            return data;
        }
    }
}
