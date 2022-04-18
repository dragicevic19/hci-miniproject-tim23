using MiniProjectHCI.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MiniProjectHCI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            APIDataService dataService = new APIDataService();

            var result = await dataService.GetData();

            base.OnStartup(e);
        }
    }
}
