using SQLite.Net;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Helpers;
using Tracker.Model;

namespace DataInit
{
    class Program
    {
        static void Main(string[] args)
        {
            // go direct to the db
            ISQLitePlatform Platform = new SQLite.Net.Platform.Win32.SQLitePlatformWin32();
            var connection = new SQLiteConnection(Platform, Constants.ActiveStoreageDBConnection);


            string[] sites = Destinations.Sites.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            List<Destination> toInsert = new List<Destination>();

            bool errors = false;

            if (sites.Length > 0)
            {
                foreach (var s in sites)
                {
                    Console.WriteLine(s);

                    string[] siteInfo = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (siteInfo.Length == 2)
                    {
                        Destination d = new Destination()
                        {
                            DestinationDesc = siteInfo[0],
                            RequiresDetail = bool.Parse(siteInfo[1]),
                            Notes = string.Empty
                        };

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Inserting destination: {0}", s);

                        toInsert.Add(d);

                        //connection.Insert(d);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Unable to parse site info from string: {0}", s);
                        Console.ForegroundColor = ConsoleColor.White;

                        errors = true;
                    }
                }
            }

            if (errors)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Errors occured, please review and fix, update failed");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                // make sure the table exists, then clear all the existing items
                connection.CreateTable<Destination>(CreateFlags.AutoIncPK);
                connection.DeleteAll<Destination>();

                foreach (var site in toInsert)
                {
                    connection.Insert(site);
                }
            }
        }
    }
}
