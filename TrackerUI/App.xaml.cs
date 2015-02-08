using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Tracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // demo data
        Data.Database data = new Data.Database(Tracker.Helpers.Constants.ActiveStoreageDBConnection);

        public App()
        {
            if (data.GetAllActiveParties().Count == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    data.Add(new Model.PartyModel()
                        {
                            Veh_Num = i.ToString(),
                            PartyRoute = 1,
                            ActualDeparture = DateTime.Now,
                            EstimatedArrival = DateTime.Now.AddHours(2),
                            PartyCount = 3,
                            Remarks = DateTime.Now.ToString()
                        });
                }
            }
        }
    }
}
