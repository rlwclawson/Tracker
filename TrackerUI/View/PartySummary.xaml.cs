using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tracker.ViewModel;

namespace Tracker.View
{
    /// <summary>
    /// Interaction logic for PartySummary.xaml
    /// </summary>
    public partial class PartySummary : Window
    {
        public PartySummary()
        {
            InitializeComponent();
        }

        // this is a hack to signal the property change in the "Destination" field from the ComboBox update
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = (PartiesViewModel)DataContext;

            if (vm == null || vm.SelectedParty == null) return;

            vm.SelectedParty.Destination = (string)((ComboBox)e.Source).SelectedValue;
        }
    }
}
