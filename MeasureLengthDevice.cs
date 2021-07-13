using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DataCollector
{
    class MeasureLengthDevice : IMeasuringDevice
    {
        //Object that gets random number.
        Device myDevice = new Device();
        //Queue that holds measurement history.
        FixedQueue<int> dataCaptured = null;
        //Timer that determines when to get a new measurement.
        private Timer timer;
        //Units in enmu to determine conversion.
        private Units unitsToUse;
        //Holds most recent random measurement.
        private int mostRecentMeasure = 0;    
       
        //Allows access to private method PrintValues.
        public string History => PrintValues(dataCaptured);

        //Constructor that initializes fields of MeasureLengthDevice class and assigns them to variables.
        public MeasureLengthDevice(Units unitsToUse)
        {
            this.unitsToUse = unitsToUse;
            dataCaptured = new FixedQueue<int>();
            dataCaptured.Limit = 10;

        }
        //Accepts int Queue and changes it to a string.
        private string PrintValues(FixedQueue<int> myCollection)
        {
            StringBuilder myString = new StringBuilder();
            
            foreach (var i in myCollection.q)
            {
                myString.AppendLine(i.ToString());
            }
            return myString.ToString();
        }
        //Returns imperial value or converts and returns metric value.
        public decimal ImperialValue()
        {
            if (unitsToUse == Units.Metric)
            {
                return (decimal)(mostRecentMeasure * .393701);
            }
            return mostRecentMeasure;
        }

        //Returns metric value or converts and returns imperial value.
        public decimal MetricValue()
        {
            if (unitsToUse == Units.Imperial)
            {
                return (decimal)(mostRecentMeasure * 2.54);
            }
            return mostRecentMeasure;

        }

        //Stop timer.
        public void StopCollecting()
        {
            //Stop the timer that started in StartCollecting()
            timer.Dispose();
        }

        //Start timer.
        public void StartCollecting()
        {
            
            timer = new Timer(timer_Tick, null, (int)TimeSpan.FromSeconds(1).TotalMilliseconds, (int)TimeSpan.FromSeconds(15).TotalMilliseconds);
        }

        //Method runs every 15 seconds.
        private async void timer_Tick(object state)
        {

            //Gets random measurement and adds it to queue.
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                mostRecentMeasure = myDevice.GetMeasurement;
                dataCaptured.Enqueue(mostRecentMeasure);

            });
        }

    }
}

