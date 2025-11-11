using System;

namespace Exercises
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== CVIČENIA C# ===\n");
            Console.WriteLine("Vitaj v sekcii cvičení!");
            Console.WriteLine("Prečítaj si EXERCISES.md pre zadania.\n");
            Console.WriteLine("Tu môžeš písať svoje riešenia...\n");

            // ================================================
            // TU PÍŠEŠ SVOJE RIEŠENIA
            // ================================================

            // PRÍKLAD - vymaž toto a píš vlastný kód:
            Console.WriteLine("Ahoj, som pripravený učiť sa C#! 🚀");




            // ================================================
            // VZOROVÉ RIEŠENIA - NEPOZRI SA PRED TÝM, 
            // AKO SA POKÚSIŠ VYRIEŠIŤ SAMI!
            // ================================================
            
            Console.WriteLine("\n\n--- Stlač Enter pre vzorové riešenia ---");
            Console.ReadLine();
            VzoroveRiesenia();

            Console.WriteLine("\n\nStlač ľubovoľnú klávesu pre ukončenie...");
            Console.ReadKey();
        }

        static void VzoroveRiesenia()
        {
            Console.Clear();
            Console.WriteLine("=== VZOROVÉ RIEŠENIA ===\n");

            // ================================================
            // RIEŠENIE 1: TRIEDY A MODIFIKÁTORY
            // ================================================
            Console.WriteLine("--- CVIČENIE 1: TRIEDY ---");
            var kniha1 = new Kniha("1984", "George Orwell", 328, 1949);
            var kniha2 = new Kniha("Hobbit", "J.R.R. Tolkien", 310, 1937);
            
            Console.WriteLine($"Celkový počet kníh: {Kniha.PocetKnih}\n");

            kniha1.VypisInfo();
            kniha2.VypisInfo();

            Kniznica kniznica = new Kniznica();
            kniznica.Pozicaj(kniha1);
            kniznica.Vrat(kniha1);
            Console.WriteLine();

            // ================================================
            // RIEŠENIE 2: STATIC
            // ================================================
            Console.WriteLine("--- CVIČENIE 2: STATIC ---");
            Console.WriteLine($"20°C = {Konvertor.CelsiaNaFahrenheit(20):F1}°F");
            Console.WriteLine($"68°F = {Konvertor.FahrenheitNaCelsia(68):F1}°C");
            Console.WriteLine($"100 km = {Konvertor.KilometreNaMile(100):F2} mil");
            Console.WriteLine($"62.14 mil = {Konvertor.MileNaKilometre(62.14):F0} km");
            Console.WriteLine($"70 kg = {Konvertor.KilogramyNaLibry(70):F2} lb");
            Console.WriteLine($"154.32 lb = {Konvertor.LibryNaKilogramy(154.32):F0} kg\n");

            var pocitadlo1 = new Pocitadlo();
            pocitadlo1.Inkrementuj();
            pocitadlo1.Inkrementuj();
            
            var pocitadlo2 = new Pocitadlo();
            pocitadlo2.Inkrementuj();
            
            pocitadlo1.VypisStav("Počítadlo 1");
            pocitadlo2.VypisStav("Počítadlo 2");
            Console.WriteLine();

            // ================================================
            // RIEŠENIE 3: EXCEPTIONS
            // ================================================
            Console.WriteLine("--- CVIČENIE 3: EXCEPTIONS ---");
            
            var kalk = new Kalkulator();
            kalk.TestujDelenie(10, 2);
            kalk.TestujDelenie(10, 0);
            
            Console.WriteLine();
            
            var ucet = new BankovyUcet(1000);
            ucet.Vloz(500);
            ucet.Vyber(300);
            ucet.Vyber(2000);
            Console.WriteLine();

            // ================================================
            // RIEŠENIE 4: LINQ ZÁKLADY
            // ================================================
            Console.WriteLine("--- CVIČENIE 4: LINQ ZÁKLADY ---");
            LinqCvicenie4();
            Console.WriteLine();

            // ================================================
            // RIEŠENIE 5: LINQ POKROČILÉ
            // ================================================
            Console.WriteLine("--- CVIČENIE 5: LINQ POKROČILÉ ---");
            LinqCvicenie5();
        }

        static void LinqCvicenie4()
        {
            int[] cisla = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            // 1. Párne čísla
            var parne = cisla.Where(c => c % 2 == 0);
            Console.WriteLine("Párne čísla: " + string.Join(", ", parne));

            // 2. Čísla > 10
            var velke = cisla.Where(c => c > 10);
            Console.WriteLine("Čísla > 10: " + string.Join(", ", velke));

            // 3. Súčet nepárnych
            var sucetNeparnych = cisla.Where(c => c % 2 != 0).Sum();
            Console.WriteLine($"Súčet nepárnych: {sucetNeparnych}");

            // 4. Priemer párnych
            var priemerParnych = cisla.Where(c => c % 2 == 0).Average();
            Console.WriteLine($"Priemer párnych: {priemerParnych}");

            // 5. Prvé 3 > 8
            var prve3 = cisla.Where(c => c > 8).Take(3);
            Console.WriteLine("Prvé 3 > 8: " + string.Join(", ", prve3));

            // 6. Existuje > 20?
            var existuje = cisla.Any(c => c > 20);
            Console.WriteLine($"Existuje > 20? {existuje}");

            // 7. Všetky kladné?
            var vsetkyKladne = cisla.All(c => c > 0);
            Console.WriteLine($"Všetky kladné? {vsetkyKladne}");

            // 8. Druhé mocniny párnych
            var mocniny = cisla.Where(c => c % 2 == 0).Select(c => c * c);
            Console.WriteLine("Druhé mocniny párnych: " + string.Join(", ", mocniny));

            // 9. Reťazec
            var retazec = string.Join(", ", cisla);
            Console.WriteLine($"Reťazec: {retazec}");
        }

        static void LinqCvicenie5()
        {
            var studenti = new[]
            {
                new { Id = 1, Meno = "Peter", Vek = 20 },
                new { Id = 2, Meno = "Jana", Vek = 22 },
                new { Id = 3, Meno = "Michal", Vek = 20 },
                new { Id = 4, Meno = "Eva", Vek = 21 }
            };

            var znamky = new[]
            {
                new { StudentId = 1, Predmet = "Matematika", Znamka = 1 },
                new { StudentId = 1, Predmet = "Fyzika", Znamka = 2 },
                new { StudentId = 2, Predmet = "Matematika", Znamka = 1 },
                new { StudentId = 2, Predmet = "Fyzika", Znamka = 1 },
                new { StudentId = 3, Predmet = "Matematika", Znamka = 3 },
                new { StudentId = 4, Predmet = "Matematika", Znamka = 2 }
            };

            // 1. Zoskupenie podľa veku
            Console.WriteLine("Študenti podľa veku:");
            var podlaVeku = studenti.GroupBy(s => s.Vek);
            foreach (var skupina in podlaVeku)
            {
                var mena = string.Join(", ", skupina.Select(s => s.Meno));
                Console.WriteLine($"  Vek {skupina.Key}: {mena}");
            }

            // 2. Priemerný vek
            var priemernyVek = studenti.Average(s => s.Vek);
            Console.WriteLine($"\nPriemerný vek: {priemernyVek}");

            // 3. Join
            Console.WriteLine("\nŠtudenti a známky:");
            var spojene = studenti.Join(
                znamky,
                s => s.Id,
                z => z.StudentId,
                (s, z) => new { s.Meno, z.Predmet, z.Znamka }
            );
            foreach (var item in spojene.Take(3))
            {
                Console.WriteLine($"  {item.Meno} - {item.Predmet}: {item.Znamka}");
            }
            Console.WriteLine("  ...");

            // 4. Najlepší priemer
            var priemery = studenti
                .GroupJoin(znamky, s => s.Id, z => z.StudentId, (s, sz) => new
                {
                    s.Meno,
                    Priemer = sz.Any() ? sz.Average(z => z.Znamka) : 0
                })
                .OrderBy(x => x.Priemer);
            
            var najlepsi = priemery.First();
            Console.WriteLine($"\nNajlepší priemer má: {najlepsi.Meno} ({najlepsi.Priemer:F1})");

            // 5. Študenti s jednotkou
            var sJednotkou = studenti
                .Join(znamky, s => s.Id, z => z.StudentId, (s, z) => new { s.Meno, z.Znamka })
                .Where(x => x.Znamka == 1)
                .Select(x => x.Meno)
                .Distinct();
            Console.WriteLine($"\nŠtudenti s jednotkou: {string.Join(", ", sJednotkou)}");

            // 6. Počet známok
            Console.WriteLine("\nPočet známok:");
            var pocty = studenti
                .GroupJoin(znamky, s => s.Id, z => z.StudentId, (s, sz) => new
                {
                    s.Meno,
                    Pocet = sz.Count()
                });
            foreach (var p in pocty)
            {
                Console.WriteLine($"  {p.Meno}: {p.Pocet}");
            }

            // 7. Najhorší predmet
            var najhorsi = znamky
                .GroupBy(z => z.Predmet)
                .Select(g => new { Predmet = g.Key, Priemer = g.Average(z => z.Znamka) })
                .OrderByDescending(x => x.Priemer)
                .First();
            Console.WriteLine($"\nNajhorší predmet: {najhorsi.Predmet} (priemer: {najhorsi.Priemer:F1})");
        }
    }

    // ================================================
    // RIEŠENIE 1: KNIHA
    // ================================================
    public class Kniha
    {
        private string nazov;
        private string autor;
        private int pocetStran;
        private int rokVydania;

        public static int PocetKnih { get; private set; } = 0;

        public string Nazov
        {
            get => nazov;
            set => nazov = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Autor
        {
            get => autor;
            set => autor = value ?? throw new ArgumentNullException(nameof(value));
        }

        public int PocetStran
        {
            get => pocetStran;
            set => pocetStran = value > 0 ? value : throw new ArgumentException("Počet strán musí byť kladný");
        }

        public int RokVydania
        {
            get => rokVydania;
            set => rokVydania = value;
        }

        public bool JeStara => DateTime.Now.Year - rokVydania > 50;

        public Kniha(string nazov, string autor, int pocetStran, int rokVydania)
        {
            Nazov = nazov;
            Autor = autor;
            PocetStran = pocetStran;
            RokVydania = rokVydania;
            PocetKnih++;
            Console.WriteLine($"Kniha vytvorená: {nazov} ({autor})");
        }

        public void VypisInfo()
        {
            Console.WriteLine($"\nKniha: {Nazov}");
            Console.WriteLine($"Autor: {Autor}");
            Console.WriteLine($"Počet strán: {PocetStran}");
            Console.WriteLine($"Rok vydania: {RokVydania}");
            Console.WriteLine($"Je stará? {(JeStara ? "Áno" : "Nie")}");
        }
    }

    public interface IPozicatelne
    {
        void Pozicaj(Kniha kniha);
        void Vrat(Kniha kniha);
    }

    public class Kniznica : IPozicatelne
    {
        public void Pozicaj(Kniha kniha)
        {
            Console.WriteLine($"Kniha {kniha.Nazov} bola požičaná.");
        }

        public void Vrat(Kniha kniha)
        {
            Console.WriteLine($"Kniha {kniha.Nazov} bola vrátená.");
        }
    }

    // ================================================
    // RIEŠENIE 2: STATIC
    // ================================================
    public static class Konvertor
    {
        public static double CelsiaNaFahrenheit(double celsius)
        {
            return celsius * 9 / 5 + 32;
        }

        public static double FahrenheitNaCelsia(double fahrenheit)
        {
            return (fahrenheit - 32) * 5 / 9;
        }

        public static double KilometreNaMile(double km)
        {
            return km * 0.621371;
        }

        public static double MileNaKilometre(double miles)
        {
            return miles / 0.621371;
        }

        public static double KilogramyNaLibry(double kg)
        {
            return kg * 2.20462;
        }

        public static double LibryNaKilogramy(double lb)
        {
            return lb / 2.20462;
        }
    }

    public class Pocitadlo
    {
        public static int celkovyPocet = 0;
        private int mojPocet = 0;

        static Pocitadlo()
        {
            Console.WriteLine("Inicializácia počítadla");
        }

        public void Inkrementuj()
        {
            mojPocet++;
            celkovyPocet++;
        }

        public void VypisStav(string nazov)
        {
            Console.WriteLine($"{nazov} - Môj: {mojPocet}, Celkový: {celkovyPocet}");
        }
    }

    // ================================================
    // RIEŠENIE 3: EXCEPTIONS
    // ================================================
    public class InvalidCalculationException : Exception
    {
        public InvalidCalculationException(string message) : base(message) { }
    }

    public class NedostatokProstriedkovException : Exception
    {
        public decimal ChybajucaSuma { get; }

        public NedostatokProstriedkovException(decimal chybajuca) 
            : base($"Nedostatok prostriedkov")
        {
            ChybajucaSuma = chybajuca;
        }
    }

    public class Kalkulator
    {
        public double Vydel(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Delenie nulou!");

            double vysledok = a / b;
            
            if (double.IsInfinity(vysledok) || double.IsNaN(vysledok))
                throw new InvalidCalculationException("Neplatný výsledok výpočtu");

            return vysledok;
        }

        public void TestujDelenie(double a, double b)
        {
            try
            {
                double vysledok = Vydel(a, b);
                Console.WriteLine($"{a} / {b} = {vysledok}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine($"{a} / {b} = Chyba: Delenie nulou!");
            }
            catch (InvalidCalculationException ex)
            {
                Console.WriteLine($"{a} / {b} = Chyba: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Výpočet dokončený.");
            }
        }
    }

    public class BankovyUcet
    {
        private decimal zostatok;

        public BankovyUcet(decimal pociatocny)
        {
            zostatok = pociatocny;
            Console.WriteLine($"Účet vytvorený so zostatkom: {zostatok}€");
        }

        public void Vloz(decimal suma)
        {
            try
            {
                if (suma <= 0)
                    throw new ArgumentException("Suma musí byť kladná");

                zostatok += suma;
                Console.WriteLine($"Vložené: {suma}€, zostatok: {zostatok}€");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Chyba: {ex.Message}");
            }
        }

        public void Vyber(decimal suma)
        {
            try
            {
                if (suma <= 0)
                    throw new ArgumentException("Suma musí byť kladná");

                if (zostatok < suma)
                {
                    decimal chyba = suma - zostatok;
                    throw new NedostatokProstriedkovException(chyba);
                }

                zostatok -= suma;
                Console.WriteLine($"Vybrané: {suma}€, zostatok: {zostatok}€");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Chyba: {ex.Message}");
            }
            catch (NedostatokProstriedkovException ex)
            {
                Console.WriteLine($"Pokus o výber {suma}€: {ex.Message}! Chýba: {ex.ChybajucaSuma}€");
                Console.WriteLine($"Zostatok: {zostatok}€");
            }
            finally
            {
                Console.WriteLine("Operácie dokončené.");
            }
        }
    }
}
