using System;

namespace ClassesExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# TRIEDY A MODIFIKÁTORY PRÍSTUPU ===\n");

            // ===== PRÍKLAD 1: PUBLIC =====
            Console.WriteLine("--- PRÍKLAD 1: PUBLIC ---");
            Auto mojAuto = new Auto();
            mojAuto.Znacka = "Škoda";
            mojAuto.Model = "Octavia";
            mojAuto.RokVyroby = 2020;
            mojAuto.VypisInfo();
            mojAuto.Nastartuj();
            Console.WriteLine();

            // ===== PRÍKLAD 2: PRIVATE (Inkapsulácia) =====
            Console.WriteLine("--- PRÍKLAD 2: PRIVATE (Inkapsulácia) ---");
            BankovyUcet ucet = new BankovyUcet("SK1234567890", 1000);
            ucet.VypisZostatok();
            ucet.Vloz(500);
            ucet.Vyber(200);
            ucet.Vyber(2000); // Pokus o výber viac než máme
            Console.WriteLine();

            // ===== PRÍKLAD 3: PROTECTED (Dedičnosť) =====
            Console.WriteLine("--- PRÍKLAD 3: PROTECTED (Dedičnosť) ---");
            Pes pes = new Pes("Dunčo", 3);
            pes.VypisInfo();
            pes.Stekaj();
            
            Macka macka = new Macka("Micka", 2);
            macka.VypisInfo();
            macka.Mnavkaj();
            Console.WriteLine();

            // ===== PRÍKLAD 4: INTERNAL =====
            Console.WriteLine("--- PRÍKLAD 4: INTERNAL ---");
            InternaSluzba sluzba = new InternaSluzba();
            sluzba.VykonajOperaciu();
            Console.WriteLine();

            // ===== PRÍKLAD 5: INTERFACE =====
            Console.WriteLine("--- PRÍKLAD 5: INTERFACE ---");
            
            // Polymorfizmus cez interface
            ILetajuce[] letajuceObjekty = new ILetajuce[]
            {
                new Lietadlo("Boeing 747"),
                new Vtak("Orol"),
                new Dron("DJI Phantom")
            };

            foreach (var objekt in letajuceObjekty)
            {
                objekt.Vzliet();
                Console.WriteLine($"Maximálna výška: {objekt.MaximalnaVyska} m");
                objekt.Pristanie();
                Console.WriteLine();
            }

            // ===== PRÍKLAD 6: VIAC INTERFACEOV =====
            Console.WriteLine("--- PRÍKLAD 6: VIAC INTERFACEOV ---");
            Superman superman = new Superman();
            superman.Vzliet();
            superman.Utoc();
            superman.Obranaaj();
            Console.WriteLine();

            // ===== PRÍKLAD 7: PROPERTIES (GET/SET) =====
            Console.WriteLine("--- PRÍKLAD 7: PROPERTIES ---");
            Osoba osoba = new Osoba();
            osoba.Meno = "Peter";
            osoba.Priezvisko = "Novák";
            osoba.DatumNarodenia = new DateTime(1990, 5, 15);
            
            Console.WriteLine($"Celé meno: {osoba.CeleMeno}");
            Console.WriteLine($"Vek: {osoba.Vek} rokov");
            
            // osoba.Vek = 30; // Nejde - Vek je read-only (len get)
            Console.WriteLine();

            Console.WriteLine("\nStlač ľubovoľnú klávesu pre ukončenie...");
            Console.ReadKey();
        }
    }

    // ================================================
    // PRÍKLAD 1: PUBLIC - Verejná trieda Auto
    // ================================================
    public class Auto
    {
        // Public polia - prístupné odkiaľkoľvek
        public string Znacka;
        public string Model;
        public int RokVyroby;

        // Public metódy
        public void VypisInfo()
        {
            Console.WriteLine($"Auto: {Znacka} {Model} ({RokVyroby})");
        }

        public void Nastartuj()
        {
            Console.WriteLine("Brum brum! Auto naštartované.");
        }
    }

    // ================================================
    // PRÍKLAD 2: PRIVATE - Inkapsulácia
    // ================================================
    public class BankovyUcet
    {
        // Private polia - skryté pred vonkajším svetom
        private string cisloUctu;
        private decimal zostatok;

        // Konštruktor
        public BankovyUcet(string cislo, decimal pociatocnyZostatok)
        {
            cisloUctu = cislo;
            zostatok = pociatocnyZostatok;
            Console.WriteLine($"Vytvorený účet {cisloUctu} so zostatkom {zostatok} €");
        }

        // Public metódy poskytujú kontrolovaný prístup
        public void Vloz(decimal suma)
        {
            if (ValidujSumu(suma))
            {
                zostatok += suma;
                Console.WriteLine($"Vložené: {suma} €. Nový zostatok: {zostatok} €");
            }
        }

        public void Vyber(decimal suma)
        {
            if (ValidujSumu(suma))
            {
                if (zostatok >= suma)
                {
                    zostatok -= suma;
                    Console.WriteLine($"Vybrané: {suma} €. Nový zostatok: {zostatok} €");
                }
                else
                {
                    Console.WriteLine($"❌ Nedostatok prostriedkov! Zostatok: {zostatok} €, požadované: {suma} €");
                }
            }
        }

        public void VypisZostatok()
        {
            Console.WriteLine($"Zostatok na účte {cisloUctu}: {zostatok} €");
        }

        // Private pomocná metóda - použiteľná len v tejto triede
        private bool ValidujSumu(decimal suma)
        {
            if (suma <= 0)
            {
                Console.WriteLine("❌ Suma musí byť väčšia ako 0!");
                return false;
            }
            return true;
        }
    }

    // ================================================
    // PRÍKLAD 3: PROTECTED - Dedičnosť
    // ================================================
    public class Zivocich
    {
        // Protected - prístupné v tejto triede a v odvodených triedach
        protected string meno;
        protected int vek;
        protected string druhZivocich;

        public Zivocich(string meno, int vek, string druh)
        {
            this.meno = meno;
            this.vek = vek;
            this.druhZivocich = druh;
        }

        // Protected metóda
        protected void ZakladneInfo()
        {
            Console.WriteLine($"Živočích: {meno}, Vek: {vek} rokov, Druh: {druhZivocich}");
        }

        public virtual void VypisInfo()
        {
            ZakladneInfo();
        }
    }

    public class Pes : Zivocich
    {
        public Pes(string meno, int vek) : base(meno, vek, "Pes")
        {
        }

        public void Stekaj()
        {
            // Môžeme pristupovať k protected členom rodiča
            Console.WriteLine($"{meno} hovorí: Haf haf!");
        }

        public override void VypisInfo()
        {
            ZakladneInfo(); // Môžeme volať protected metódu
            Console.WriteLine("🐕 Šteniatko!");
        }
    }

    public class Macka : Zivocich
    {
        public Macka(string meno, int vek) : base(meno, vek, "Mačka")
        {
        }

        public void Mnavkaj()
        {
            Console.WriteLine($"{meno} hovorí: Mňau mňau!");
        }

        public override void VypisInfo()
        {
            ZakladneInfo();
            Console.WriteLine("🐱 Mačička!");
        }
    }

    // ================================================
    // PRÍKLAD 4: INTERNAL
    // ================================================
    internal class InternaSluzba
    {
        // Táto trieda je viditeľná len v rámci tohto projektu
        internal void VykonajOperaciu()
        {
            Console.WriteLine("Interná služba - viditeľná len v tomto assembly (projekte)");
        }
    }

    // ================================================
    // PRÍKLAD 5: INTERFACE
    // ================================================
    public interface ILetajuce
    {
        void Vzliet();
        void Pristanie();
        int MaximalnaVyska { get; }
    }

    public class Lietadlo : ILetajuce
    {
        private string model;
        public int MaximalnaVyska { get; } = 12000;

        public Lietadlo(string model)
        {
            this.model = model;
        }

        public void Vzliet()
        {
            Console.WriteLine($"✈️ {model}: Motory naštartované, vzlietam...");
        }

        public void Pristanie()
        {
            Console.WriteLine($"✈️ {model}: Pristávam na letisko.");
        }
    }

    public class Vtak : ILetajuce
    {
        private string druh;
        public int MaximalnaVyska { get; } = 3000;

        public Vtak(string druh)
        {
            this.druh = druh;
        }

        public void Vzliet()
        {
            Console.WriteLine($"🦅 {druh}: Mávam krídlami a vzlietam!");
        }

        public void Pristanie()
        {
            Console.WriteLine($"🦅 {druh}: Sadám na strom.");
        }
    }

    public class Dron : ILetajuce
    {
        private string model;
        public int MaximalnaVyska { get; } = 500;

        public Dron(string model)
        {
            this.model = model;
        }

        public void Vzliet()
        {
            Console.WriteLine($"🚁 {model}: Rotory sa roztáčajú, vzlietam...");
        }

        public void Pristanie()
        {
            Console.WriteLine($"🚁 {model}: Kontrolované pristátie.");
        }
    }

    // ================================================
    // PRÍKLAD 6: VIAC INTERFACEOV
    // ================================================
    public interface IBojovnik
    {
        void Utoc();
        void Obranaaj();
    }

    public class Superman : ILetajuce, IBojovnik
    {
        public int MaximalnaVyska { get; } = 20000;

        public void Vzliet()
        {
            Console.WriteLine("🦸 Superman: Vzlietam do oblakov!");
        }

        public void Pristanie()
        {
            Console.WriteLine("🦸 Superman: Pristávam na zem.");
        }

        public void Utoc()
        {
            Console.WriteLine("🦸 Superman: Laserový pohľad! Pew pew!");
        }

        public void Obranaaj()
        {
            Console.WriteLine("🦸 Superman: Nepriestrečný štít!");
        }
    }

    // ================================================
    // PRÍKLAD 7: PROPERTIES (GET/SET)
    // ================================================
    public class Osoba
    {
        // Auto-implemented properties
        public string Meno { get; set; }
        public string Priezvisko { get; set; }
        public DateTime DatumNarodenia { get; set; }

        // Read-only property (len get)
        public string CeleMeno
        {
            get { return $"{Meno} {Priezvisko}"; }
        }

        // Computed property
        public int Vek
        {
            get
            {
                int vek = DateTime.Now.Year - DatumNarodenia.Year;
                if (DateTime.Now.DayOfYear < DatumNarodenia.DayOfYear)
                    vek--;
                return vek;
            }
        }

        // Property s validáciou
        private int _bodov;
        public int Bodov
        {
            get { return _bodov; }
            set
            {
                if (value < 0)
                    _bodov = 0;
                else if (value > 100)
                    _bodov = 100;
                else
                    _bodov = value;
            }
        }
    }
}
