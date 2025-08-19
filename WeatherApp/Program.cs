using System;

namespace WeatherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== WEATHER APP ===");
            //fake simluacia API
            Console.Write("Zadaj nazov mesta: ");
            string mesto = Console.ReadLine();

            Console.WriteLine($"Hladam pocasie pre {mesto}...");

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
            Console.WriteLine();
        }
    }
}
