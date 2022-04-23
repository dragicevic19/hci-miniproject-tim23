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

 
        private IDataService dataService;
 
        public ObservableCollection<DataModel> TableData { get; set; }

 

        public ViewModel(IDataService dataService)
        {
            this.dataService = dataService;
            ChartDataSets = new SeriesCollection();
            LineChartDataSets = new SeriesCollection();
            ColumnLabels = new ObservableCollection<string>();
            BarColumnLabels = new ObservableCollection<string>();
            TableData = new ObservableCollection<DataModel>();
        }
        public IEnumerable<DataModel> InitializeDataForTable(string function, string interval = "")
        {
            IEnumerable<DataModel> data = dataService.GetData(function, interval);
            return data;
        }


            public void InitializeData(string function, string interval="")
        {
            var values = new ChartValues<DataModel>();
            var barValues = new ChartValues<DataModel>();

            IEnumerable<DataModel> data = dataService.GetData(function, interval);

            foreach (var d in data)
            {
                values.Add(new DataModel() { Label = d.Label, Value = d.Value });
                TableData.Add(new DataModel() { Label = d.Label, Value = d.Value });
            }
            IEnumerable<DataModel> barData = values.Take(20);
            foreach (var d in barData)
            {
                barValues.Add(new DataModel() { Label = d.Label, Value = d.Value });
            }

            // ovde dodati i ucitavanje za tabele


            foreach (var v in values)
            {
                this.ColumnLabels.Add(v.Label);
            }
            foreach (var v in barValues)
            {
                this.BarColumnLabels.Add(v.Label);
            }

            var dataMapper = new CartesianMapper<DataModel>()
              .Y(dataModel => dataModel.Value);

            this.ChartDataSets.Add(
                new ColumnSeries
                {
                Values = barValues,
                Configuration = dataMapper
                }
            );

            var lineDataMapper = new CartesianMapper<DataModel>()
                .Y(dataModel => dataModel.Value);

            this.LineChartDataSets.Add(
                new LineSeries
                {
                    Values = values,
                    Configuration = lineDataMapper,
                    PointGeometry = null,
                    PointGeometrySize = 15
                }
            );
        }

        internal void Clear()
        {
            if (ChartDataSets.Count != 0)
            {
                this.ChartDataSets.Clear();
                this.LineChartDataSets.Clear();
                this.BarColumnLabels.Clear();
                this.ColumnLabels.Clear();
                this.TableData.Clear();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
