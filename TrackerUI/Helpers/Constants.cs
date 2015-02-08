using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Helpers
{
    public static partial class Constants
    {
        public static int Minutes_Before_Notify = GetConfig<int>("MinutesBeforeWarn", 20);

        public static string ActiveStorageLocation = GetConfig("dbLocation",  Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
        public static string ActiveStoreageDB = GetConfig("dbName", "PartyDB.db3");
        public static string ActiveStoreageDBConnection = Path.Combine(ActiveStorageLocation, ActiveStoreageDB);

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

        //private static bool GetConfig(string key, bool defaultValue)
        //{
        //    string configFromApp = ConfigurationManager.AppSettings.Get(key);
        //    if (string.IsNullOrWhiteSpace(configFromApp))
        //    {
        //        return defaultValue;
        //    }
        //    else
        //    {
        //        return bool.Parse(configFromApp);
        //    }
        //}

        //private static int GetConfig(string key, int defaultValue) 
        //{
        //    string configFromApp = ConfigurationManager.AppSettings.Get(key);
        //    if (string.IsNullOrWhiteSpace(configFromApp))
        //    {
        //        return defaultValue;
        //    }
        //    else
        //    {
        //        return int.Parse(configFromApp);
        //    }
        //}
    }
}
