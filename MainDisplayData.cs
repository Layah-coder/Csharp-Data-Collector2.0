using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector
{

    //Updates UI when data changes.
    class MainDisplayData : INotifyPropertyChanged
    {
        //Holds current measurement.
        private string measurement;
        //Holds current history.
        private string history;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //Checks if history has changed then updates UI.
        public string History
        {
            get { return this.history; }
            set
            {
                if (history != value)
                {
                    history = value;
                    this.OnPropertyChanged();
                }
            }
        }

        //Check if measurement changed then updates UI.
        public string Measurement
        {
            get { return this.measurement; }
            set
            {
                if (measurement != value)
                {
                    measurement = value;
                    this.OnPropertyChanged();
                }
            }
        }
        
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
