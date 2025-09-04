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
            Console.WriteLine();

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
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Neocakavana chyba: {e.Message}");
                Console.WriteLine();
            }
        }

        //5-dnova predpoved metoda/funkcia - NOVE
        static async Task Zobraz5Days()
        {
            Console.WriteLine("Zadaj nazov mesta: ");
            string mesto = Console.ReadLine()!;
            Console.WriteLine();

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
                    Console.WriteLine("   " + new string('─', 40));
                    Console.WriteLine();
                    den++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Neocakavana chyba {e.Message}");
                Console.WriteLine();
            }
        }

        //zobrazenie oblubenych miest
        static async Task ZobrazOblubeneMesta()
        {
            FavoriteCitiesService service = new FavoriteCitiesService();

            while (true)
            {
                Console.Clear();

                List<string> mesta = service.NacitajOblubeneMesta();

                Console.WriteLine("=== OBLUBENE MESTA ===");
                Console.WriteLine();

                if (mesta.Count == 0)
                {
                    Console.WriteLine("ziadne oblubene mesta nie su ulozene.");
                }
                else
                {
                    for (int i = 0; i < mesta.Count; i++)
                    {
                        Console.WriteLine($"   {i}. {mesta[i]}");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Moznosti:");
                Console.WriteLine("1. Zobrazit pocasie pre mesto");
                Console.WriteLine("2. Pridat nove mesto");
                Console.WriteLine("3. Odstranit mesto");
                Console.WriteLine("4. Spat do hlavneho menu");
                Console.WriteLine();
                Console.Write("Vyberte moznost (1-4): ");

                string volba = Console.ReadLine()!;
                Console.WriteLine();

                switch (volba)
                {
                    case "1":
                        await ZobrazPocasiePreMesto(service);
                        break;
                    case "2":
                        PridatNoveMesto(service);
                        break;
                    case "3":
                        OdstranitMesto(service);
                        break;
                    case "4":
                        Console.Clear();
                        return; // Návrat do hlavného menu
                    default:
                        Console.WriteLine("Neplatna volba!");
                        break;
                }

                if (volba != "4")
                { 
                    Console.WriteLine();
                    Console.WriteLine(new string('─', 50));
                    Console.WriteLine("Stlacte Enter pre pokracovanie...");
                    Console.ReadLine();
                }
            }
        }

        //zobrazenie pocasie pre vybrane mesto z oblubenych
        static async Task ZobrazPocasiePreMesto(FavoriteCitiesService service)
        {
            List<string> mesta = service.NacitajOblubeneMesta();

            if (mesta.Count == 0)
            {
                Console.WriteLine("Ziadne mesta na vyber!");
                Console.WriteLine();
                return;
            }

            Console.Write($"Vyberte cislo mesta (0-{mesta.Count - 1}): ");
            Console.WriteLine();

            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < mesta.Count)
            {
                string mesto = mesta[index];

                try
                {
                    WeatherData weather = await weatherService.ZiskajAktualnePocasie(mesto);

                    Console.WriteLine("=== AKTUÁLNE POČASIE ===");
                    Console.WriteLine($"Mesto: {weather.Name}");
                    Console.WriteLine($"Teplota: {weather.Main.Temperature}°C");
                    Console.WriteLine($"Pocitovo: {weather.Main.FeelsLike}°C");
                    Console.WriteLine($"Vlhkosť: {weather.Main.Humidity}%");
                    Console.WriteLine($"Pocasie: {weather.Weather[0].Description}");
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Chyba: {e.Message}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Neplane cislo!");
                Console.WriteLine();
            }
        }

        static void PridatNoveMesto(FavoriteCitiesService service)
        {
            Console.Write("Zadaj nazov mesta na pridanie: ");
            string mesto = Console.ReadLine()!;
            Console.WriteLine();

            if (!string.IsNullOrWhiteSpace(mesto))
            {
                service.PridajMesto(mesto);
            }
            else
            {
                Console.WriteLine("Nazor mesta nemoze byt prazdny!");
            }
            Console.WriteLine();
        }

        static void OdstranitMesto(FavoriteCitiesService service)
        {
            List<string> mesta = service.NacitajOblubeneMesta();

            if (mesta.Count == 0)
            {
                Console.WriteLine("Ziadne mesta na odstranenie!");
                Console.WriteLine();
                return;
            }

            Console.WriteLine("Ktore mesto chcete odstranit?");
            for (int i = 0; i < mesta.Count; i++)
            {
                Console.WriteLine($"{i}. {mesta[i]}");
            }
            Console.WriteLine();
            Console.Write($"Zadajte cislo mesta (0-{mesta.Count - 1}): ");

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                service.OdstranMesto(index);
            }
            else
            {
                Console.WriteLine("Neplatne cislo!");
            }
            Console.WriteLine();
        }

        //main
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== WEATHER APP ===");
            Console.WriteLine();

            try
            {
                while (true)
                {
                    Console.WriteLine("=== MENU ===");
                    Console.WriteLine("1. Aktualne pocasie");
                    Console.WriteLine("2. 5-dnova predpoved");
                    Console.WriteLine("3. Oblubene mesta");
                    Console.WriteLine("4. Koniec");
                    Console.WriteLine();
                    Console.Write("Vyberte moznost (1-4): ");

                    string volba = Console.ReadLine()!;
                    Console.WriteLine();

                    switch (volba)
                    {
                        case "1":
                            await ZobrazAktualnePocasie();
                            break;
                        case "2":
                            await Zobraz5Days();
                            break;
                        case "3":
                            await ZobrazOblubeneMesta();
                            break;
                        case "4":
                            Console.WriteLine("Dakujem za pouzivanie!");
                            return;
                        default:
                            Console.WriteLine("Neplatna volba. Zadajte 1,2,3 alebo 4.");
                            break;
                    }

                    if (volba != "4")
                    { 
                        Console.WriteLine("\n" + new string('=', 50));
                        Console.WriteLine("Stlacte Enter pre pokracovanie...");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
            }
            finally
            {
                weatherService?.Dispose();
            }
        }
    }
}
