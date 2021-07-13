using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace DataCollector
{
    class Device
    {
        //Determines when to generate random number.
        private Timer timer;
        //Holds current time stamp.
        private DateTime dateTime;
        //Random number/measurement generated.
        private int data = 0;
       
        //Create random object.
        Random random = new Random();

        public Device()
        {
            //Runs timer_tick method every second.
            timer = new Timer(timer_Tick, null, (int)TimeSpan.FromSeconds(1).TotalMilliseconds, (int)TimeSpan.FromSeconds(1).TotalMilliseconds);
        }
        //Method that generates random number and time stamp.
        private async void timer_Tick(object state)
        {
            
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                //Call Random Next method and assign to int variable randomMeasurement.
                data = random.Next(1, 11);
                dateTime = DateTime.Now; 
            });
        }
        //Returns random number.
        public int GetMeasurement => data;
        

    }
}
