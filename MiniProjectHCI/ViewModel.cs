using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using MiniProjectHCI.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectHCI
{
    class ViewModel : INotifyPropertyChanged
    {
        public SeriesCollection ChartDataSets { get; set; }
        public SeriesCollection LineChartDataSets { get; set; }
        public ObservableCollection<string> ColumnLabels { get; set; }

        private IDataService dataService = new APIDataService();

        public ViewModel()
        {
            InitializeBarChartDataAsync();
        }

        private void InitializeBarChartDataAsync()
        {
            var values = new ChartValues<DataModel>();

            IEnumerable<DataModel> data = (IEnumerable<DataModel>)dataService.GetData();

            foreach (var d in data)
            {
                values.Add(new DataModel() { Label = d.Label, Value = d.Value });
            }

            // Create a labels collection from the DataModel items
            this.ColumnLabels = new ObservableCollection<string>(values.Select(dataModel => dataModel.Label));

            var dataMapper = new CartesianMapper<DataModel>()
              .Y(dataModel => dataModel.Value);
            //.Fill(dataModel => dataModel.Value > 15.0 ? Brushes.Red : Brushes.Green);

            this.ChartDataSets = new SeriesCollection
            {
                new ColumnSeries
                {
                Values = values,
                Configuration = dataMapper
                }
            };

            var lineDataMapper = new CartesianMapper<DataModel>()
                .Y(dataModel => dataModel.Value);

            this.LineChartDataSets = new SeriesCollection
            {
                new LineSeries
                {
                    Values = values,
                    Configuration = lineDataMapper,
                    PointGeometry = null,
                    PointGeometrySize = 15
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
