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

namespace MiniProjectHCI
{
    /// <summary>
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        public ViewModel Data { get; set; } // a u InitializeData pravis neku novu listu za podatke za tabelu

        public TableWindow(ViewModel data)
        {
            InitializeComponent();
            this.Data = data;
        }
    }
}
