using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectHCI
{
    public class DataModel : INotifyPropertyChanged
    {

        private string label;
        public string Label
        {
            get => this.label;
            set
            {
                this.label = value;
                OnPropertyChanged();
            }
        }


        private double value;
        public double Value
        {
            get => this.value;
            set
            {
                this.value = value;
                OnPropertyChanged();
            }
        }

      

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
