using SQLite.Net;
using SQLite.Net.Interop;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Tracker.Helpers;
using Tracker.Model;

namespace Tracker.Data
{
    public class DatabasePartyService : Tracker.Data.IPartyService
    {
        private static ISQLitePlatform Platform = new SQLite.Net.Platform.Win32.SQLitePlatformWin32();
        private string connectionString;

        private SQLiteConnection GetConnection()
        {
             SQLiteConnection connection = new SQLiteConnection(Platform, this.connectionString);
             return connection;
        }

        public DatabasePartyService(string connString)
        {
            if (string.IsNullOrWhiteSpace(connString))
            {
                throw new ArgumentNullException("connString");
            }

            this.connectionString = connString;

            // make sure DB is created  - also will validate the connectionString
            using (var connection = GetConnection())
            {
                connection.CreateTable<Party>(CreateFlags.AutoIncPK);
            }
        }

        public bool DeletePartyById(int id)
        {
            int result = -1;

            using (var connection = GetConnection())
            {
                result = connection.Delete<Party>(id);
            }

            return result > 0;
        }

        public PartyModel GetPartyById(int id)
        {
            Party party;

            using (var conn = GetConnection())
            {
                // pk search
                var x = conn.Get<Party>(id);
                party = x;
            }

            return Helpers.ModelHelpers.FromPoco(party);
        }

        public bool Add(PartyModel party)
        {
            int result = -1;

            Party p = ModelHelpers.FromModel(party);

            using (var connection = GetConnection())
            {
                result = connection.Insert(p);
            }

            // HACK:  this isn't right, but I need to move on
            party.PartyId = p.ID;

            // result is count of inserts
            return result > 0;
        }

        public bool Update(PartyModel party)
        {
            int result = -1;

            Party p = ModelHelpers.FromModel(party);

            using (var connection = GetConnection())
            {
                result = connection.Update(p);
            }

            return result > 0;
        }

        public ObservableCollection<PartyModel> GetAllActiveParties()
        {
            ObservableCollection<PartyModel> models;

            using (SQLiteConnection connection = GetConnection())
            {
                // BUGBUG:  expression compiler blows
                // var data = connection.Table<Party>().Where(p => p.Closed == false || p.ActualDeparture.Date == DateTime.Today);
                var data = connection.Table<Party>().Where(p => p.Closed == false);

                var items = data.ToList();

                models = new ObservableCollection<PartyModel>(items.ConvertAll(ModelHelpers.FromPoco));
            }

            return models;
        }

        public void ExecuteQuery(string txtQuery)
        {
            using (var connection = GetConnection())
            {
                var sql_cmd = connection.CreateCommand(txtQuery);
                sql_cmd.ExecuteNonQuery();
            }
        }
    }
}