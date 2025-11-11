# 🎓 C# Komplexný Výukový Kurz

Vitaj! Tento projekt obsahuje komplexný výukový materiál pre C# pokrývajúci:

- ✅ **Triedy a Modifikátory Prístupu** (public, private, protected, internal, interface)
- ✅ **Static Keyword** (static polia, metódy, triedy, konštruktory)
- ✅ **Exception Handling** (try-catch-finally, vlastné výnimky)
- ✅ **LINQ** (Language Integrated Query - komplexné príklady)
- ✅ **Praktické Cvičenia** (na precvičenie naučeného)

---

## 📁 Štruktúra Projektu

```
CSharpLearning/
├── README.md              ← Kompletná teória (ČÍTAj NAJPRV!)
├── HOW_TO_RUN.md          ← Návod na spustenie
│
├── 01_Classes/            ← Triedy a modifikátory prístupu
│   ├── Program.cs         ← 7 praktických príkladov
│   └── 01_Classes.csproj
│
├── 02_Static/             ← Static keyword
│   ├── Program.cs         ← 9 praktických príkladov
│   └── 02_Static.csproj
│
├── 03_Exceptions/         ← Spracovanie výnimiek
│   ├── Program.cs         ← 9 príkladov + reálne scenáre
│   └── 03_Exceptions.csproj
│
├── 04_LINQ/               ← LINQ queries
│   ├── Program.cs         ← 12 príkladov od základov po pokročilé
│   └── 04_LINQ.csproj
│
└── 05_Exercises/          ← Cvičenia pre teba!
    ├── EXERCISES.md       ← Zadania cvičení
    ├── Program.cs         ← Tu píšeš svoje riešenia
    └── 05_Exercises.csproj
```

---

## 🚀 Rýchly Štart

### 1. Prečítaj Teóriu
Otvor a preštuduj: **`README.md`** - obsahuje celú teóriu s vysvetleniami.

### 2. Spusti Príklady

```powershell
# Prejdi do priečinka (napr. triedy)
cd CSharpLearning\01_Classes

# Skomiluj a spusti
dotnet run
```

### 3. Experimentuj
- Otvor `Program.cs` v každom projekte
- Preštuduj kód
- Zmeň ho, skús nové veci
- Opäť spusti `dotnet run`

### 4. Vyriež Cvičenia
```powershell
cd CSharpLearning\05_Exercises
```
- Prečítaj `EXERCISES.md`
- Píš riešenia do `Program.cs`
- Spusti a otestuj

---

## 📚 Odporúčané Poradie Učenia

### Deň 1: Triedy a Základy OOP
1. ✅ Prečítaj teóriu o triedach v `README.md`
2. ✅ Spusti `01_Classes` - pozri si všetky príklady
3. ✅ Experimentuj - zmeň kód, skús vlastné triedy
4. ✅ Vyriež **Cvičenie 1** v `05_Exercises`

### Deň 2: Static
1. ✅ Prečítaj teóriu o static v `README.md`
2. ✅ Spusti `02_Static` - pozri si príklady
3. ✅ Pochop rozdiel medzi static a instance
4. ✅ Vyriež **Cvičenie 2** v `05_Exercises`

### Deň 3: Exception Handling
1. ✅ Prečítaj teóriu o výnimkách v `README.md`
2. ✅ Spusti `03_Exceptions` - sleduj, ako sa chyby ošetrujú
3. ✅ Vyskúšaj vlastné výnimky
4. ✅ Vyriež **Cvičenie 3** v `05_Exercises`

### Deň 4-5: LINQ
1. ✅ Prečítaj teóriu o LINQ v `README.md`
2. ✅ Spusti `04_LINQ` - postupne prechádzaj príklady
3. ✅ Pochop Where, Select, OrderBy, GroupBy, Join
4. ✅ Vyriež **Cvičenia 4 a 5** v `05_Exercises`

### Deň 6: Komplexný Projekt
1. ✅ Vyriež **Cvičenie 6** - TODO List aplikáciu
2. ✅ Skombinuj všetky naučené koncepty
3. ✅ Experimentuj s vlastnými rozšíreniami

---

## 💡 Čo Obsahuje Každý Projekt?

