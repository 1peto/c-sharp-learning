using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace WeatherApp
{

    //nove triedy pre JSON deserializaciu pre momentalnu predpoved
    public class WeatherData
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("main")]
        public MainData Main { get; set; } = new MainData();

        [JsonProperty("weather")]
        public WeatherDescription[] Weather { get; set; } = new WeatherDescription[0];
    }

    //momentalna predpoved
    public class MainData
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }

    //momentalna predpoved
    public class WeatherDescription
    {
        [JsonProperty("main")]
        public string Main { get; set; } = "";

        [JsonProperty("description")]
        public string Description { get; set; } = "";
    }

    //deserializacia pre 5-day predpoved
    //hlavna triedna pre forecast
    public class ForecastData
    {
        [JsonProperty("list")]
        public ForecastItem[] List { get; set; } = new ForecastItem[0];
    }

    //forecast predpoved (jedna polozka predpovede)
    public class ForecastItem
    {
        [JsonProperty("dt")]
        public long DataTime { get; set; } //Unix timestamp

        [JsonProperty("main")]
        public MainData Main { get; set; } = new MainData(); //existujuca MainData

        [JsonProperty("weather")]
        public WeatherDescription[] Weather { get; set; } = new WeatherDescription[0];

        [JsonProperty("dt_txt")]
        public string DataTimeText { get; set; } = "";
    }

    //hlavna trieda, ktora obsahuje main
    class Program
    {
        //na zaciatku triedy program dame api a http client
        static string apiKluc = "c192e22b5e3142970a05ae25a3ff8d90";
        static HttpClient client = new HttpClient();

        //nova metoda - Newtonsoft deserializacia
        static WeatherData ParseWeatherJsonNewtonsoft(string json)
        {
            return JsonConvert.DeserializeObject<WeatherData>(json)!;
        }

        //deserializacia pre forecast
        static ForecastData ParseForecastJsonNewtonsoft(string json)
        {
            return JsonConvert.DeserializeObject<ForecastData>(json)!;
        }

        //vytvorim nove metody ktore sa budu pouzivat v main
        //volanie pre aktualne pocasie
        static async Task ZobrazAktualnePocasie()
        {
            Console.WriteLine("Zadaj nazov mesta: ");
            string mesto = Console.ReadLine()!;

            try
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={mesto}&appid={apiKluc}&units=metric";
                string response = await client.GetStringAsync(url);

                WeatherData weather = ParseWeatherJsonNewtonsoft(response);

                Console.WriteLine("=== AKTUÁLNE POČASIE ===");
                Console.WriteLine($"Mesto: {weather.Name}");
                Console.WriteLine($"Teplota: {weather.Main.Temperature}°C");
                Console.WriteLine($"Pocitovo: {weather.Main.FeelsLike}°C");
                Console.WriteLine($"Vlhkosť: {weather.Main.Humidity}%");
                Console.WriteLine($"Počasie: {weather.Weather[0].Description}");
                Console.WriteLine($"Kategória: {weather.Weather[0].Main}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Mesto '{mesto}' sa nenaslo alebo problem s internetom");
                Console.WriteLine($"Problem: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Neocakavana chyba: {e.Message}");
            }
        }

        //5-dnova predpoved metoda/funkcia - NOVE
        static async Task Zobraz5Days()
        {
            Console.WriteLine("Zadaj nazov mesta: ");
            string mesto = Console.ReadLine()!;

            try
            {
                //url pre forecast API
                string url = $"https://api.openweathermap.org/data/2.5/forecast?q={mesto}&appid={apiKluc}&units=metric";
                string response = await client.GetStringAsync(url);

                ForecastData forecast = ParseForecastJsonNewtonsoft(response);

                Console.WriteLine("=== 5-DNOVA PREDPOVED ===");

                //zobrazime len jeden zaznam na kazdy den
                var denneZaznamy = forecast.List
                    .Where(item => item.DataTimeText.Contains("12:00:00"))
                    .Take(5);

                int den = 1;
                foreach (var item in denneZaznamy)
                {
                    // Extrahovat datum
                    string datum = item.DataTimeText.Split(' ')[0];

                    Console.WriteLine($" Den {den} - {datum}");
                    Console.WriteLine($" Teplota: {item.Main.Temperature}°C (pocitovo {item.Main.FeelsLike}°C)");
                    Console.WriteLine($" Vlhkost: {item.Main.Humidity}%");
                    Console.WriteLine($" Pocasie: {item.Weather[0].Description}");

                    Console.WriteLine("--------------------------------");
                    den++;
                }
            }
            catch (HttpRequestException)
            {
                Console.WriteLine($"Mesto '{mesto}' sa nenaslo");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Neocakavana chyba {e.Message}");
            }
        }

        //funkcie pre manualne parsovanie

        /*static string ParseMesto(string json)
        {
            //najprv najdeme zaciatok mesta po "name":"
            int startIndex = json.IndexOf("\"name\":\"") + 8;
            //potom najdeme kde sa konci nazov mesta "
            int endIndex = json.IndexOf("\"", startIndex);
            //a kedze to je string metoda, vratime najdeny string
            return json.Substring(startIndex, endIndex - startIndex);
        }

        static double ParseTeplota(string json)
        {
            int startIndex = json.IndexOf("\"temp\":") + 7;
            //Teplota moze koncit ciarkou alebo kuceravou zatvorkou
            int endIndex = json.IndexOf(",", startIndex);
            //ked nenajde ciarku, hlada }
            if (endIndex == -1)
            {
                endIndex = json.IndexOf("}", startIndex);
            }

            //vyberieme cislo ako string, a konvertujeme na double
            string teplota = json.Substring(startIndex, endIndex - startIndex);
            //pozuijeme InvariantCulture pre anglicky format
            return double.Parse(teplota, System.Globalization.CultureInfo.InvariantCulture);
        }

        static string ParsePocasie(string json)
        {
            int startIndex = json.IndexOf("\"description\":\"") + 15;
            int endIndex = json.IndexOf("\"", startIndex);

            return json.Substring(startIndex, endIndex - startIndex);
        }*/

        //main
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== WEATHER APP ===");

            while (true)
            {
                Console.WriteLine("=== MENU ===");
                Console.WriteLine("1. Aktualne poacsie");
                Console.WriteLine("2. 5-dnova predpoved");
                Console.WriteLine("3. Koniec");
                Console.Write("Vyberte moznost (1-3): ");

                string volba = Console.ReadLine()!;

                switch (volba)
                {
                    case "1":
                        await ZobrazAktualnePocasie();
                        break;
                    case "2":
                        await Zobraz5Days();
                        break;
                    case "3":
                        Console.WriteLine("Dakujem za pouzivanie!");
                        client.Dispose();
                        return;
                    default:
                        Console.WriteLine("Neplatna volba. Zadajte 1,2 alebo 3.");
                        break;
                }

                Console.WriteLine("Stlacte Enter pre pokracovanie...");
                Console.ReadLine();
            }

            /* toto je len simulacia
            Random random = new Random();
            int teplota = random.Next(-5, 30);
            string[] pocasie = { "slnecno", "oblačno", "dážď", "sneženie" };
            string aktualnePocasie = pocasie[random.Next(pocasie.Length)];

            //vypisy random ass udajov
            Console.WriteLine($"Teplota: {teplota} °C");
            Console.WriteLine($"Pocasie: {aktualnePocasie}");

            Console.WriteLine("TODO - keď bude API aktívne:");
            Console.WriteLine("- Nahradiť simuláciu skutočným HTTP requestom");
            Console.WriteLine("- Spracovať JSON odpoveď z OpenWeatherMap");
            Console.WriteLine();*/
        }
    }
}
