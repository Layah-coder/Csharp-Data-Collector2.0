using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using System.Text;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using System.Threading;

/*
4/23/2021 
Leah Oswald
Program that generates random measurements and allows user to convert them to metric or imperial.
Also, program stores some of the past data in an array.
*/

namespace DataCollector
{

    public sealed partial class MainPage : Page
    {
        //Objects.
        private Timer timer;
        MeasureLengthDevice measureDevice = null;
        MainDisplayData displayData = null;
        //Sets units for measure.
        Units myUnits = Units.Imperial;


        public MainPage()
        {
            this.InitializeComponent();
            //Create new object and pass it imperial units.
            measureDevice = new MeasureLengthDevice(myUnits);
            //Create new object that updates history when property is changed.
            displayData = new MainDisplayData
            {
                History = measureDevice.History
            };
            //Binds to UI.
            DataContext = displayData;
        }

        private void startClick(object sender, RoutedEventArgs e)
        {
            //Start collecting data and timer.
            measureDevice.StartCollecting();
            timer = new Timer(timer_Tick, null, (int)TimeSpan.FromSeconds(1).TotalMilliseconds, (int)TimeSpan.FromSeconds(15).TotalMilliseconds);

        }

        private void stopClick(object sender, RoutedEventArgs e)
        {
            //Stop collecting data and dispose of timer.
            measureDevice.StopCollecting();
            timer.Dispose();
        }


        private async void timer_Tick(object state)
        {

            //Displays current measurement by calling methods in MeasureLengthDevice. Displays time stamp.
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                if (myUnits == Units.Imperial)
                {
                    displayData.Measurement = measureDevice.ImperialValue().ToString() + " in.   " + DateTime.Now;
                }
                else if (myUnits == Units.Metric)
                {
                    displayData.Measurement = measureDevice.MetricValue().ToString() + " cm.   " + DateTime.Now;
                }
                displayData.History = measureDevice.History;


            });
        }
        //Updates units when radio button is clicked.
        private void imperialRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            myUnits = Units.Imperial;
        }

        //Updates units when radio button is clicked.
        private void metricRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            myUnits = Units.Metric;
        }

        private void exitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
            
        }
    }
}