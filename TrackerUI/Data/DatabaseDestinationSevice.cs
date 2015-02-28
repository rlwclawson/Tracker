using SQLite.Net;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
            return new ObservableCollection<Destination>(GetFromLocalFile());
        }

        private IList<Destination> GetFromDB()
        {
            ObservableCollection<Destination> data;

            using (var connection = GetConnection())
            {
                var dests = connection.Table<Destination>().ToList();
                data = new ObservableCollection<Destination>(dests);
            }

            return data;
        }

        private static List<Destination> GetFromLocalFile()
        {
            // file expected in /bin
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
            string loc = Path.Combine(path, "Locations.txt");

            List<Destination> toInsert = new List<Destination>();

            using (FileStream fs = new FileStream(loc, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {

                    while (!sr.EndOfStream)
                    {
                        string s = sr.ReadLine();

                        string[] siteInfo = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (siteInfo.Length == 2)
                        {
                            Destination d = new Destination()
                            {
                                DestinationDesc = siteInfo[0],
                                RequiresDetail = bool.Parse(siteInfo[1]),
                                Notes = string.Empty
                            };

                            toInsert.Add(d);
                        }
                    }
                }
            }

            return toInsert;

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
