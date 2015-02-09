using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Model
{
    public class DestinationModel : ObservableObject
    {
        public int Id { get; set; }
        public string Destination { get; set; }
        public string Notes { get; set; }
    }
}
