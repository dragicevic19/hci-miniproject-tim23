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
using System.Collections.ObjectModel;

namespace MiniProjectHCI
{
    /// <summary>
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        public ViewModel vm { get; set; }
        public TableWindow(ViewModel d)
        {
            Title = "Economic Indicators [TABLE]";
            Uri iconUri = new Uri("../../../resources/economy.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

            InitializeComponent();
            this.DataContext = this;

            vm = d;
            

            String s = "naslov";
            headerValue.Header = "Values\t[" + vm.dataService.Unit.ToString()+ "]"; ;
        }


      




    }
}
