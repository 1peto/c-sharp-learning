using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherApp
{
    //trieda zodnovedna za API komunikaciu
    public class WeatherService
    {
        private readonly string _apiKluc;
        private readonly HttpClient _client;

        //konstruktor - nastavi api kluc a http klient
        public WeatherService(string apiKluc)
        {
            _apiKluc = apiKluc;
            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromSeconds(AppConfig.API_TIMEOUT_SECONDS);
        }

        //presuvame tu metody na ziskavanie pocasia, v Program.cs bude hlavne UI
        public async Task<WeatherData> ZiskajAktualnePocasie(string mesto)
        {
            try
            {
                string url = $"{AppConfig.API_BASE_URL}/weather?q={mesto}&appid={_apiKluc}&units={AppConfig.UNITS}&lang={AppConfig.LANGUAGE}";
                string response = await _client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<WeatherData>(response)!;
            }
            catch (HttpRequestException)
            {
                throw new Exception($"Mesto '{mesto}' sa nenašlo alebo problém s internetom.");
            }
        }

        public async Task<ForecastData> ZiskajForecast(string mesto)
        {
            try
            {
                string url = $"{AppConfig.API_BASE_URL}/forecast?q={mesto}&appid={_apiKluc}&units={AppConfig.UNITS}&lang={AppConfig.LANGUAGE}";
                string response = await _client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<ForecastData>(response)!;
            }
            catch (HttpRequestException)
            {
                throw new Exception($"Mesto '{mesto}' sa nenašlo alebo problém s internetom.");
            }
        }

        //a upratanie, free
        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}