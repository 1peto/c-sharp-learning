using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApp
{
    class Program
    {
        //funkcie pre manualne parsovanie
        static string ParseMesto(string json)
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
        }

        //main
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

                //spracujeme udaje a pekne ich vypiseme pomocou novo vzniknutych metod
                string parsovaneMesto = ParseMesto(response);
                double teplota = ParseTeplota(response);
                string pocasie = ParsePocasie(response);

                //vypis
                Console.WriteLine("=== POCASIE ===");
                Console.WriteLine($"Mesto : {parsovaneMesto}");
                Console.WriteLine($"Teplota: {teplota}°C");
                Console.WriteLine($"Pocasie: {pocasie}");
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
