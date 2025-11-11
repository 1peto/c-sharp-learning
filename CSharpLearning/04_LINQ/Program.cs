using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# LINQ (Language Integrated Query) ===\n");

            // ===== PRÍKLAD 1: WHERE - FILTROVANIE =====
            Console.WriteLine("--- PRÍKLAD 1: WHERE - FILTROVANIE ---");
            Priklad1_Where();
            Console.WriteLine();

            // ===== PRÍKLAD 2: SELECT - TRANSFORMÁCIA =====
            Console.WriteLine("--- PRÍKLAD 2: SELECT - TRANSFORMÁCIA ---");
            Priklad2_Select();
            Console.WriteLine();

            // ===== PRÍKLAD 3: ORDERBY - TRIEDENIE =====
            Console.WriteLine("--- PRÍKLAD 3: ORDERBY - TRIEDENIE ---");
            Priklad3_OrderBy();
            Console.WriteLine();

            // ===== PRÍKLAD 4: GROUPBY - ZOSKUPOVANIE =====
            Console.WriteLine("--- PRÍKLAD 4: GROUPBY - ZOSKUPOVANIE ---");
            Priklad4_GroupBy();
            Console.WriteLine();

            // ===== PRÍKLAD 5: FIRST, LAST, SINGLE =====
            Console.WriteLine("--- PRÍKLAD 5: FIRST, LAST, SINGLE ---");
            Priklad5_FirstLastSingle();
            Console.WriteLine();

            // ===== PRÍKLAD 6: ANY, ALL, COUNT =====
            Console.WriteLine("--- PRÍKLAD 6: ANY, ALL, COUNT ---");
            Priklad6_AnyAllCount();
            Console.WriteLine();

            // ===== PRÍKLAD 7: TAKE, SKIP - STRÁNKOVANIE =====
            Console.WriteLine("--- PRÍKLAD 7: TAKE, SKIP ---");
            Priklad7_TakeSkip();
            Console.WriteLine();

            // ===== PRÍKLAD 8: AGGREGATE - REDUKCIA =====
            Console.WriteLine("--- PRÍKLAD 8: AGGREGATE ---");
            Priklad8_Aggregate();
            Console.WriteLine();

            // ===== PRÍKLAD 9: JOIN - SPOJENIE =====
            Console.WriteLine("--- PRÍKLAD 9: JOIN ---");
            Priklad9_Join();
            Console.WriteLine();

            // ===== PRÍKLAD 10: DISTINCT, UNION, INTERSECT =====
            Console.WriteLine("--- PRÍKLAD 10: DISTINCT, UNION, INTERSECT ---");
            Priklad10_SetOperations();
            Console.WriteLine();

            // ===== PRÍKLAD 11: DEFERRED EXECUTION =====
            Console.WriteLine("--- PRÍKLAD 11: DEFERRED EXECUTION ---");
            Priklad11_DeferredExecution();
            Console.WriteLine();

            // ===== PRÍKLAD 12: KOMPLEXNÝ REÁLNY PRÍKLAD =====
            Console.WriteLine("--- PRÍKLAD 12: KOMPLEXNÝ E-SHOP PRÍKLAD ---");
            Priklad12_EshopPriklad();
            Console.WriteLine();

            Console.WriteLine("\nStlač ľubovoľnú klávesu pre ukončenie...");
            Console.ReadKey();
        }

        // ================================================
        // PRÍKLAD 1: WHERE - FILTROVANIE
        // ================================================
        static void Priklad1_Where()
        {
            int[] cisla = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Párne čísla
            var parne = cisla.Where(c => c % 2 == 0);
            Console.WriteLine("Párne čísla: " + string.Join(", ", parne));

            // Čísla väčšie ako 5
            var velke = cisla.Where(c => c > 5);
            Console.WriteLine("Väčšie ako 5: " + string.Join(", ", velke));

            // Kombinácia podmienok
            var vysledneCisla = cisla.Where(c => c > 3 && c < 8);
            Console.WriteLine("Medzi 3 a 8: " + string.Join(", ", vysledneCisla));

            // Where s indexom
            var kazdeStvrte = cisla.Where((cislo, index) => index % 4 == 0);
            Console.WriteLine("Každé 4. (index 0,4,8...): " + string.Join(", ", kazdeStvrte));
        }

        // ================================================
        // PRÍKLAD 2: SELECT - TRANSFORMÁCIA
        // ================================================
        static void Priklad2_Select()
        {
            string[] mena = { "peter", "jana", "michal", "eva" };

            // Transformuj na veľké písmená
            var velkeMena = mena.Select(m => m.ToUpper());
            Console.WriteLine("Veľké písmená: " + string.Join(", ", velkeMena));

            // Transformuj na dĺžky
            var dlzky = mena.Select(m => m.Length);
            Console.WriteLine("Dĺžky mien: " + string.Join(", ", dlzky));

            // Select do anonymných objektov
            var menaSInfo = mena.Select(m => new
            {
                PuvodneMeno = m,
                Kapitalizovane = char.ToUpper(m[0]) + m.Substring(1),
                Dlzka = m.Length
            });

            Console.WriteLine("\nMená s info:");
            foreach (var info in menaSInfo)
            {
                Console.WriteLine($"  {info.Kapitalizovane} (dĺžka: {info.Dlzka})");
            }

            // Select s indexom
            var cislovane = mena.Select((meno, index) => $"{index + 1}. {meno}");
            Console.WriteLine("\nČíslované: " + string.Join(", ", cislovane));
        }

        // ================================================
        // PRÍKLAD 3: ORDERBY - TRIEDENIE
        // ================================================
        static void Priklad3_OrderBy()
        {
            var produkty = new[]
            {
                new { Nazov = "Jablko", Cena = 0.5, Kategoria = "Ovocie" },
                new { Nazov = "Banan", Cena = 0.3, Kategoria = "Ovocie" },
                new { Nazov = "Mrkva", Cena = 0.4, Kategoria = "Zelenina" },
                new { Nazov = "Citrón", Cena = 0.7, Kategoria = "Ovocie" },
                new { Nazov = "Brokolica", Cena = 1.2, Kategoria = "Zelenina" }
            };

            // Triedenie vzostupne podľa ceny
            var podlaCeny = produkty.OrderBy(p => p.Cena);
            Console.WriteLine("Podľa ceny (vzostupne):");
            foreach (var p in podlaCeny)
                Console.WriteLine($"  {p.Nazov}: {p.Cena}€");

            // Triedenie zostupne
            var podlaNazvu = produkty.OrderByDescending(p => p.Nazov);
            Console.WriteLine("\nPodľa názvu (zostupne):");
            foreach (var p in podlaNazvu)
                Console.WriteLine($"  {p.Nazov}");

            // Viacúrovňové triedenie
            var zlozene = produkty
                .OrderBy(p => p.Kategoria)
                .ThenByDescending(p => p.Cena);
            
            Console.WriteLine("\nPodľa kategórie, potom podľa ceny:");
            foreach (var p in zlozene)
                Console.WriteLine($"  [{p.Kategoria}] {p.Nazov}: {p.Cena}€");
        }

        // ================================================
        // PRÍKLAD 4: GROUPBY - ZOSKUPOVANIE
        // ================================================
        static void Priklad4_GroupBy()
        {
            var studenti = new[]
            {
                new { Meno = "Peter", Vek = 20, Trieda = "A" },
                new { Meno = "Jana", Vek = 22, Trieda = "B" },
                new { Meno = "Michal", Vek = 20, Trieda = "A" },
                new { Meno = "Eva", Vek = 21, Trieda = "B" },
                new { Meno = "Tomáš", Vek = 20, Trieda = "A" }
            };

            // Zoskup podľa veku
            var podlaVeku = studenti.GroupBy(s => s.Vek);
            Console.WriteLine("Zoskupenie podľa veku:");
            foreach (var skupina in podlaVeku)
            {
                Console.WriteLine($"  Vek {skupina.Key}: {string.Join(", ", skupina.Select(s => s.Meno))}");
            }

            // Zoskup podľa triedy s agregáciou
            var podlaTriedy = studenti
                .GroupBy(s => s.Trieda)
                .Select(g => new
                {
                    Trieda = g.Key,
                    Pocet = g.Count(),
                    PriemernyVek = g.Average(s => s.Vek),
                    Studenti = string.Join(", ", g.Select(s => s.Meno))
                });

            Console.WriteLine("\nZoskupenie podľa triedy:");
            foreach (var t in podlaTriedy)
            {
                Console.WriteLine($"  Trieda {t.Trieda}:");
                Console.WriteLine($"    Počet: {t.Pocet}");
                Console.WriteLine($"    Priemerný vek: {t.PriemernyVek:F1}");
                Console.WriteLine($"    Študenti: {t.Studenti}");
            }
        }

        // ================================================
        // PRÍKLAD 5: FIRST, LAST, SINGLE
        // ================================================
        static void Priklad5_FirstLastSingle()
        {
            int[] cisla = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // First
            var prve = cisla.First();
            Console.WriteLine($"Prvé číslo: {prve}");

            var prveParne = cisla.First(c => c % 2 == 0);
            Console.WriteLine($"Prvé párne: {prveParne}");

            // FirstOrDefault
            var prveSte = cisla.FirstOrDefault(c => c > 100);
            Console.WriteLine($"Prvé > 100: {prveSte} (default ak nenájde)");

            // Last
            var posledne = cisla.Last();
            Console.WriteLine($"Posledné číslo: {posledne}");

            // Single - očakáva PRESNE jeden výsledok
            var tri = cisla.Single(c => c == 3);
            Console.WriteLine($"Jediné číslo = 3: {tri}");

            try
            {
                var parne = cisla.Single(c => c % 2 == 0);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("❌ Single zlyhalo - je viac párnych čísiel!");
            }

            // SingleOrDefault
            var jedenNadSto = cisla.SingleOrDefault(c => c > 100);
            Console.WriteLine($"Jediné > 100: {jedenNadSto} (default)");
        }

        // ================================================
        // PRÍKLAD 6: ANY, ALL, COUNT
        // ================================================
        static void Priklad6_AnyAllCount()
        {
            int[] cisla = { 2, 4, 6, 8, 10 };

            // Any - existuje aspoň jeden?
            bool existujeParne = cisla.Any(c => c % 2 == 0);
            Console.WriteLine($"Existuje párne číslo? {existujeParne}");

            bool existujeVelke = cisla.Any(c => c > 100);
            Console.WriteLine($"Existuje číslo > 100? {existujeVelke}");

            // All - platí pre všetky?
            bool vsetkyParne = cisla.All(c => c % 2 == 0);
            Console.WriteLine($"Všetky čísla sú párne? {vsetkyParne}");

            bool vsetkyKladne = cisla.All(c => c > 0);
            Console.WriteLine($"Všetky čísla sú kladné? {vsetkyKladne}");

            // Count
            int pocet = cisla.Count();
            Console.WriteLine($"Celkový počet: {pocet}");

            int pocetVelkych = cisla.Count(c => c > 5);
            Console.WriteLine($"Počet čísel > 5: {pocetVelkych}");

            // Sum, Min, Max, Average
            int sucet = cisla.Sum();
            int minimum = cisla.Min();
            int maximum = cisla.Max();
            double priemer = cisla.Average();

            Console.WriteLine($"Súčet: {sucet}, Min: {minimum}, Max: {maximum}, Priemer: {priemer}");
        }

        // ================================================
        // PRÍKLAD 7: TAKE, SKIP - STRÁNKOVANIE
        // ================================================
        static void Priklad7_TakeSkip()
        {
            int[] cisla = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            // Prvých 5
            var prvych5 = cisla.Take(5);
            Console.WriteLine("Prvých 5: " + string.Join(", ", prvych5));

            // Preskočiť prvých 5
            var bezPrvych5 = cisla.Skip(5);
            Console.WriteLine("Bez prvých 5: " + string.Join(", ", bezPrvych5));

            // Stránkovanie
            int velkostStranky = 4;
            Console.WriteLine($"\nStránkovanie (po {velkostStranky}):");

            for (int stranka = 1; stranka <= 4; stranka++)
            {
                var data = cisla
                    .Skip((stranka - 1) * velkostStranky)
                    .Take(velkostStranky);
                
                Console.WriteLine($"  Strana {stranka}: {string.Join(", ", data)}");
            }

            // TakeWhile, SkipWhile
            var dokialMensie7 = cisla.TakeWhile(c => c < 7);
            Console.WriteLine("\nTakeWhile (< 7): " + string.Join(", ", dokialMensie7));

            var preskocDokialMensie7 = cisla.SkipWhile(c => c < 7);
            Console.WriteLine("SkipWhile (< 7): " + string.Join(", ", preskocDokialMensie7));
        }

        // ================================================
        // PRÍKLAD 8: AGGREGATE - REDUKCIA
        // ================================================
        static void Priklad8_Aggregate()
        {
            int[] cisla = { 1, 2, 3, 4, 5 };

            // Súčet pomocou Aggregate
            int sucet = cisla.Aggregate((acc, c) => acc + c);
            Console.WriteLine($"Súčet (Aggregate): {sucet}");

            // Súčin
            int sucin = cisla.Aggregate((acc, c) => acc * c);
            Console.WriteLine($"Súčin: {sucin}");

            // S počiatočnou hodnotou
            int sucetOd100 = cisla.Aggregate(100, (acc, c) => acc + c);
            Console.WriteLine($"Súčet od 100: {sucetOd100}");

            // Zložitejší príklad - reťazec
            string[] slova = { "C#", "je", "super", "jazyk" };
            string veta = slova.Aggregate((acc, slovo) => acc + " " + slovo);
            Console.WriteLine($"Veta: {veta}");

            // S transformáciou výsledku
            string formatovane = cisla.Aggregate(
                "Čísla:",
                (acc, c) => acc + " " + c,
                vysledok => vysledok + "."
            );
            Console.WriteLine(formatovane);
        }

        // ================================================
        // PRÍKLAD 9: JOIN - SPOJENIE
        // ================================================
        static void Priklad9_Join()
        {
            var studenti = new[]
            {
                new { Id = 1, Meno = "Peter" },
                new { Id = 2, Meno = "Jana" },
                new { Id = 3, Meno = "Michal" }
            };

            var znamky = new[]
            {
                new { StudentId = 1, Predmet = "Mat", Znamka = 1 },
                new { StudentId = 2, Predmet = "Mat", Znamka = 2 },
                new { StudentId = 1, Predmet = "Fyz", Znamka = 3 },
                new { StudentId = 3, Predmet = "Mat", Znamka = 1 },
                new { StudentId = 2, Predmet = "Fyz", Znamka = 1 }
            };

            // Inner Join
            var vysledok = studenti.Join(
                znamky,
                student => student.Id,
                znamka => znamka.StudentId,
                (student, znamka) => new
                {
                    student.Meno,
                    znamka.Predmet,
                    znamka.Znamka
                }
            );

            Console.WriteLine("Join študentov a známok:");
            foreach (var v in vysledok)
            {
                Console.WriteLine($"  {v.Meno}: {v.Predmet} = {v.Znamka}");
            }

            // Group Join
            var studentiSoZnamkami = studenti.GroupJoin(
                znamky,
                student => student.Id,
                znamka => znamka.StudentId,
                (student, studentZnamky) => new
                {
                    student.Meno,
                    Znamky = studentZnamky,
                    Priemer = studentZnamky.Any() ? studentZnamky.Average(z => z.Znamka) : 0
                }
            );

            Console.WriteLine("\nŠtudenti so známkami:");
            foreach (var s in studentiSoZnamkami)
            {
                Console.WriteLine($"  {s.Meno} (priemer: {s.Priemer:F2})");
                foreach (var z in s.Znamky)
                {
                    Console.WriteLine($"    {z.Predmet}: {z.Znamka}");
                }
            }
        }

        // ================================================
        // PRÍKLAD 10: DISTINCT, UNION, INTERSECT
        // ================================================
        static void Priklad10_SetOperations()
        {
            int[] cisla1 = { 1, 2, 3, 4, 5, 5, 6 };
            int[] cisla2 = { 4, 5, 6, 7, 8 };

            // Distinct - unikátne hodnoty
            var unikatne = cisla1.Distinct();
            Console.WriteLine("Distinct: " + string.Join(", ", unikatne));

            // Union - zjednotenie (bez duplikátov)
            var zjednotenie = cisla1.Union(cisla2);
            Console.WriteLine("Union: " + string.Join(", ", zjednotenie));

            // Intersect - prienik
            var prienik = cisla1.Intersect(cisla2);
            Console.WriteLine("Intersect: " + string.Join(", ", prienik));

            // Except - rozdiel
            var rozdiel = cisla1.Except(cisla2);
            Console.WriteLine("Except (1-2): " + string.Join(", ", rozdiel));

            // Concat - spojenie (s duplikátmi)
            var spojenie = cisla1.Concat(cisla2);
            Console.WriteLine("Concat: " + string.Join(", ", spojenie));
        }

        // ================================================
        // PRÍKLAD 11: DEFERRED EXECUTION
        // ================================================
        static void Priklad11_DeferredExecution()
        {
            Console.WriteLine("Vytváram zoznam...");
            List<int> cisla = new List<int> { 1, 2, 3 };

            Console.WriteLine("Vytváram LINQ dotaz (ešte sa NEVYKONÁ)...");
            var dotaz = cisla.Where(c => c > 1);

            Console.WriteLine("Pridávam nové čísla do zoznamu...");
            cisla.Add(4);
            cisla.Add(5);

            Console.WriteLine("TERAZ iterujem dotaz (vykoná sa):");
            foreach (var c in dotaz)
            {
                Console.WriteLine($"  {c}");
            }

            Console.WriteLine("\nAk chcem vykonať IHNEĎ:");
            var ihned = cisla.Where(c => c > 1).ToList();
            Console.WriteLine("ToList() vykonal dotaz ihneď!");

            cisla.Add(6);
            Console.WriteLine("Pridané 6, ale 'ihned' sa nezmení:");
            Console.WriteLine("  Počet v 'ihned': " + ihned.Count);
        }

        // ================================================
        // PRÍKLAD 12: KOMPLEXNÝ E-SHOP PRÍKLAD
        // ================================================
        static void Priklad12_EshopPriklad()
        {
            var produkty = new[]
            {
                new Produkt { Id = 1, Nazov = "Notebook HP", Cena = 799, KategoriaId = 1, NaSkladé = 5 },
                new Produkt { Id = 2, Nazov = "Myš Logitech", Cena = 25, KategoriaId = 1, NaSkladé = 20 },
                new Produkt { Id = 3, Nazov = "Klávesnica", Cena = 45, KategoriaId = 1, NaSkladé = 15 },
                new Produkt { Id = 4, Nazov = "Tričko", Cena = 15, KategoriaId = 2, NaSkladé = 50 },
                new Produkt { Id = 5, Nazov = "Rifle", Cena = 60, KategoriaId = 2, NaSkladé = 30 },
                new Produkt { Id = 6, Nazov = "Topánky", Cena = 80, KategoriaId = 2, NaSkladé = 0 },
                new Produkt { Id = 7, Nazov = "Monitor", Cena = 299, KategoriaId = 1, NaSkladé = 8 }
            };

            var kategorie = new[]
            {
                new Kategoria { Id = 1, Nazov = "Elektronika" },
                new Kategoria { Id = 2, Nazov = "Oblečenie" }
            };

            var objednavky = new[]
            {
                new Objednavka { Id = 1, ProduktId = 1, Mnozstvo = 2, Datum = DateTime.Now.AddDays(-5) },
                new Objednavka { Id = 2, ProduktId = 2, Mnozstvo = 5, Datum = DateTime.Now.AddDays(-3) },
                new Objednavka { Id = 3, ProduktId = 4, Mnozstvo = 10, Datum = DateTime.Now.AddDays(-2) },
                new Objednavka { Id = 4, ProduktId = 1, Mnozstvo = 1, Datum = DateTime.Now.AddDays(-1) }
            };

            // 1. Produkty na sklade, zoradené podľa ceny
            Console.WriteLine("1. Produkty na sklade (najlacnejšie):");
            var naSkladé = produkty
                .Where(p => p.NaSkladé > 0)
                .OrderBy(p => p.Cena)
                .Take(3);
            
            foreach (var p in naSkladé)
                Console.WriteLine($"   {p.Nazov}: {p.Cena}€ (sklad: {p.NaSkladé})");

            // 2. Produkty podľa kategórie
            Console.WriteLine("\n2. Produkty podľa kategórie:");
            var podlaKategorií = produkty
                .Join(kategorie,
                    p => p.KategoriaId,
                    k => k.Id,
                    (p, k) => new { Produkt = p, Kategoria = k.Nazov })
                .GroupBy(x => x.Kategoria)
                .Select(g => new
                {
                    Kategoria = g.Key,
                    Pocet = g.Count(),
                    CelkovaCena = g.Sum(x => x.Produkt.Cena * x.Produkt.NaSkladé),
                    Produkty = g.Select(x => x.Produkt.Nazov)
                });

            foreach (var k in podlaKategorií)
            {
                Console.WriteLine($"   {k.Kategoria}: {k.Pocet} produktov, hodnota skladu: {k.CelkovaCena}€");
            }

            // 3. Top predávané produkty
            Console.WriteLine("\n3. Top predávané produkty:");
            var topProdukty = objednavky
                .GroupBy(o => o.ProduktId)
                .Select(g => new
                {
                    ProduktId = g.Key,
                    CelkoveMnozstvo = g.Sum(o => o.Mnozstvo),
                    PocetObjednavok = g.Count()
                })
                .Join(produkty,
                    stat => stat.ProduktId,
                    p => p.Id,
                    (stat, p) => new
                    {
                        p.Nazov,
                        stat.CelkoveMnozstvo,
                        stat.PocetObjednavok,
                        Trzba = p.Cena * stat.CelkoveMnozstvo
                    })
                .OrderByDescending(x => x.Trzba)
                .Take(3);

            foreach (var p in topProdukty)
                Console.WriteLine($"   {p.Nazov}: {p.CelkoveMnozstvo} ks, tržba: {p.Trzba}€");

            // 4. Štatistiky
            Console.WriteLine("\n4. Celkové štatistiky:");
            Console.WriteLine($"   Produktov celkom: {produkty.Count()}");
            Console.WriteLine($"   Produktov na sklade: {produkty.Count(p => p.NaSkladé > 0)}");
            Console.WriteLine($"   Priemerná cena: {produkty.Average(p => p.Cena):F2}€");
            Console.WriteLine($"   Najdrahší produkt: {produkty.OrderByDescending(p => p.Cena).First().Nazov}");
            Console.WriteLine($"   Hodnota skladu: {produkty.Sum(p => p.Cena * p.NaSkladé)}€");
        }
    }

    // ================================================
    // POMOCNÉ TRIEDY
    // ================================================

    class Produkt
    {
        public int Id { get; set; }
        public string Nazov { get; set; }
        public decimal Cena { get; set; }
        public int KategoriaId { get; set; }
        public int NaSkladé { get; set; }
    }

    class Kategoria
    {
        public int Id { get; set; }
        public string Nazov { get; set; }
    }

    class Objednavka
    {
        public int Id { get; set; }
        public int ProduktId { get; set; }
        public int Mnozstvo { get; set; }
        public DateTime Datum { get; set; }
    }
}
