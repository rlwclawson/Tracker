using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SQLite.Net;
using Tracker.Model;
using SQLite.Net.Interop;
using Tracker.Helpers;
using System.Collections.ObjectModel;

namespace Tracker.Data
{
    public static class Database
    {
        private static ISQLitePlatform Platform = new SQLite.Net.Platform.Win32.SQLitePlatformWin32();

        private static SQLiteConnection GetConnection()
        {
             SQLiteConnection connection = new SQLiteConnection(Platform, Constants.ActiveStoreageDBConnection);
             return connection;
        }

        static Database()
        {
            // make sure DB is created
            using (SQLiteConnection connection = GetConnection())
            {
                connection.CreateTable<Party>(CreateFlags.AutoIncPK);
                connection.CreateTable<Destination>(CreateFlags.AutoIncPK);
            }
        }

        public static PartyModel GetById(int id)
        {
            Party party;

            using (var conn = GetConnection())
            {
                // pk search
                var x = conn.Get<Party>(1);
                party = x;
            }

            return Helpers.ModelHelpers.FromPoco(party);
        }

        public static int Add(PartyModel party)
        {
            int result = -1;

            Party p = ModelHelpers.FromModel(party);

            using (var connection = GetConnection())
            {
                result = connection.Insert(p);
            }

            // returns the PK
            return result;
        }

        public static ObservableCollection<PartyModel> GetAllActiveParties()
        {
            ObservableCollection<PartyModel> models;

            using (SQLiteConnection connection = GetConnection())
            {
                var data = connection.Table<Party>().Where(p => !p.Closed);
                var items = data.ToList();

                models = new ObservableCollection<PartyModel>(items.ConvertAll(ModelHelpers.FromPoco));
            }

            return models;
        }

        private static void ExecuteQuery(string txtQuery)
        {
            using (var connection = GetConnection())
            {
                var sql_cmd = connection.CreateCommand(txtQuery);
                sql_cmd.ExecuteNonQuery();
            }
        }
    }
}
