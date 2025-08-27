using System;
using System.Threading.Tasks;
using System.Linq;

namespace WeatherApp
{
    //hlavna trieda, ktora obsahuje main
    class Program
    {
        static WeatherService weatherService = new WeatherService(AppConfig.API_KLUC);

        //vytvorim nove metody ktore sa budu pouzivat v main
        //volanie pre aktualne pocasie
        static async Task ZobrazAktualnePocasie()
        {
            Console.WriteLine("Zadaj nazov mesta: ");
            string mesto = Console.ReadLine()!;

            try
            {
                WeatherData weather = await weatherService.ZiskajAktualnePocasie(mesto);

                Console.WriteLine("=== AKTUÁLNE POČASIE ===");
                Console.WriteLine($"Mesto: {weather.Name}");
                Console.WriteLine($"Teplota: {weather.Main.Temperature}°C");
                Console.WriteLine($"Pocitovo: {weather.Main.FeelsLike}°C");
                Console.WriteLine($"Vlhkosť: {weather.Main.Humidity}%");
                Console.WriteLine($"Počasie: {weather.Weather[0].Description}");
                Console.WriteLine($"Kategória: {weather.Weather[0].Main}");
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
                ForecastData forecast = await weatherService.ZiskajForecast(mesto);

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
            catch (Exception e)
            {
                Console.WriteLine($"Neocakavana chyba {e.Message}");
            }
        }

        //main
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== WEATHER APP ===");

            try
            {
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
                            return;
                        default:
                            Console.WriteLine("Neplatna volba. Zadajte 1,2 alebo 3.");
                            break;
                    }

                    Console.WriteLine("Stlacte Enter pre pokracovanie...");
                    Console.ReadLine();
                }
            }
            finally
            {
                weatherService?.Dispose();
            }
        }
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
