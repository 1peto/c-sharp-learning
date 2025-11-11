using System;

namespace StaticExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# STATIC KEYWORD ===\n");

            // ===== PRÍKLAD 1: STATIC POLIA =====
            Console.WriteLine("--- PRÍKLAD 1: STATIC POLIA ---");
            Console.WriteLine($"Počet vytvorených áut: {Auto.PocetVytvorenych}");
            
            Auto auto1 = new Auto("Škoda", "Fabia");
            Auto auto2 = new Auto("VW", "Golf");
            Auto auto3 = new Auto("Audi", "A4");
            
            Console.WriteLine($"Počet vytvorených áut: {Auto.PocetVytvorenych}");
            Console.WriteLine();

            // ===== PRÍKLAD 2: STATIC METÓDY =====
            Console.WriteLine("--- PRÍKLAD 2: STATIC METÓDY ---");
            
            // Volanie bez vytvorenia objektu
            int sucet = Matematika.Scitaj(15, 27);
            Console.WriteLine($"15 + 27 = {sucet}");
            
            int soucin = Matematika.Vynasob(8, 7);
            Console.WriteLine($"8 × 7 = {soucin}");
            
            double odmocnina = Matematika.Odmocnina(144);
            Console.WriteLine($"√144 = {odmocnina}");
            
            double mocnina = Matematika.Mocnina(2, 10);
            Console.WriteLine($"2^10 = {mocnina}");
            Console.WriteLine();

            // ===== PRÍKLAD 3: STATIC TRIEDA =====
            Console.WriteLine("--- PRÍKLAD 3: STATIC TRIEDA ---");
            
            Pomocnik.VypisHlavicku("Vitaj v programe", 40);
            Pomocnik.VypisOddelovac(40);
            
            string text = "  tento text má medzery  ";
            Console.WriteLine($"Pred: '{text}'");
            Console.WriteLine($"Po: '{Pomocnik.VycistiText(text)}'");
            
            Console.WriteLine($"\nNáhodné číslo 1-100: {Pomocnik.NahodneCislo(1, 100)}");
            Console.WriteLine($"Náhodné číslo 1-100: {Pomocnik.NahodneCislo(1, 100)}");
            Console.WriteLine();

            // ===== PRÍKLAD 4: STATIC KONŠTRUKTOR =====
            Console.WriteLine("--- PRÍKLAD 4: STATIC KONŠTRUKTOR ---");
            Console.WriteLine("Prvé použitie Databaza triedy:");
            Databaza.Pripoj();
            Console.WriteLine("\nDruhé použitie:");
            Databaza.Odpoj();
            Console.WriteLine();

            // ===== PRÍKLAD 5: STATIC VS INSTANCE =====
            Console.WriteLine("--- PRÍKLAD 5: STATIC VS INSTANCE ---");
            
            Pocitadlo p1 = new Pocitadlo();
            Pocitadlo p2 = new Pocitadlo();
            
            p1.ZvysInstance();
            p1.ZvysInstance();
            p1.ZvysStaticka();
            
            p2.ZvysInstance();
            p2.ZvysStaticka();
            
            Console.WriteLine("Počítadlo 1:");
            p1.VypisStav();
            
            Console.WriteLine("\nPočítadlo 2:");
            p2.VypisStav();
            
            Console.WriteLine($"\nCelkový počet inkrementácií (static): {Pocitadlo.CelkovyPocet}");
            Console.WriteLine();

            // ===== PRÍKLAD 6: STATIC PROPERTIES =====
            Console.WriteLine("--- PRÍKLAD 6: STATIC PROPERTIES ---");
            
            Konfigurace.NazovAplikacie = "Moja Super Appka";
            Konfigurace.Verzia = "1.2.3";
            Konfigurace.DebugMode = true;
            
            Konfigurace.VypisKonfiguraciu();
            Console.WriteLine();

            // ===== PRÍKLAD 7: SINGLETON PATTERN =====
            Console.WriteLine("--- PRÍKLAD 7: SINGLETON PATTERN ---");
            
            // Nemôžeme vytvoriť: new Logger() - konštruktor je private
            Logger log1 = Logger.Instance;
            Logger log2 = Logger.Instance;
            
            Console.WriteLine($"Je to tá istá inštancia? {Object.ReferenceEquals(log1, log2)}");
            
            log1.Zaznamenaj("Prvý záznam");
            log2.Zaznamenaj("Druhý záznam");
            log1.Zaznamenaj("Tretí záznam");
            
            log1.VypisVsetkyZaznamy();
            Console.WriteLine();

            // ===== PRÍKLAD 8: EXTENSION METHODS (vyžadujú static) =====
            Console.WriteLine("--- PRÍKLAD 8: EXTENSION METHODS ---");
            
            string testText = "ahoj svet";
            Console.WriteLine($"Pôvodný: {testText}");
            Console.WriteLine($"Kapitalizovaný: {testText.Kapitalizuj()}");
            
            int cislo = 12345;
            Console.WriteLine($"\nČíslo: {cislo}");
            Console.WriteLine($"Je párne? {cislo.JeParne()}");
            Console.WriteLine($"Je nepárne? {cislo.JeNeparne()}");
            
            int neparne = 999;
            Console.WriteLine($"\nČíslo: {neparne}");
            Console.WriteLine($"Je párne? {neparne.JeParne()}");
            Console.WriteLine();

            // ===== PRÍKLAD 9: CACHE (Static Dictionary) =====
            Console.WriteLine("--- PRÍKLAD 9: CACHE ---");
            
            Cache.Uloz("uzivatel1", "Peter Novák");
            Cache.Uloz("uzivatel2", "Jana Kováčová");
            Cache.Uloz("email", "peter@example.com");
            
            Console.WriteLine($"Užívateľ1: {Cache.Ziskaj("uzivatel1")}");
            Console.WriteLine($"Email: {Cache.Ziskaj("email")}");
            Console.WriteLine($"Neexistuje: {Cache.Ziskaj("neexistuje") ?? "null"}");
            
            Cache.VymazVsetko();
            Console.WriteLine($"Po vymazaní: {Cache.Ziskaj("uzivatel1") ?? "null"}");
            Console.WriteLine();

            Console.WriteLine("\nStlač ľubovoľnú klávesu pre ukončenie...");
            Console.ReadKey();
        }
    }

    // ================================================
    // PRÍKLAD 1: STATIC POLIA
    // ================================================
    public class Auto
    {
        // Static pole - zdieľané medzi všetkými inštanciami
        public static int PocetVytvorenych = 0;

        // Instance polia - unikátne pre každý objekt
        public string Znacka { get; set; }
        public string Model { get; set; }
        private int instanceCislo;

        public Auto(string znacka, string model)
        {
            PocetVytvorenych++; // Zvýši sa pre každý nový objekt
            instanceCislo = PocetVytvorenych;
            
            Znacka = znacka;
            Model = model;
            
            Console.WriteLine($"Vytvorené auto #{instanceCislo}: {Znacka} {Model}");
        }
    }

    // ================================================
    // PRÍKLAD 2: STATIC METÓDY
    // ================================================
    public class Matematika
    {
        // Static metódy - fungujú bez vytvorenia objektu
        public static int Scitaj(int a, int b)
        {
            return a + b;
        }

        public static int Odcitaj(int a, int b)
        {
            return a - b;
        }

        public static int Vynasob(int a, int b)
        {
            return a * b;
        }

        public static double Vydel(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Nemožno deliť nulou!");
            return a / b;
        }

        public static double Odmocnina(double x)
        {
            return Math.Sqrt(x);
        }

        public static double Mocnina(double zaklad, double exponent)
        {
            return Math.Pow(zaklad, exponent);
        }
    }

    // ================================================
    // PRÍKLAD 3: STATIC TRIEDA
    // ================================================
    public static class Pomocnik
    {
        // Static trieda môže obsahovať LEN static členy
        private static Random random = new Random();

        public static void VypisHlavicku(string text, int sirka)
        {
            string oddelovac = new string('=', sirka);
            int padding = (sirka - text.Length) / 2;
            string paddingText = new string(' ', Math.Max(0, padding));
            
            Console.WriteLine(oddelovac);
            Console.WriteLine(paddingText + text);
            Console.WriteLine(oddelovac);
        }

        public static void VypisOddelovac(int sirka, char znak = '-')
        {
            Console.WriteLine(new string(znak, sirka));
        }

        public static string VycistiText(string text)
        {
            return text.Trim().ToLower();
        }

        public static int NahodneCislo(int min, int max)
        {
            return random.Next(min, max + 1);
        }
    }

    // ================================================
    // PRÍKLAD 4: STATIC KONŠTRUKTOR
    // ================================================
    public class Databaza
    {
        private static string connectionString;
        private static bool jeInitializovana = false;

        // Static konštruktor - zavolá sa AUTOMATICKY pred prvým použitím triedy
        static Databaza()
        {
            Console.WriteLine("⚙️ Static konštruktor: Inicializácia databázy...");
            connectionString = "Server=localhost;Database=MojaDB;";
            jeInitializovana = true;
            Console.WriteLine("✅ Databáza inicializovaná!");
        }

        public static void Pripoj()
        {
            Console.WriteLine($"Pripájam sa k databáze: {connectionString}");
        }

        public static void Odpoj()
        {
            Console.WriteLine("Odpojenie od databázy.");
        }
    }

    // ================================================
    // PRÍKLAD 5: STATIC VS INSTANCE
    // ================================================
    public class Pocitadlo
    {
        // Static - zdieľané medzi všetkými inštanciami
        public static int CelkovyPocet = 0;

        // Instance - unikátne pre každý objekt
        private int instancePocet = 0;

        public void ZvysStaticka()
        {
            CelkovyPocet++;
        }

        public void ZvysInstance()
        {
            instancePocet++;
        }

        public void VypisStav()
        {
            Console.WriteLine($"Instance počet: {instancePocet}");
            Console.WriteLine($"Static počet: {CelkovyPocet}");
        }
    }

    // ================================================
    // PRÍKLAD 6: STATIC PROPERTIES
    // ================================================
    public static class Konfigurace
    {
        public static string NazovAplikacie { get; set; }
        public static string Verzia { get; set; }
        public static bool DebugMode { get; set; }

        // Static property s backing field
        private static int _maxPocetPripojeni = 10;
        public static int MaxPocetPripojeni
        {
            get { return _maxPocetPripojeni; }
            set
            {
                if (value > 0 && value <= 100)
                    _maxPocetPripojeni = value;
            }
        }

        public static void VypisKonfiguraciu()
        {
            Console.WriteLine("=== KONFIGURÁCIA ===");
            Console.WriteLine($"Názov: {NazovAplikacie}");
            Console.WriteLine($"Verzia: {Verzia}");
            Console.WriteLine($"Debug mód: {(DebugMode ? "Zapnutý" : "Vypnutý")}");
            Console.WriteLine($"Max pripojení: {MaxPocetPripojeni}");
        }
    }

    // ================================================
    // PRÍKLAD 7: SINGLETON PATTERN
    // ================================================
    public class Logger
    {
        // Private static inštancia
        private static Logger _instance;
        private static readonly object _lock = new object();

        // Private konštruktor - nikto zvonku nemôže vytvoriť inštanciu
        private Logger()
        {
            zaznamy = new List<string>();
            Console.WriteLine("🔧 Logger inicializovaný");
        }

        // Public static property pre prístup k jedinej inštancii
        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock) // Thread-safe
                    {
                        if (_instance == null)
                        {
                            _instance = new Logger();
                        }
                    }
                }
                return _instance;
            }
        }

        private List<string> zaznamy;

        public void Zaznamenaj(string sprava)
        {
            string casovyZaznam = $"[{DateTime.Now:HH:mm:ss}] {sprava}";
            zaznamy.Add(casovyZaznam);
            Console.WriteLine($"📝 {casovyZaznam}");
        }

        public void VypisVsetkyZaznamy()
        {
            Console.WriteLine("\n=== VŠETKY ZÁZNAMY ===");
            foreach (var zaznam in zaznamy)
            {
                Console.WriteLine(zaznam);
            }
        }
    }

    // ================================================
    // PRÍKLAD 8: EXTENSION METHODS
    // ================================================
    public static class StringExtensions
    {
        // Extension metóda pre string
        public static string Kapitalizuj(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            return char.ToUpper(text[0]) + text.Substring(1);
        }

        public static string Opakuj(this string text, int pocet)
        {
            return string.Concat(Enumerable.Repeat(text, pocet));
        }
    }

    public static class IntExtensions
    {
        // Extension metóda pre int
        public static bool JeParne(this int cislo)
        {
            return cislo % 2 == 0;
        }

        public static bool JeNeparne(this int cislo)
        {
            return cislo % 2 != 0;
        }
    }

    // ================================================
    // PRÍKLAD 9: CACHE (Static Dictionary)
    // ================================================
    public static class Cache
    {
        private static Dictionary<string, string> data = new Dictionary<string, string>();

        public static void Uloz(string kluc, string hodnota)
        {
            data[kluc] = hodnota;
            Console.WriteLine($"💾 Uložené: {kluc} = {hodnota}");
        }

        public static string Ziskaj(string kluc)
        {
            if (data.ContainsKey(kluc))
                return data[kluc];
            return null;
        }

        public static void Vymaz(string kluc)
        {
            if (data.ContainsKey(kluc))
            {
                data.Remove(kluc);
                Console.WriteLine($"🗑️ Vymazané: {kluc}");
            }
        }

        public static void VymazVsetko()
        {
            data.Clear();
            Console.WriteLine("🗑️ Cache vyčistená");
        }

        public static int Pocet()
        {
            return data.Count;
        }
    }
}
