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
    public class ViewModel : INotifyPropertyChanged
    {
        public SeriesCollection ChartDataSets { get; set; }
        public SeriesCollection LineChartDataSets { get; set; }
        public ObservableCollection<string> ColumnLabels { get; set; }
        public ObservableCollection<string> BarColumnLabels { get; set; }


        private IDataService dataService = new APIDataService();

        public ViewModel()
        {
            //InitializeData();
        }

        public void InitializeData()
        {
            var values = new ChartValues<DataModel>();
            var barValues = new ChartValues<DataModel>();

            IEnumerable<DataModel> data = dataService.GetData(0);

            foreach (var d in data)
            {
                values.Add(new DataModel() { Label = d.Label, Value = d.Value });
            }
            IEnumerable<DataModel> barData = values.Take(20);
            foreach (var d in barData)
            {
                barValues.Add(new DataModel() { Label = d.Label, Value = d.Value });
            }


            // Create a labels collection from the DataModel items
            this.ColumnLabels = new ObservableCollection<string>(values.Select(dataModel => dataModel.Label));
            this.BarColumnLabels = new ObservableCollection<string>(barValues.Select(dataModel => dataModel.Label));
            var dataMapper = new CartesianMapper<DataModel>()
              .Y(dataModel => dataModel.Value);
            //.Fill(dataModel => dataModel.Value > 15.0 ? Brushes.Red : Brushes.Green);

            this.ChartDataSets = new SeriesCollection
            {
                new ColumnSeries
                {
                Values = barValues,
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
