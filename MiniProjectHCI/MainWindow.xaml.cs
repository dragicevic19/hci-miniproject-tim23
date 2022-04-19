using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiniProjectHCI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel Data {get; set;}

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Economic Indicators";
            // ikonica
            Uri iconUri = new Uri("../../../resources/economy.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            
            DataContext = this;
            Data = new ViewModel();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string function = DataTypeCombo.SelectedValue.ToString().Split(':').Last().Trim();
            string interval = (function == "CPI") ? IntervalCombo.SelectedValue.ToString().Split(':').Last().Trim() : "";
            this.Data.Clear();
            this.Data.InitializeData(function, interval);
        }

        private void CPIComboSelected(object sender, RoutedEventArgs e)
        {
            if (IntervalLabel != null && IntervalCombo != null)
            {
                IntervalLabel.Visibility = Visibility.Visible;
                IntervalCombo.Visibility = Visibility.Visible;
            }
        }

        private void InflationOrSalesComboSelected(object sender, RoutedEventArgs e)
        {
            if (IntervalLabel != null && IntervalCombo != null) {

                IntervalLabel.Visibility = Visibility.Hidden;
                IntervalCombo.Visibility = Visibility.Hidden;
            }
            
        }
    }
}
