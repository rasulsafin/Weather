using System;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WeatherApp
{
    public partial class MainForm : Form
    {
        private const string ApiKey = "e4574dd10229912f84cbc309da149476";
        private const string ApiUrl = "http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}";

        public MainForm()
        {
            InitializeComponent();
        }

        private void GetWeatherButton_Click(object sender, EventArgs e)
        {
            string city = CityTextBox.Text;

            try
            {
                using (WebClient client = new WebClient())
                {
                    string url = string.Format(ApiUrl, city, ApiKey);
                    string json = client.DownloadString(url);

                    WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(json);

                    TemperatureLabel.Text = $"Temperature: {weatherData.Main.Temp}°C";
                    DescriptionLabel.Text = $"Description: {weatherData.Weather[0].Description}";
                    WindSpeedLabel.Text = $"Wind Speed: {weatherData.Wind.Speed} m/s";
                }
            }
            catch (WebException)
            {
                MessageBox.Show("Failed to retrieve weather data. Please check your internet connection and try again.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }

    public class WeatherData
    {
        public Weather[] Weather { get; set; }
        public MainData Main { get; set; }
        public WindData Wind { get; set; }
    }

    public class Weather
    {
        public string Description { get; set; }
    }

    public class MainData
    {
        public double Temp { get; set; }
    }

    public class WindData
    {
        public double Speed { get; set; }
    }
}
