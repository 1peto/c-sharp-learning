using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== WEATHER APP ===");

            Console.Write("Zadaj nazov mesta: ");
            string mesto = Console.ReadLine()!;

            Console.WriteLine($"Hladam pocasie pre {mesto}...");

            //skutocny HTTP request na API
            try
            {
                string apiKluc = "c192e22b5e3142970a05ae25a3ff8d90";
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={mesto}&appid={apiKluc}&units=metric";

                using HttpClient client = new HttpClient();
                Console.WriteLine("Posielam HTTP request...");
                string response = await client.GetStringAsync(url);

                Console.WriteLine("Odpoved z API:");
                Console.WriteLine(response);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Chyba pri volani API: {e.Message}");
                Console.WriteLine("Skontrolujte, internetove pripojenie, ci nazov mesta.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Neocakavana chyba: {e.Message}");
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
