using BDSA2017.Lecture06.App.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BDSA2017.Lecture06.App.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private double _dkk;
        public double DKK { get => _dkk; set { if (_dkk != value) { _dkk = value; OnPropertyChanged(); } } }

        private double _usd;
        public double USD { get => _usd; set { if (_usd != value) { _usd = value; OnPropertyChanged(); } } }

        private double _gbp;
        public double GBP { get => _gbp; set { if (_gbp != value) { _gbp = value; OnPropertyChanged(); } } }

        private double _eur;
        public double EUR { get => _eur; set { if (_eur != value) { _eur = value; OnPropertyChanged(); } } }

        public ICommand Calculate => new RelayCommand(CalculateRates);

        private void CalculateRates(object o)
        {
            var amount = DKK;

            USD = GetRate("DKK", "USD") * DKK;
            GBP = GetRate("DKK", "GBP") * DKK;
            EUR = GetRate("DKK", "EUR") * DKK;
        }

        private double GetRate(string from, string to)
        {
            Task.Delay(TimeSpan.FromSeconds(2)).Wait();

            using (var client = new HttpClient())
            {
                var url = $"http://currency-api.appspot.com/api/{from}/{to}.json";

                var data = client.GetStringAsync(url).Result;
                var json = JsonConvert.DeserializeObject<ExchangeRate>(data);

                return json.Rate;
            }
        }
    }
}
