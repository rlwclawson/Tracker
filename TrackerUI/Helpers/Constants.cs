using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Helpers
{
    public static partial class Constants
    {
        public static int Minutes_Before_Notify = 20;
        public static string ActiveStorageLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string ActiveStoreageDB = "PartyDB.db3";
        public static string ActiveStoreageDBConnection = Path.Combine(ActiveStorageLocation, ActiveStoreageDB);

        public static string ActiveStorageFile = Path.Combine(ActiveStorageLocation, "activeparties.json");

        public static bool SavedClosedRecords = true;
    }
}
