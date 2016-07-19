using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WebServiceUWPApp.WeatherService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WebServiceUWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void GoButton_Click(object sender, RoutedEventArgs e)
        {
            WeatherSoapClient proxy = new WeatherSoapClient();
            WeatherReturn result = await proxy.GetCityWeatherByZIPAsync(inputZipCode.Text);
            ForecastReturn fr = await proxy.GetCityForecastByZIPAsync(inputZipCode.Text);
            Forecast[] f = fr.ForecastResult;
            if (result.Success)
            {
                resultCityState.Text = String.Format("{0}, {1}", result.City, result.State);
                resultDetails.Text = String.Format
                    ("\n\nConditions - {0} \n\nTemperature - {1} \n\nRelative Humidity - {2} \n\nWind - {3} \n\nPressure - {4} - \n\nPressure - {5}  - \n\nWind Chill - {6}  - \n\nVisibility - {7}",

                     result.Description, result.Temperature, result.RelativeHumidity, result.Wind, result.Pressure, fr.WeatherStationCity, result.WindChill, result.Visibility);
            }

            GetWeatherInformationResponse result1 = await proxy.GetWeatherInformationAsync();
            WeatherDescription[] r = result1.GetWeatherInformationResult;
            foreach (WeatherDescription d in r)
                if (d.WeatherID == result.WeatherID)
                {
                    image i = new image();
                    i.imageurl = d.PictureURL;
                    myimage.DataContext = i;
                    break;
                }
        }
    }

    public class image
    {
        public string imageurl { get; set; }
    }
}