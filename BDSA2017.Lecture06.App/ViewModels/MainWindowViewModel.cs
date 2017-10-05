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

        public ICommand Calculate => new RelayCommand(async o => await CalculateRates());

        private async Task CalculateRates()
        {
            var amount = DKK;

            USD = await GetRate("DKK", "USD") * DKK;
            GBP = await GetRate("DKK", "GBP") * DKK;
            EUR = await GetRate("DKK", "EUR") * DKK;
        }

        private async Task<double> GetRate(string from, string to)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));

            using (var client = new HttpClient())
            {
                var url = $"http://currency-api.appspot.com/api/{from}/{to}.json";

                var data = await client.GetStringAsync(url);
                var json = JsonConvert.DeserializeObject<ExchangeRate>(data);

                return json.Rate;
            }
        }
    }
}
