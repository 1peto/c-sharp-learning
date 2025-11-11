using System;
using System.IO;

namespace ExceptionExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# EXCEPTION HANDLING ===\n");

            // ===== PRÍKLAD 1: ZÁKLADNÝ TRY-CATCH =====
            Console.WriteLine("--- PRÍKLAD 1: ZÁKLADNÝ TRY-CATCH ---");
            Priklad1_ZakladnyTryCatch();
            Console.WriteLine();

            // ===== PRÍKLAD 2: VIAC CATCH BLOKOV =====
            Console.WriteLine("--- PRÍKLAD 2: VIAC CATCH BLOKOV ---");
            Priklad2_ViacCatchBlokov();
            Console.WriteLine();

            // ===== PRÍKLAD 3: FINALLY BLOK =====
            Console.WriteLine("--- PRÍKLAD 3: FINALLY BLOK ---");
            Priklad3_FinallyBlok();
            Console.WriteLine();

            // ===== PRÍKLAD 4: VLASTNÉ VÝNIMKY =====
            Console.WriteLine("--- PRÍKLAD 4: VLASTNÉ VÝNIMKY ---");
            Priklad4_VlastneVynimky();
            Console.WriteLine();

            // ===== PRÍKLAD 5: THROW VS THROW EX =====
            Console.WriteLine("--- PRÍKLAD 5: THROW VS THROW EX ---");
            Priklad5_ThrowVsThrowEx();
            Console.WriteLine();

            // ===== PRÍKLAD 6: WHEN KLAUZULA =====
            Console.WriteLine("--- PRÍKLAD 6: WHEN KLAUZULA ---");
            Priklad6_WhenKlauzula();
            Console.WriteLine();

            // ===== PRÍKLAD 7: NESTED TRY-CATCH =====
            Console.WriteLine("--- PRÍKLAD 7: NESTED TRY-CATCH ---");
            Priklad7_NestedTryCatch();
            Console.WriteLine();

            // ===== PRÍKLAD 8: REÁLNY PRÍKLAD - SÚBOROVÁ OPERÁCIA =====
            Console.WriteLine("--- PRÍKLAD 8: SÚBOROVÁ OPERÁCIA ---");
            Priklad8_SuborovaOperacia();
            Console.WriteLine();

            // ===== PRÍKLAD 9: REÁLNY PRÍKLAD - BANKOVNÍCTVO =====
            Console.WriteLine("--- PRÍKLAD 9: BANKOVNÍCTVO ---");
            Priklad9_Bankovnictvo();
            Console.WriteLine();

            Console.WriteLine("\nStlač ľubovoľnú klávesu pre ukončenie...");
            Console.ReadKey();
        }

        // ================================================
        // PRÍKLAD 1: ZÁKLADNÝ TRY-CATCH
        // ================================================
        static void Priklad1_ZakladnyTryCatch()
        {
            try
            {
                Console.WriteLine("Zadaj číslo pre delenie 100:");
                Console.Write("> ");
                string vstup = Console.ReadLine();
                
                int cislo = int.Parse(vstup);
                int vysledok = 100 / cislo;
                
                Console.WriteLine($"✅ 100 / {cislo} = {vysledok}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("❌ Chyba: Nemôžeš deliť nulou!");
            }
            catch (FormatException)
            {
                Console.WriteLine("❌ Chyba: Zadal si neplatné číslo!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Neočakávaná chyba: {ex.Message}");
            }
        }

        // ================================================
        // PRÍKLAD 2: VIAC CATCH BLOKOV (HIERARCHIA)
        // ================================================
        static void Priklad2_ViacCatchBlokov()
        {
            string[] pole = { "10", "20", "abc", "30" };

            for (int i = 0; i <= pole.Length; i++) // Úmyselne pretečenie
            {
                try
                {
                    string hodnota = pole[i];
                    int cislo = int.Parse(hodnota);
                    Console.WriteLine($"Index {i}: {cislo * 2}");
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine($"❌ Index {i} je mimo rozsahu!");
                    Console.WriteLine($"   Detail: {ex.Message}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"❌ Hodnota '{pole[i]}' nie je číslo!");
                    Console.WriteLine($"   Detail: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Iná chyba: {ex.GetType().Name}");
                    Console.WriteLine($"   {ex.Message}");
                }
            }
        }

        // ================================================
        // PRÍKLAD 3: FINALLY BLOK
        // ================================================
        static void Priklad3_FinallyBlok()
        {
            StreamWriter writer = null;
            
            try
            {
                Console.WriteLine("Otváram súbor...");
                writer = new StreamWriter("test.txt");
                
                Console.WriteLine("Zapisujem do súboru...");
                writer.WriteLine("Test riadok");
                
                // Simulácia chyby
                // throw new IOException("Simulovaná chyba!");
                
                Console.WriteLine("✅ Zápis úspešný");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"❌ Chyba pri práci so súborom: {ex.Message}");
            }
            finally
            {
                // FINALLY sa vykoná VŽDY - či nastala výnimka alebo nie
                Console.WriteLine("🧹 FINALLY: Upratávam zdroje...");
                
                if (writer != null)
                {
                    writer.Close();
                    Console.WriteLine("   Súbor uzavretý");
                }
                
                // Vymazanie testovacieho súboru
                if (File.Exists("test.txt"))
                {
                    File.Delete("test.txt");
                    Console.WriteLine("   Testovací súbor vymazaný");
                }
            }
        }

        // ================================================
        // PRÍKLAD 4: VLASTNÉ VÝNIMKY
        // ================================================
        static void Priklad4_VlastneVynimky()
        {
            BankovyUcet ucet = new BankovyUcet(500);

            try
            {
                ucet.Vyber(200);
                ucet.Vyber(400); // Toto vyhodí vlastnú výnimku
            }
            catch (NedostatokProstriedkovException ex)
            {
                Console.WriteLine($"❌ {ex.Message}");
                Console.WriteLine($"   Zostatok: {ex.AktualnyZostatok} €");
                Console.WriteLine($"   Pokus o výber: {ex.PozadovanaSuma} €");
                Console.WriteLine($"   Chýba: {ex.ChybajucaSuma} €");
            }

            try
            {
                ucet.Vloz(-50); // Záporná suma
            }
            catch (NeplatnaOperaciaException ex)
            {
                Console.WriteLine($"❌ {ex.Message}");
            }
        }

        // ================================================
        // PRÍKLAD 5: THROW VS THROW EX
        // ================================================
        static void Priklad5_ThrowVsThrowEx()
        {
            Console.WriteLine("Test THROW (zachová stack trace):");
            try
            {
                MetodaKtoraVyhadzuje_Throw();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Výnimka: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace?.Split('\n')[0]}");
            }

            Console.WriteLine("\nTest THROW EX (prepíše stack trace):");
            try
            {
                MetodaKtoraVyhadzuje_ThrowEx();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Výnimka: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace?.Split('\n')[0]}");
            }
        }

        static void MetodaKtoraVyhadzuje_Throw()
        {
            try
            {
                VnutornaMetoda();
            }
            catch (Exception)
            {
                Console.WriteLine("   Zachytená v strednej metóde");
                throw; // ✅ DOBRÉ - zachová pôvodný stack trace
            }
        }

        static void MetodaKtoraVyhadzuje_ThrowEx()
        {
            try
            {
                VnutornaMetoda();
            }
            catch (Exception ex)
            {
                Console.WriteLine("   Zachytená v strednej metóde");
                throw ex; // ❌ ZLÉ - prepíše stack trace
            }
        }

        static void VnutornaMetoda()
        {
            throw new InvalidOperationException("Chyba vo vnútornej metóde!");
        }

        // ================================================
        // PRÍKLAD 6: WHEN KLAUZULA
        // ================================================
        static void Priklad6_WhenKlauzula()
        {
            TestujWhen(5);
            TestujWhen(50);
            TestujWhen(500);
        }

        static void TestujWhen(int kod)
        {
            try
            {
                SimulujHttpChybu(kod);
            }
            catch (Exception ex) when (kod == 404)
            {
                Console.WriteLine($"❌ Stránka nenájdená (404)");
            }
            catch (Exception ex) when (kod >= 500)
            {
                Console.WriteLine($"❌ Chyba servera ({kod})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Iná chyba ({kod}): {ex.Message}");
            }
        }

        static void SimulujHttpChybu(int kod)
        {
            throw new Exception($"HTTP chyba {kod}");
        }

        // ================================================
        // PRÍKLAD 7: NESTED TRY-CATCH
        // ================================================
        static void Priklad7_NestedTryCatch()
        {
            try
            {
                Console.WriteLine("Vonkajší try blok");
                
                try
                {
                    Console.WriteLine("Vnútorný try blok");
                    int[] pole = { 1, 2, 3 };
                    Console.WriteLine(pole[10]); // IndexOutOfRangeException
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("❌ Vnútorný catch: Index mimo rozsahu");
                    throw new InvalidOperationException("Pretransformovaná výnimka");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"❌ Vonkajší catch: {ex.Message}");
            }
        }

        // ================================================
        // PRÍKLAD 8: REÁLNY PRÍKLAD - SÚBOROVÁ OPERÁCIA
        // ================================================
        static void Priklad8_SuborovaOperacia()
        {
            SuborManager manager = new SuborManager();
            
            // Test 1: Existujúci súbor (vytvoríme ho)
            manager.ZapisdoSuboru("test_data.txt", "Toto je testovací obsah.");
            string obsah = manager.PrecitajSubor("test_data.txt");
            if (obsah != null)
            {
                Console.WriteLine($"✅ Obsah súboru: {obsah}");
            }

            // Test 2: Neexistujúci súbor
            manager.PrecitajSubor("neexistujuci.txt");

            // Upratanie
            if (File.Exists("test_data.txt"))
            {
                File.Delete("test_data.txt");
            }
        }

        // ================================================
        // PRÍKLAD 9: REÁLNY PRÍKLAD - BANKOVNÍCTVO
        // ================================================
        static void Priklad9_Bankovnictvo()
        {
            BankovySystem system = new BankovySystem();
            
            system.VykonajTransakciu("SK001", 1000, 200);
            system.VykonajTransakciu("SK001", 1000, 1500);
            system.VykonajTransakciu("SK002", 500, -100);
        }
    }

    // ================================================
    // VLASTNÉ VÝNIMKY
    // ================================================
    
    public class NedostatokProstriedkovException : Exception
    {
        public decimal AktualnyZostatok { get; }
        public decimal PozadovanaSuma { get; }
        public decimal ChybajucaSuma { get; }

        public NedostatokProstriedkovException(decimal zostatok, decimal pozadovana)
            : base($"Nedostatok prostriedkov na účte")
        {
            AktualnyZostatok = zostatok;
            PozadovanaSuma = pozadovana;
            ChybajucaSuma = pozadovana - zostatok;
        }
    }

    public class NeplatnaOperaciaException : Exception
    {
        public NeplatnaOperaciaException(string message) : base(message)
        {
        }
    }

    // ================================================
    // PODPORNÉ TRIEDY
    // ================================================

    public class BankovyUcet
    {
        private decimal zostatok;

        public BankovyUcet(decimal pociatocnyZostatok)
        {
            zostatok = pociatocnyZostatok;
            Console.WriteLine($"💰 Účet vytvorený so zostatkom: {zostatok} €");
        }

        public void Vloz(decimal suma)
        {
            if (suma <= 0)
                throw new NeplatnaOperaciaException("Suma musí byť kladná!");

            zostatok += suma;
            Console.WriteLine($"✅ Vložené: {suma} €, Zostatok: {zostatok} €");
        }

        public void Vyber(decimal suma)
        {
            if (suma <= 0)
                throw new NeplatnaOperaciaException("Suma musí byť kladná!");

            if (zostatok < suma)
                throw new NedostatokProstriedkovException(zostatok, suma);

            zostatok -= suma;
            Console.WriteLine($"✅ Vybrané: {suma} €, Zostatok: {zostatok} €");
        }
    }

    public class SuborManager
    {
        public void ZapisdoSuboru(string cesta, string obsah)
        {
            try
            {
                File.WriteAllText(cesta, obsah);
                Console.WriteLine($"✅ Súbor '{cesta}' vytvorený");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"❌ Nemáš oprávnenie zapisovať do '{cesta}'");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"❌ Adresár neexistuje");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"❌ Chyba pri zápise: {ex.Message}");
            }
        }

        public string PrecitajSubor(string cesta)
        {
            try
            {
                string obsah = File.ReadAllText(cesta);
                Console.WriteLine($"✅ Súbor '{cesta}' načítaný");
                return obsah;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"❌ Súbor '{cesta}' neexistuje");
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"❌ Nemáš oprávnenie čítať '{cesta}'");
                return null;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"❌ Chyba pri čítaní: {ex.Message}");
                return null;
            }
        }
    }

    public class BankovySystem
    {
        public void VykonajTransakciu(string cisloUctu, decimal zostatok, decimal suma)
        {
            Console.WriteLine($"\n💳 Transakcia pre účet {cisloUctu}");
            
            try
            {
                ValidujCisloUctu(cisloUctu);
                ValidujSumu(suma);
                
                if (zostatok < suma)
                {
                    throw new NedostatokProstriedkovException(zostatok, suma);
                }

                // Simulácia transakcie
                decimal novyZostatok = zostatok - suma;
                Console.WriteLine($"✅ Transakcia úspešná!");
                Console.WriteLine($"   Vybratá suma: {suma} €");
                Console.WriteLine($"   Nový zostatok: {novyZostatok} €");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ Neplatné údaje: {ex.Message}");
            }
            catch (NedostatokProstriedkovException ex)
            {
                Console.WriteLine($"❌ {ex.Message}");
                Console.WriteLine($"   Zostatok: {ex.AktualnyZostatok} €");
                Console.WriteLine($"   Požadované: {ex.PozadovanaSuma} €");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Systémová chyba: {ex.Message}");
                // V reálnom systéme by sme logovali do súboru
            }
            finally
            {
                Console.WriteLine($"   Transakcia ukončená: {DateTime.Now:HH:mm:ss}");
            }
        }

        private void ValidujCisloUctu(string cislo)
        {
            if (string.IsNullOrWhiteSpace(cislo))
                throw new ArgumentException("Číslo účtu nesmie byť prázdne");

            if (cislo.Length < 5)
                throw new ArgumentException("Neplatné číslo účtu");
        }

        private void ValidujSumu(decimal suma)
        {
            if (suma <= 0)
                throw new ArgumentException("Suma musí byť kladná");

            if (suma > 10000)
                throw new ArgumentException("Suma presahuje maximálny limit");
        }
    }
}
