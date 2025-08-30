using System;
using System.Collections.Generic;
using System.IO;

namespace WeatherApp
{
    public class FavoriteCitiesService
    {
        private const string FAVORITES_FILE = "oblubene_mesta.txt";

        //metoda na nacitanie oblubenych miest zo suboru
        public List<string> NacitajOblubeneMesta()
        {
            //ak subor neexistuje, vrat prazdny zoznam
            if (!File.Exists(FAVORITES_FILE))
            {
                return new List<string>();
            }

            //nacitaj vsetky riadky zo suboru
            string[] riadky = File.ReadAllLines(FAVORITES_FILE);

            //konvertuj na List<string> a vrat
            return new List<string>(riadky);
        }

        //metoda na ulozenie obl. miest do suboru
        public void UlozOblubeneMesta(List<string> mesta)
        {
            try
            {
                File.WriteAllLines(FAVORITES_FILE, mesta);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Chyba pri ukladani: {e.Message}");
            }
        }

        //metoda na pridanie noveho mesta do obl.
        public void PridajMesto(string mesto)
        {
            //nacitaj aktualne mesto
            List<string> mesta = NacitajOblubeneMesta();

            //kontrola, ci uz nie je v zozname
            if (!mesta.Contains(mesto))
            {
                mesta.Add(mesto);
                UlozOblubeneMesta(mesta);
                Console.WriteLine($"Mesto '{mesto}' pridane do oblubenych!");
            }
            else
            {
                Console.WriteLine($"Mesto '{mesto}' uz je v oblubenych!");
            }
        }

        //metoda na odstranenie mesta z oblubenych
        public void OdstranMesto(int index)
        {
            //nacitaj aktualne mesta
            List<string> mesta = NacitajOblubeneMesta();

            //kontrola platnosti indexu
            if (index >= 0 && index < mesta.Count)
            {
                string odstranene = mesta[index];
                mesta.RemoveAt(index);
                UlozOblubeneMesta(mesta);
                Console.WriteLine($"Mesto '{odstranene}' odstranene z oblubenych!");
            }
            else
            {
                Console.WriteLine("Neplatny index!");
            }
        }
    }
}