### 01_Classes (Triedy)
- Public/Private/Protected/Internal modifikátory
- Interfaces (rozhrania)
- Dedičnosť (inheritance)
- Properties (get/set)
- Inkapsulácia
- Polymorfizmus

**Príklady:** Auto, BankovýÚčet, Zvieratá, Lietajúce objekty, Superman, Osoba

### 02_Static (Static Keyword)
- Static polia a properties
- Static metódy
- Static triedy
- Static konštruktory
- Singleton pattern
- Extension methods
- Cache (Dictionary)

**Príklady:** Počítadlo áut, Matematika, Pomocník, Databáza, Logger, Cache

### 03_Exceptions (Výnimky)
- Try-Catch-Finally
- Viac catch blokov
- Hierarchia výnimiek
- Vlastné výnimky
- Throw vs Throw ex
- When klauzula
- Reálne scenáre (súbory, bankovníctvo)

**Príklady:** Kalkulátor, Súborový manager, Bankový systém

### 04_LINQ (Dotazy)
- Where (filtrovanie)
- Select (transformácia)
- OrderBy (triedenie)
- GroupBy (zoskupovanie)
- First/Last/Single
- Any/All/Count
- Take/Skip (stránkovanie)
- Aggregate (redukcia)
- Join (spojenie)
- Distinct/Union/Intersect
- Deferred Execution
- Komplexný E-shop príklad

### 05_Exercises (Cvičenia)
- 6 cvičení od základných po pokročilé
- Vzorové riešenia (ale najprv skús sám!)
- Bonusové výzvy

---

## 🎯 Čo Sa Naučíš?

Po absolvovaní tohto kurzu budeš vedieť:

✅ Vytvárať komplexné objektovo-orientované aplikácie  
✅ Používať správne modifikátory prístupu (public, private...)  
✅ Rozumieť static vs instance členom  
✅ Správne spracovávať chyby pomocou výnimiek  
✅ Písať efektívne LINQ dotazy  
✅ Vytvárať vlastné triedy, rozhrania a výnimky  
✅ Debuggovať a testovať kód  
✅ Aplikovať best practices v C#  

---

## 🛠️ Požiadavky

- **.NET SDK 9.0** alebo novší ([stiahnuť tu](https://dotnet.microsoft.com/download))
- **Visual Studio Code** s C# extension (odporúčané)
- Alebo akékoľvek C# IDE (Visual Studio, Rider...)

### Overenie inštalácie
```powershell
dotnet --version
```
Malo by vypísať verziu (napr. `9.0.10`)

---

## 📖 Užitočné Odkazy

- [Oficiálna C# Dokumentácia](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [LINQ Tutorial](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)
- [.NET API Reference](https://docs.microsoft.com/en-us/dotnet/api/)

---

## 💪 Tipy Pre Efektívne Učenie

1. **Píš kód rukami** - nekopíruj, píš sám
2. **Experimentuj** - skús zmeniť kód a pozri čo sa stane
3. **Rieš chyby** - keď niečo nefunguje, snaž sa pochopiť prečo
4. **Postupuj pomaly** - lepšie dobre pochopiť ako rýchlo preletieť
5. **Praktizuj denne** - aspoň 30 minút denne je lepšie ako 5 hodín raz za týždeň
6. **Vytváraj vlastné projekty** - aplikuj naučené na vlastných nápadoch

---

## 🤔 Potrebuješ Pomoc?

Ak niečomu nerozumieš:
1. Prečítaj si teóriu v `README.md` znova
2. Pozri si príklady v príslušnom projekte
3. Použi debugger - breakpointy sú tvoj priateľ!
4. Hľadaj na [Stack Overflow](https://stackoverflow.com/questions/tagged/c%23)
5. Čítaj oficiálnu dokumentáciu

---

## 🎓 Ďalšie Kroky Po Kurze

Keď dokončíš tento kurz, môžeš pokračovať:

- **Async/Await** - asynchronné programovanie
- **Entity Framework** - práca s databázami
- **ASP.NET Core** - webové aplikácie a API
- **MAUI/WPF** - desktopové aplikácie
- **Unit Testing** - testovanie kódu
- **Design Patterns** - návrhové vzory

---

**Veľa šťastia pri učení! 🚀 Enjoy coding! 💻**

---

*Vytvorené s ❤️ pre všetkých, ktorí sa chcú naučiť C# poriadne.*
