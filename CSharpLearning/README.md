# C# Komplexný Výukový Materiál

Vitaj v komplexnom kurze C#! Tento materiál pokrýva základné aj pokročilé témy s praktickými príkladmi.

## 📚 Obsah

1. [Triedy a Modifikátory Prístupu](#1-triedy-a-modifikátory-prístupu)
2. [Static Keyword](#2-static-keyword)
3. [Spracovanie Výnimiek (Exception Handling)](#3-spracovanie-výnimiek)
4. [LINQ (Language Integrated Query)](#4-linq)
5. [Praktické Cvičenia](#5-praktické-cvičenia)

---

## 1. Triedy a Modifikátory Prístupu

### Čo je trieda?
**Trieda** je šablóna (blueprint) pre vytvorenie objektov. Obsahuje:
- **Polia (fields)** - premenné, ktoré uchovávajú stav objektu
- **Properties** - vlastnosti s get/set prístupom
- **Metódy** - funkcie, ktoré definujú správanie objektu
- **Konštruktory** - špeciálne metódy pre inicializáciu objektu

### Modifikátory prístupu

#### `public` - Verejný prístup
- Prístupný odkiaľkoľvek
- Používa sa pre API, ktoré má byť dostupné zvonku

```csharp
public class Auto {
    public string Znacka;  // Prístupné odkiaľkoľvek
    public void Zavod() { }  // Metóda dostupná všade
}
```

#### `private` - Súkromný prístup
- Prístupný LEN v rámci tej istej triedy
- Štandardne pre inkapsuláciu (skrytie detailov)

```csharp
public class BankovyUcet {
    private decimal zostatok;  // Skryté pred vonkajším svetom
    
    private void ValidujPrevodku() { }  // Len interná metóda
}
```

#### `protected` - Chránený prístup
- Prístupný v triede a v odvodených (zdedenych) triedach
- Používa sa pri dedičnosti

```csharp
public class Zivocich {
    protected string druhZivocich;  // Dostupné v odvodených triedach
}

public class Pes : Zivocich {
    public void VypisInfo() {
        Console.WriteLine(druhZivocich);  // OK - protected je dostupný
    }
}
```

#### `internal` - Interný prístup
- Prístupný len v rámci toho istého assembly (projektu)
- Výchozí pre triedy, ak neuvedieme modifikátor

```csharp
internal class InternaSluzba {
    // Viditeľná len v tomto projekte
}
```

#### `protected internal`
- Kombinácia protected A internal
- Prístupný v tom istom assembly ALEBO v odvodených triedach

### Interface (Rozhranie)

**Interface** definuje kontrakt - čo musí trieda implementovať, ale nie ako.

```csharp
public interface ILetajuce {
    void Vzliet();
    void Pristanie();
    int MaximalnaVyska { get; }
}

public class Lietadlo : ILetajuce {
    public int MaximalnaVyska { get; } = 10000;
    
    public void Vzliet() {
        Console.WriteLine("Lietadlo vzlieta");
    }
    
    public void Pristanie() {
        Console.WriteLine("Lietadlo pristáva");
    }
}
```

**Kedy použiť interface?**
- Keď chceme definovať schopnosti, ktoré môže mať viac nepríbuzných tried
- Pre dependency injection a testovateľnosť
- Keď trieda môže mať viac "rolí" (C# podporuje viac interfaceov, ale len jednu základnú triedu)

---

## 2. Static Keyword

### Čo znamená `static`?

`static` znamená, že člen patrí **triede samotnej**, nie konkrétnej inštancii objektu.

### Static polia a properties

```csharp
public class Counter {
    public static int PocetVytvorenych = 0;  // Zdieľané medzi všetkými inštanciami
    private int mojeCislo;
    
    public Counter() {
        PocetVytvorenych++;
        mojeCislo = PocetVytvorenych;
    }
}

// Použitie:
Counter c1 = new Counter();
Counter c2 = new Counter();
Console.WriteLine(Counter.PocetVytvorenych);  // 2
```

**Kedy použiť static polia?**
- Pre zdieľaný stav medzi všetkými inštanciami
- Pre konštanty a konfiguračné hodnoty
- Pre počítadlá, cache a podobne

### Static metódy

```csharp
public class Matematika {
    public static int Scitaj(int a, int b) {
        return a + b;
    }
    
    public static double Odmocnina(double x) {
        return Math.Sqrt(x);
    }
}

// Použitie - bez vytvárania objektu:
int vysledok = Matematika.Scitaj(5, 3);
```

**Pravidlo:** Static metóda **NEMÔŽE** pristupovať k non-static členom!

```csharp
public class Osoba {
    private string meno;
    
    public static void VypisInfo() {
        Console.WriteLine(meno);  // ❌ CHYBA! Static nemôže pristúpiť k non-static
    }
}
```

### Static triedy

**Static trieda** môže obsahovať LEN static členy a nemožno z nej vytvoriť inštanciu.

```csharp
public static class Pomocnik {
    public static void VypisHviezdy(int pocet) {
        Console.WriteLine(new string('*', pocet));
    }
}

// Pomocnik h = new Pomocnik();  // ❌ NEJDE!
Pomocnik.VypisHviezdy(10);  // ✅ OK
```

**Príklady static tried v .NET:**
- `Math` - matematické funkcie
- `Console` - vstup/výstup
- `File` - práca so súbormi
- `Convert` - konverzie typov

### Static konštruktor

Zavolá sa AUTOMATICKY ŤAŽIVO PREDTÝM, než sa prvýkrát použije trieda.

```csharp
public class Databaza {
    private static string connectionString;
    
    static Databaza() {
        // Zavolá sa len raz, pri prvom použití
        Console.WriteLine("Inicializácia databázy...");
        connectionString = "Server=localhost;...";
    }
    
    public static void Pripoj() {
        Console.WriteLine("Pripájam sa: " + connectionString);
    }
}
```

---

## 3. Spracovanie Výnimiek (Exception Handling)

### Čo je výnimka (Exception)?

**Výnimka** (exception) je špeciálny objekt, ktorý reprezentuje chybu alebo neočakávanú situáciu, ktorá nastala počas vykonávania programu. Keď nastane výnimka a nie je ošetrená, program sa ukončí (spadne) a zobrazí chybovú hlášku.

**Prečo používať výnimky?**
- Oddeľujú kód spracovania chýb od normálneho kódu
- Propagujú chyby nahor cez call stack
- Poskytujú detailné informácie o chybe (typ, správa, miesto vzniku)
- Umožňujú centralizované spracovanie chýb

### Základná štruktúra: Try-Catch-Finally

```csharp
try {
    // Kód, ktorý môže vyhodiť výnimku (riziková časť)
    int vysledok = 10 / 0;  // DivideByZeroException
}
catch (DivideByZeroException ex) {
    // Spracovanie konkrétnej výnimky
    Console.WriteLine("Nemôžeš deliť nulou!");
    Console.WriteLine($"Detail: {ex.Message}");
}
catch (Exception ex) {
    // Zachytenie všetkých ostatných výnimiek
    Console.WriteLine($"Nastala neočakávaná chyba: {ex.Message}");
}
finally {
    // Vykoná sa VŽDY - či výnimka nastala alebo nie
    // Používa sa na upratanie zdrojov (uzatvorenie súborov, spojení, atď.)
    Console.WriteLine("Upratávam zdroje...");
}
```

**Ako to funguje:**
1. **try** blok obsahuje kód, ktorý môže vyhodiť výnimku
2. **catch** blok zachytí výnimku a spracuje ju (môže byť viac catch blokov)
3. **finally** blok sa vykoná vždy - používa sa na cleanup operácie
4. Ak výnimka nie je zachytená, propaguje sa vyššie v call stacku

### Praktické príklady Try-Catch

#### Príklad 1: Práca so súbormi

```csharp
string cesta = "subor.txt";

try {
    string obsah = File.ReadAllText(cesta);
    Console.WriteLine(obsah);
}
catch (FileNotFoundException ex) {
    Console.WriteLine($"Súbor '{cesta}' neexistuje!");
    Console.WriteLine($"Skontroluj, či je cesta správna: {ex.FileName}");
}
catch (UnauthorizedAccessException ex) {
    Console.WriteLine("Nemáš oprávnenie na čítanie tohto súboru!");
}
catch (IOException ex) {
    Console.WriteLine($"Chyba pri čítaní súboru: {ex.Message}");
}
finally {
    Console.WriteLine("Pokus o čítanie súboru dokončený.");
}
```

#### Príklad 2: Konverzia vstupu od používateľa

```csharp
Console.Write("Zadaj svoje vek: ");
string vstup = Console.ReadLine();

try {
    int vek = int.Parse(vstup);
    
    if (vek < 0 || vek > 150) {
        throw new ArgumentOutOfRangeException(nameof(vek), "Vek musí byť medzi 0 a 150!");
    }
    
    Console.WriteLine($"Tvoj vek je: {vek}");
}
catch (FormatException) {
    Console.WriteLine("Zadal si neplatné číslo! Zadaj celé číslo.");
}
catch (ArgumentOutOfRangeException ex) {
    Console.WriteLine($"Neplatný vek: {ex.Message}");
}
catch (Exception ex) {
    Console.WriteLine($"Nastala neočakávaná chyba: {ex.Message}");
}
```

#### Príklad 3: Práca s poľom (Array)

```csharp
int[] cisla = { 10, 20, 30, 40, 50 };

try {
    Console.Write("Zadaj index (0-4): ");
    int index = int.Parse(Console.ReadLine());
    
    int hodnota = cisla[index];
    Console.WriteLine($"Hodnota na indexe {index} je: {hodnota}");
}
catch (IndexOutOfRangeException) {
    Console.WriteLine($"Index mimo rozsahu! Pole má len {cisla.Length} prvkov (indexy 0-{cisla.Length - 1})");
}
catch (FormatException) {
    Console.WriteLine("Zadaj platné celé číslo!");
}
```

#### Príklad 4: Null Reference

```csharp
string text = null;

try {
    // Pokus o volanie metódy na null objekte
    int dlzka = text.Length;  // NullReferenceException
}
catch (NullReferenceException) {
    Console.WriteLine("Premenná 'text' je null! Nemôžeš volať Length na null.");
    
    // Oprava:
    text = text ?? "prázdny text";  // Null-coalescing operator
    Console.WriteLine($"Opravená hodnota: {text}");
}
```

### Hierarchia výnimiek

Všetky výnimky dedia z triedy `Exception`. Pochopenie hierarchie je kľúčové pre správne zachytávanie.

```
Exception (základná trieda pre všetky výnimky)
│
├── SystemException (systémové výnimky)
│   │
│   ├── ArithmeticException
│   │   ├── DivideByZeroException (delenie nulou)
│   │   ├── OverflowException (pretečenie čísla)
│   │   └── NotFiniteNumberException
│   │
│   ├── NullReferenceException (prístup k null objektu)
│   ├── IndexOutOfRangeException (index mimo rozsahu)
│   ├── InvalidOperationException (neplatná operácia)
│   ├── InvalidCastException (neplatné pretypovanie)
│   │
│   └── ArgumentException (neplatný argument)
│       ├── ArgumentNullException (null argument)
│       └── ArgumentOutOfRangeException (argument mimo rozsahu)
│
├── IOException (vstup/výstup)
│   ├── FileNotFoundException (súbor nenájdený)
│   ├── DirectoryNotFoundException
│   ├── PathTooLongException
│   └── EndOfStreamException
│
├── FormatException (zlý formát)
├── NotSupportedException (nepodporované)
├── TimeoutException (timeout)
└── ... mnoho ďalších
```

### Pravidlá zachytávania výnimiek

#### ⚠️ Pravidlo #1: Zachytávaj od KONKRÉTNYCH po VŠEOBECNÉ

```csharp
try {
    string obsah = File.ReadAllText("data.txt");
}
catch (FileNotFoundException ex) {      // ✅ Najkonkrétnejšia
    Console.WriteLine("Súbor neexistuje");
}
catch (IOException ex) {                 // ✅ Všeobecnejšia (parent)
    Console.WriteLine("Chyba I/O");
}
catch (Exception ex) {                   // ✅ Najvšeobecnejšia (root)
    Console.WriteLine("Akákoľvek iná chyba");
}
```

**❌ NESPRÁVNE:**
```csharp
try {
    // ...
}
catch (Exception ex) {              // ❌ Najvšeobecnejšia prvá = zachytí všetko
    Console.WriteLine("Chyba");
}
catch (FileNotFoundException ex) {  // ❌ NIKDY sa nevykoná! (unreachable code)
    Console.WriteLine("Súbor neexistuje");
}
```

#### Pravidlo #2: Nezachytávaj výnimky, ktoré nevieš spracovať

```csharp
// ❌ ZLÉ - "prehltneš" výnimku bez spracovania
try {
    NiecoNebezpecne();
}
catch (Exception) {
    // Nič... (chyba sa stratila)
}

// ✅ DOBRÉ - zachyť len to, čo vieš spracoovať
try {
    NiecoNebezpecne();
}
catch (SpecifickaVynimka ex) {
    // Spracuj špecifickú výnimku
    Console.WriteLine($"Riešim problém: {ex.Message}");
}
// Ostatné výnimky sa propagujú ďalej
```

### Vlastnosti objektu Exception

Každý objekt výnimky obsahuje užitočné informácie:

```csharp
try {
    throw new InvalidOperationException("Niečo sa pokazilo!");
}
catch (Exception ex) {
    Console.WriteLine($"Typ: {ex.GetType().Name}");           // Typ výnimky
    Console.WriteLine($"Správa: {ex.Message}");               // Popis chyby
    Console.WriteLine($"Stack Trace:\n{ex.StackTrace}");      // Kde nastala
    Console.WriteLine($"Source: {ex.Source}");                // Odkiaľ pochádza
    Console.WriteLine($"Target Site: {ex.TargetSite}");       // Ktorá metóda
    
    if (ex.InnerException != null) {                          // Vnorená výnimka
        Console.WriteLine($"Vnorená: {ex.InnerException.Message}");
    }
}
```

### Vyhadzovanie výnimiek (throw)

#### Základné použitie throw

```csharp
public void NastavVek(int vek) {
    if (vek < 0) {
        throw new ArgumentException("Vek nemôže byť záporný!");
    }
    if (vek > 150) {
        throw new ArgumentOutOfRangeException(nameof(vek), vek, "Vek je príliš vysoký!");
    }
    
    this.vek = vek;
}
```

#### Throw vs Throw ex - DÔLEŽITÝ rozdiel!

```csharp
try {
    NebezpecnaMetoda();
}
catch (Exception ex) {
    LogujChybu(ex);
    
    throw;      // ✅ DOBRÉ - zachová PÔVODNÝ stack trace
    // throw ex; // ❌ ZLÉ - PREPÍŠE stack trace (stratíš informáciu, kde chyba vznikla)
}
```

**Prečo je to dôležité?**
```csharp
// Originálny stack trace:
//   at MetodaC() line 50
//   at MetodaB() line 30  
//   at MetodaA() line 10

// throw;    => Zachová celý stack trace ✅
// throw ex; => Stack trace začne od aktuálneho miesta ❌
```

#### Wrapping výnimiek (InnerException)

```csharp
public void NacitajData(string cesta) {
    try {
        string json = File.ReadAllText(cesta);
        var data = JsonConvert.DeserializeObject(json);
    }
    catch (FileNotFoundException ex) {
        // Zabal originálnu výnimku do novej s lepším kontextom
        throw new DataException($"Nepodarilo sa načítať dáta z '{cesta}'", ex);
    }
    catch (JsonException ex) {
        throw new DataException($"Súbor '{cesta}' obsahuje neplatný JSON", ex);
    }
}

// Použitie:
try {
    NacitajData("config.json");
}
catch (DataException ex) {
    Console.WriteLine($"Chyba: {ex.Message}");
    Console.WriteLine($"Pôvodná príčina: {ex.InnerException?.Message}");
}
```

### Vytváranie vlastných výnimiek

Vlastné výnimky používaj, keď štandardné nepokrývajú tvoj prípad.

#### Jednoduchá vlastná výnimka

```csharp
public class PrazdnyUcetException : Exception {
    public decimal AktualnyZostatok { get; }
    public decimal PozadovanaSuma { get; }
    
    public PrazdnyUcetException(decimal zostatok, decimal suma) 
        : base($"Nedostatok prostriedkov. Zostatok: {zostatok} €, požadované: {suma} €") {
        AktualnyZostatok = zostatok;
        PozadovanaSuma = suma;
    }
    
    // Konštruktor s inner exception
    public PrazdnyUcetException(decimal zostatok, decimal suma, Exception innerException) 
        : base($"Nedostatok prostriedkov. Zostatok: {zostatok} €, požadované: {suma} €", innerException) {
        AktualnyZostatok = zostatok;
        PozadovanaSuma = suma;
    }
}
```

#### Použitie vlastnej výnimky

```csharp
public class BankovyUcet {
    private decimal zostatok;
    
    public void Vyber(decimal suma) {
        if (suma <= 0) {
            throw new ArgumentException("Suma musí byť väčšia ako 0", nameof(suma));
        }
        
        if (zostatok < suma) {
            throw new PrazdnyUcetException(zostatok, suma);
        }
        
        zostatok -= suma;
        Console.WriteLine($"Vybraté: {suma} €. Nový zostatok: {zostatok} €");
    }
}

// Použitie:
var ucet = new BankovyUcet();
try {
    ucet.Vyber(1000);
}
catch (PrazdnyUcetException ex) {
    Console.WriteLine(ex.Message);
    Console.WriteLine($"Chýba ti: {ex.PozadovanaSuma - ex.AktualnyZostatok} €");
}
catch (ArgumentException ex) {
    Console.WriteLine($"Neplatný argument: {ex.Message}");
}
```

### When klauzula - Filter výnimiek (C# 6.0+)

**When** umožňuje zachytiť výnimku len pri splnení podmienky.

#### Základný príklad

```csharp
try {
    DownloadFile(url);
}
catch (WebException ex) when (ex.Status == WebExceptionStatus.Timeout) {
    Console.WriteLine("Server neodpovedá, skúsim znova...");
    Retry();
}
catch (WebException ex) when (ex.Status == WebExceptionStatus.ConnectFailure) {
    Console.WriteLine("Nepodarilo sa pripojiť k serveru");
}
catch (WebException ex) {
    Console.WriteLine($"Iná sieťová chyba: {ex.Status}");
}
```

#### When s viacerými podmienkami

```csharp
try {
    ProcessFile(fileName);
}
catch (IOException ex) when (ex.Message.Contains("locked")) {
    Console.WriteLine("Súbor je používaný iným procesom. Čakám...");
    Thread.Sleep(1000);
    Retry();
}
catch (IOException ex) when (IsDiskFull(ex)) {
    Console.WriteLine("Disk je plný! Uvoľni miesto.");
}
catch (IOException ex) {
    Console.WriteLine($"Iná I/O chyba: {ex.Message}");
}

bool IsDiskFull(IOException ex) {
    return ex.HResult == -2147024784; // Disk full error code
}
```

#### When s logovaním

```csharp
try {
    KritickaOperacia();
}
catch (Exception ex) when (LogException(ex)) {
    // Tento blok sa NIKDY nevykoná (LogException vráti false)
    // Ale výnimka sa zaloguje skôr, než sa propaguje ďalej
}

bool LogException(Exception ex) {
    Console.WriteLine($"[LOG] {DateTime.Now}: {ex.Message}");
    return false;  // Nikdy nezachyť, len zaloguj
}
```

### Finally blok - Upratovanie zdrojov

**Finally** sa vykoná VŽDY - aj keď nastane výnimka, aj keď nie.

```csharp
FileStream fs = null;
try {
    fs = new FileStream("data.txt", FileMode.Open);
    // Práca so súborom...
}
catch (IOException ex) {
    Console.WriteLine($"Chyba: {ex.Message}");
}
finally {
    // Zatvor súbor - vykoná sa VŽDY
    if (fs != null) {
        fs.Close();
        Console.WriteLine("Súbor zatvorený");
    }
}
```

### Using statement - Automatické upratovanie

**Using** je elegatnejší spôsob pre objekty implementujúce `IDisposable`.

```csharp
// Namiesto try-finally:
using (FileStream fs = new FileStream("data.txt", FileMode.Open)) {
    // Práca so súborom...
}  // Automaticky sa zavolá fs.Dispose() na konci

// C# 8.0+ using deklarácia:
using FileStream fs = new FileStream("data.txt", FileMode.Open);
// Práca so súborom...
// fs.Dispose() sa zavolá na konci scope
```

#### Viacero using statements

```csharp
using (var reader = new StreamReader("vstup.txt"))
using (var writer = new StreamWriter("vystup.txt")) {
    string riadok;
    while ((riadok = reader.ReadLine()) != null) {
        writer.WriteLine(riadok.ToUpper());
    }
}  // Oba sa automaticky zatvoria
```

### Best Practices - Osvedčené postupy

#### ✅ DOBRE

```csharp
// 1. Zachytávaj špecifické výnimky
try {
    var data = LoadData();
}
catch (FileNotFoundException ex) {
    Console.WriteLine("Súbor nebol nájdený");
}

// 2. Používaj using pre IDisposable
using (var stream = File.OpenRead("file.txt")) {
    // ...
}

// 3. Validuj vstupy a vyhadzuj výnimky včas
public void SetAge(int age) {
    if (age < 0) throw new ArgumentException("Vek nemôže byť záporný");
    this.age = age;
}

// 4. Pridaj kontext do vlastných výnimiek
throw new DataException($"Chyba pri spracovaní súboru '{fileName}'", ex);

// 5. Používaj when pre filtre
catch (IOException ex) when (ex.Message.Contains("locked")) {
    // Špecifické spracovanie
}
```

#### ❌ ZLE

```csharp
// 1. Nezachytávaj všetko
try {
    // ...
}
catch (Exception) {  // ❌ Príliš všeobecné
    // ...
}

// 2. Neprehĺtaj výnimky
catch (Exception) {  // ❌ "Tiché zlyhanie"
    // Nič...
}

// 3. Nepoužívaj výnimky na tok programu
try {
    return array[index];  // ❌
}
catch (IndexOutOfRangeException) {
    return defaultValue;
}
// Namiesto toho:
if (index >= 0 && index < array.Length) {
    return array[index];
}
return defaultValue;

// 4. Throw ex namiesto throw
catch (Exception ex) {
    throw ex;  // ❌ Stráca stack trace
}
```

### Komplexný praktický príklad

```csharp
public class UserService {
    public User GetUser(int userId) {
        // Validácia vstupu
        if (userId <= 0) {
            throw new ArgumentException("User ID musí byť kladné číslo", nameof(userId));
        }
        
        try {
            // Pokus o načítanie z databázy
            using (var connection = new SqlConnection(connectionString)) {
                connection.Open();
                
                using (var command = new SqlCommand("SELECT * FROM Users WHERE Id = @Id", connection)) {
                    command.Parameters.AddWithValue("@Id", userId);
                    
                    using (var reader = command.ExecuteReader()) {
                        if (!reader.Read()) {
                            throw new UserNotFoundException(userId);
                        }
                        
                        return new User {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2)
                        };
                    }
                }
            }
        }
        catch (SqlException ex) when (ex.Number == 53) {
            // Špecifická SQL chyba - server nedostupný
            throw new DatabaseUnavailableException("Databázový server je nedostupný", ex);
        }
        catch (SqlException ex) {
            // Iné SQL chyby
            throw new DatabaseException($"Chyba pri načítaní užívateľa {userId}", ex);
        }
        catch (UserNotFoundException) {
            // Propaguj ďalej
            throw;
        }
        catch (Exception ex) {
            // Neočakávané chyby
            LogError($"Neočakávaná chyba pri načítaní užívateľa {userId}", ex);
            throw new ServiceException("Nepodarilo sa načítať užívateľa", ex);
        }
    }
}

// Vlastné výnimky
public class UserNotFoundException : Exception {
    public int UserId { get; }
    
    public UserNotFoundException(int userId) 
        : base($"Užívateľ s ID {userId} nebol nájdený") {
        UserId = userId;
    }
}

public class DatabaseUnavailableException : Exception {
    public DatabaseUnavailableException(string message, Exception innerException) 
        : base(message, innerException) { }
}
```

---

## 4. LINQ (Language Integrated Query)

### Čo je LINQ?

**LINQ** umožňuje písať SQL-like dotazy priamo v C# na:
- Kolekcie (arrays, lists, dictionaries)
- Databázy (Entity Framework)
- XML dokumenty
- A mnoho ďalšieho

### Dva spôsoby zápisu LINQ

#### 1. Query Syntax (SQL-like)

```csharp
var vysledok = from cislo in cisla
               where cislo > 5
               orderby cislo descending
               select cislo;
```

#### 2. Method Syntax (Fluent API)

```csharp
var vysledok = cisla
    .Where(cislo => cislo > 5)
    .OrderByDescending(cislo => cislo)
    .Select(cislo => cislo);
```

**Odporúčanie:** Method syntax je používanejší a flexibilnejší.

### Základné LINQ operácie

#### Where - Filtrovanie

```csharp
int[] cisla = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Všetky párne čísla
var parne = cisla.Where(c => c % 2 == 0);

// S viacerými podmienkami
var vysledok = cisla.Where(c => c > 3 && c < 8);
```

#### Select - Transformácia

```csharp
string[] mena = { "Peter", "Jana", "Michal" };

// Transformuj na veľké písmená
var velke = mena.Select(m => m.ToUpper());

// Transformuj na objekty
var osoby = mena.Select(m => new Osoba { Meno = m });

// Select s indexom
var sIndexom = mena.Select((meno, index) => $"{index + 1}. {meno}");
```

#### OrderBy / OrderByDescending - Triedenie

```csharp
var produkty = new[] {
    new { Nazov = "Jablko", Cena = 0.5 },
    new { Nazov = "Banan", Cena = 0.3 },
    new { Nazov = "Citrón", Cena = 0.7 }
};

// Triedenie vzostupne
var podlaCeny = produkty.OrderBy(p => p.Cena);

// Triedenie zostupne
var podlaNazvu = produkty.OrderByDescending(p => p.Nazov);

// Viacúrovňové triedenie
var zlozene = produkty
    .OrderBy(p => p.Cena)
    .ThenByDescending(p => p.Nazov);
```

#### GroupBy - Zoskupovanie

```csharp
var studenti = new[] {
    new { Meno = "Peter", Vek = 20 },
    new { Meno = "Jana", Vek = 22 },
    new { Meno = "Michal", Vek = 20 }
};

// Zoskup podľa veku
var podlaVeku = studenti.GroupBy(s => s.Vek);

foreach (var skupina in podlaVeku) {
    Console.WriteLine($"Vek {skupina.Key}:");
    foreach (var student in skupina) {
        Console.WriteLine($"  - {student.Meno}");
    }
}
```

#### First / FirstOrDefault / Single

```csharp
int[] cisla = { 1, 2, 3, 4, 5 };

var prve = cisla.First();                    // 1
var prveParne = cisla.First(c => c % 2 == 0); // 2
// var prveSte = cisla.First(c => c > 10);    // ❌ Exception!

var prveSteOrNull = cisla.FirstOrDefault(c => c > 10); // 0 (default int)

// Single - očakáva PRESNE jeden výsledok
var tri = cisla.Single(c => c == 3);  // ✅ OK
// var parne = cisla.Single(c => c % 2 == 0); // ❌ Exception - viac výsledkov!
```

#### Any / All / Count

```csharp
int[] cisla = { 2, 4, 6, 8 };

bool existujeParne = cisla.Any(c => c % 2 == 0);    // true
bool vsetkyParne = cisla.All(c => c % 2 == 0);      // true
bool existujeViac10 = cisla.Any(c => c > 10);       // false

int pocet = cisla.Count();                           // 4
int pocetVelkych = cisla.Count(c => c > 5);         // 2
```

#### Take / Skip - Stránkovanie

```csharp
int[] cisla = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var prvych5 = cisla.Take(5);           // { 1, 2, 3, 4, 5 }
var bezPrvych5 = cisla.Skip(5);        // { 6, 7, 8, 9, 10 }

// Stránkovanie (strana 2, po 3 položky)
int stranaVeľkosť = 3;
int strana = 2;
var strankaData = cisla.Skip((strana - 1) * stranaVeľkosť)
                       .Take(stranaVeľkosť);  // { 4, 5, 6 }
```

#### Aggregate - Redukcia

```csharp
int[] cisla = { 1, 2, 3, 4, 5 };

// Súčet
int sucet = cisla.Aggregate((acc, c) => acc + c);  // 15

// S počiatočnou hodnotou a transformáciou
string retazec = cisla.Aggregate("Čísla:", (acc, c) => acc + " " + c);
// "Čísla: 1 2 3 4 5"
```

#### Join - Spojenie kolekcií

```csharp
var studenti = new[] {
    new { Id = 1, Meno = "Peter" },
    new { Id = 2, Meno = "Jana" }
};

var znamky = new[] {
    new { StudentId = 1, Predmet = "Mat", Znamka = 1 },
    new { StudentId = 2, Predmet = "Mat", Znamka = 2 },
    new { StudentId = 1, Predmet = "Fyz", Znamka = 3 }
};

var vysledok = studenti.Join(
    znamky,
    student => student.Id,           // Kľúč z prvej kolekcie
    znamka => znamka.StudentId,      // Kľúč z druhej kolekcie
    (student, znamka) => new {       // Výsledná transformácia
        student.Meno,
        znamka.Predmet,
        znamka.Znamka
    }
);
```

### Deferred Execution (Odložené vykonanie)

**Dôležité:** LINQ dotazy sa nevykonajú ihneď! Vykonajú sa až pri iterácii.

```csharp
List<int> cisla = new List<int> { 1, 2, 3 };

var dotaz = cisla.Where(c => c > 1);  // Dotaz sa ešte NEVYKONAL!

cisla.Add(4);  // Pridám nové číslo

foreach (var c in dotaz) {  // TERAZ sa dotaz vykoná
    Console.WriteLine(c);    // Vypíše: 2, 3, 4
}
```

**Ako vykonať dotaz ihneď?** Použiť `ToList()`, `ToArray()`, `Count()`, atď.

```csharp
var vysledok = cisla.Where(c => c > 1).ToList();  // Vykoná sa IHNEĎ
```

---

## 5. Praktické Cvičenia

Po preštudovaní teórie a príkladov nájdeš v priečinku `05_Exercises` cvičenia, kde si môžeš sám vyskúšať získané vedomosti.

### Ako postupovať:

1. **Preštuduj teóriu** v tomto README
2. **Spusti a preskúmaj príklady** v priečinkoch 01-04
3. **Vyriešiť cvičenia** v priečinku 05_Exercises
4. **Experimentuj** - menuj kód, skúšaj nové veci!

### Kompilácia a spustenie

Každý projekt môžeš skompilovať a spustiť:

```bash
# Prejdi do priečinka projektu
cd 01_Classes

# Skomiluj a spusti
dotnet run
```

---

## 📖 Užitočné odkazy

- [Microsoft C# Dokumentácia](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [LINQ Dokumentácia](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)
- [C# Programming Guide](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/)

---

**Hodně štěstí pri učení! 🚀**
