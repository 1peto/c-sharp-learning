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

**Výnimka** je chyba, ktorá nastane počas behu programu. Bez spracovania = program spadne.

### Try-Catch-Finally

```csharp
try {
    // Kód, ktorý môže vyhodiť výnimku
    int vysledok = 10 / 0;  // DivideByZeroException
}
catch (DivideByZeroException ex) {
    // Spracovanie konkrétnej výnimky
    Console.WriteLine("Nemôžeš deliť nulou!");
    Console.WriteLine($"Detail: {ex.Message}");
}
catch (Exception ex) {
    // Všeobecné zachytenie akejkoľvek výnimky
    Console.WriteLine($"Nastala chyba: {ex.Message}");
}
finally {
    // Vykoná sa VŽDY, či nastala výnimka alebo nie
    Console.WriteLine("Upratávam zdroje...");
}
```

### Hierarchia výnimiek

```
Exception (základná trieda)
├── SystemException
│   ├── ArithmeticException
│   │   ├── DivideByZeroException
│   │   └── OverflowException
│   ├── NullReferenceException
│   ├── IndexOutOfRangeException
│   ├── InvalidOperationException
│   └── ArgumentException
│       ├── ArgumentNullException
│       └── ArgumentOutOfRangeException
├── IOException
└── ... mnoho ďalších
```

**Pravidlo:** Zachytávaj výnimky od KONKRÉTNYCH po VŠEOBECNÉ!

```csharp
try {
    // ...
}
catch (FileNotFoundException ex) { }  // Konkrétna
catch (IOException ex) { }            // Všeobecnejšia
catch (Exception ex) { }               // Najvšeobecnejšia
```

### Vytváranie vlastných výnimiek

```csharp
public class PrazdnyUcetException : Exception {
    public decimal AktualnyZostatok { get; }
    
    public PrazdnyUcetException(decimal zostatok) 
        : base($"Nedostatok prostriedkov. Zostatok: {zostatok}") {
        AktualnyZostatok = zostatok;
    }
}

// Použitie:
public void Vyber(decimal suma) {
    if (zostatok < suma) {
        throw new PrazdnyUcetException(zostatok);
    }
    zostatok -= suma;
}
```

### Throw vs Throw ex

```csharp
catch (Exception ex) {
    // Zaloguj chybu...
    
    throw;     // ✅ DOBRÉ - zachová pôvodný stack trace
    throw ex;  // ❌ ZLÉ - prepíše stack trace
}
```

### When klauzula (od C# 6.0)

```csharp
try {
    DownloadFile(url);
}
catch (WebException ex) when (ex.Status == WebExceptionStatus.Timeout) {
    // Spracuj len timeout výnimky
    Console.WriteLine("Server neodpovedá, skúsim znova...");
}
catch (WebException ex) {
    // Ostatné web výnimky
    Console.WriteLine($"Sieťová chyba: {ex.Message}");
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
