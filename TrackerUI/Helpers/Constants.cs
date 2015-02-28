using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Helpers
{
    // TODO:  this is crap - it should all be injected.  
    public static partial class Constants
    {
        public static int Minutes_Before_Notify;
        public static int Minutes_After_Alarm;

        public static string ActiveStorageLocation; 
        public static string ActiveStoreageDB;
        public static string ActiveStoreageDBConnection;

        static Constants()
        {
            ActiveStorageLocation = GetConfig("dbLocation", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TrackerApp"));

            // this might throw - if it does we'll have to rework it. 
            if (!Directory.Exists(ActiveStorageLocation))
            {
                Directory.CreateDirectory(ActiveStorageLocation);
            }

            ActiveStoreageDB = GetConfig("dbName", "PartyDB.db3");
            ActiveStoreageDBConnection = Path.Combine(ActiveStorageLocation, ActiveStoreageDB);

            Minutes_Before_Notify = GetConfig<int>("MinutesBeforeWarn", 20);
            Minutes_After_Alarm = GetConfig<int>("MinutesAfterAlarm", 15);
        }

        private static string GetConfig(string key, string defaultValue)
        {
            string configFromApp = ConfigurationManager.AppSettings.Get(key);
            if (string.IsNullOrWhiteSpace(configFromApp))
            {
                return defaultValue;
            }
            else
            {
                return configFromApp;
            }
        }

        private static T GetConfig<T>(string key, T defaultValue) where T : struct
        {
            string configFromApp = ConfigurationManager.AppSettings.Get(key);
            if (!string.IsNullOrWhiteSpace(configFromApp))
            {
                try
                {
                    return (T)Convert.ChangeType(configFromApp, typeof(T));
                    // this will throw if the value provided is not of the correct type
                    // TODO:  log this error?
                }
                catch { }
            }

            return defaultValue;
        }
    }
}
