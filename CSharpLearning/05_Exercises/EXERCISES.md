# Praktické Cvičenia - C#

Vitaj v sekcii cvičení! Tu si môžeš otestovať svoje vedomosti z C#.

## 📝 Ako cvičiť

1. Prečítaj si zadanie cvičenia
2. Napíš riešenie do `Program.cs`
3. Skomiluj a spusti: `dotnet run`
4. Skontroluj výsledok
5. Porovnaj so vzorovým riešením (nižšie)

---

## Cvičenie 1: Triedy a Modifikátory Prístupu

### Zadanie
Vytvor triedu `Kniha` s nasledujúcimi požiadavkami:

- Private polia: `nazov`, `autor`, `pocetStran`, `rokVydania`
- Public properties pre všetky polia (s validáciou)
- Konštruktor, ktorý inicializuje všetky polia
- Metóda `VypisInfo()`, ktorá vypíše informácie o knihe
- Property `JeStara` (read-only), ktorá vráti true, ak je kniha staršia ako 50 rokov
- Static pole `PocetKnih`, ktoré počíta vytvorené knihy

Potom vytvor:
- Rozhranie `IPozicatelne` s metódami `Pozicaj()` a `Vrat()`
- Triedu `Kniznica`, ktorá implementuje toto rozhranie

### Očakávaný výstup
```
Kniha vytvorená: 1984 (George Orwell)
Kniha vytvorená: Hobbit (J.R.R. Tolkien)
Celkový počet kníh: 2

Kniha: 1984
Autor: George Orwell
Počet strán: 328
Rok vydania: 1949
Je stará? Áno

Kniha: Hobbit
Autor: J.R.R. Tolkien
Počet strán: 310
Rok vydania: 1937
Je stará? Áno

Kniha 1984 bola požičaná.
Kniha 1984 bola vrátená.
```

---

## Cvičenie 2: Static

### Zadanie
Vytvor static triedu `Konvertor` s nasledujúcimi metódami:

- `CelsiaNaFahrenheit(double celsius)` - konverzia teploty
- `FahrenheitNaCelsia(double fahrenheit)` - konverzia teploty
- `KilometreNaMile(double km)` - konverzia vzdialenosti
- `MileNaKilometre(double miles)` - konverzia vzdialenosti
- `KilogramyNaLibry(double kg)` - konverzia hmotnosti
- `LibryNaKilogramy(double lb)` - konverzia hmotnosti

Vytvor aj triedu `Pocitadlo` s:
- Static poľom `celkovyPocet`
- Instance poľom `mojPocet`
- Static konštruktorom, ktorý vypíše "Inicializácia počítadla"
- Metódou `Inkrementuj()`, ktorá zvýši obe počítadlá

### Očakávaný výstup
```
20°C = 68°F
68°F = 20°C
100 km = 62.14 mil
62.14 mil = 100 km
70 kg = 154.32 lb
154.32 lb = 70 kg

Inicializácia počítadla
Počítadlo 1 - Môj: 2, Celkový: 2
Počítadlo 2 - Môj: 1, Celkový: 3
```

---

## Cvičenie 3: Exception Handling

### Zadanie
Vytvor triedu `Kalkulator` s metódou `Vydel(double a, double b)`:
- Ak je `b` nula, vyhoď `DivideByZeroException`
- Ak je výsledok nekonečno alebo NaN, vyhoď vlastnú výnimku `InvalidCalculationException`

Vytvor triedu `BankovyUcet` s metódami:
- `Vloz(decimal suma)` - validuje sumu (musí byť > 0)
- `Vyber(decimal suma)` - validuje sumu a dostupnosť prostriedkov
- Vlastná výnimka `NedostatokProstriedkovException` s info o chýbajúcej sume

Vytvor program, ktorý:
1. Vyskúša kalkulátor s rôznymi vstupmi
2. Vytvorí účet a vyskúša vkladanie/výber
3. Všetky výnimky správne ošetrí pomocou try-catch-finally

### Očakávaný výstup
```
10 / 2 = 5
10 / 0 = Chyba: Delenie nulou!
Výpočet dokončený.

Účet vytvorený so zostatkom: 1000€
Vložené: 500€, zostatok: 1500€
Vybrané: 300€, zostatok: 1200€
Pokus o výber 2000€: Nedostatok prostriedkov! Chýba: 800€
Zostatok: 1200€
Operácie dokončené.
```

---

## Cvičenie 4: LINQ - Základy

### Zadanie
Máš pole čísel: `{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }`

Použitím LINQ:
1. Nájdi všetky párne čísla
2. Nájdi čísla väčšie ako 10
3. Vypočítaj súčet všetkých nepárnych čísel
4. Nájdi priemer párnych čísel
5. Vyber prvé 3 čísla väčšie ako 8
6. Zisti, či existuje číslo väčšie ako 20
7. Zisti, či sú všetky čísla kladné
8. Transformuj čísla na ich druhé mocniny (iba pre párne)
9. Vytvor reťazec zo všetkých čísel oddelených čiarkou

### Očakávaný výstup
```
Párne čísla: 2, 4, 6, 8, 10, 12, 14
Čísla > 10: 11, 12, 13, 14, 15
Súčet nepárnych: 64
Priemer párnych: 8
Prvé 3 > 8: 9, 10, 11
Existuje > 20? False
Všetky kladné? True
Druhé mocniny párnych: 4, 16, 36, 64, 100, 144, 196
Reťazec: 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15
```

---

## Cvičenie 5: LINQ - Pokročilé

### Zadanie
Máš nasledujúce dáta:

```csharp
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
```

Použitím LINQ:
1. Zoskup študentov podľa veku
2. Vypočítaj priemerný vek študentov
3. Spoj študentov so známkami (Join)
4. Nájdi študenta/študentov s najlepším priemerom
5. Vypíš všetkých študentov, ktorí majú aspoň jednu jednotku
6. Vypíš počet známok pre každého študenta
7. Nájdi predmet s najhorším priemerom

### Očakávaný výstup
```
Študenti podľa veku:
  Vek 20: Peter, Michal
  Vek 21: Eva
  Vek 22: Jana

Priemerný vek: 20.75

Študenti a známky:
  Peter - Matematika: 1
  Peter - Fyzika: 2
  Jana - Matematika: 1
  ...

Najlepší priemer má: Jana (1.0)

Študenti s jednotkou: Peter, Jana

Počet známok:
  Peter: 2
  Jana: 2
  Michal: 1
  Eva: 1

Najhorší predmet: Fyzika (priemer: 1.5)
```

---

## Cvičenie 6: Komplexný Projekt - Správa Úloh (TODO List)

### Zadanie
Vytvor komplexnú aplikáciu pre správu úloh s nasledujúcimi požiadavkami:

**Triedy:**
- `Uloha` s properties: `Id`, `Nazov`, `Popis`, `Priorita` (enum), `Dokoncena` (bool), `DatumVytvorenia`
- `SpravcaUloh` s metódami:
  - `PridajUlohu(Uloha)`
  - `OznacAkoHotovu(int id)`
  - `VymazUlohu(int id)`
  - `ZiskajVsetkyUlohy()`
  - `ZiskajNedokonceneUlohy()`
  - `ZiskajUlohyPodlaPriority(Priorita)`

**Enum:**
- `Priorita` { Nizka, Stredna, Vysoka, Kriticka }

**LINQ operácie:**
- Filtrovanie úloh podľa stavu
- Triedenie podľa priority
- Zoskupenie podľa priority
- Štatistiky (počet, percentá dokončených)

**Exception handling:**
- Vlastná výnimka `UlohaNenajdenaException`
- Validácia vstupov

**Príklad použitia:**
```csharp
var spravca = new SpravcaUloh();
spravca.PridajUlohu(new Uloha("Nakúpiť", "Mlieko, chlieb", Priorita.Vysoka));
spravca.PridajUlohu(new Uloha("Upratať", "Obývačka", Priorita.Stredna));
spravca.VypisUlohy();
spravca.VypisStatistiky();
```

---

## 💡 Tipy pre riešenie

1. **Začni jednoducho** - najprv vytvor základnú štruktúru tried
2. **Testuj priebežne** - po každej metóde vyskúšaj, či funguje
3. **Použi debugger** - ak niečo nefunguje, použi breakpointy
4. **Čítaj chybové hlášky** - často ti povedia presne, čo je zle
5. **Experimentuj** - skúšaj rôzne variácie kódu

---

## ✅ Vzorové riešenia

Vzorové riešenia nájdeš v súbore `Program.cs` v sekcii komentárov na konci.
Ale POZOR - najprv sa pokús vyriešiť cvičenia sám! 💪

---

## 🎯 Bonusové výzvy

Keď dokončíš základné cvičenia, skús:

1. **Knižnica:** Rozšír cvičenie 1 o možnosť požičiavať viac kníh naraz
2. **Kalkulačka:** Pridaj ďalšie operácie (mocnina, odmocnina, logaritmus)
3. **TODO List:** Pridaj možnosť uložiť úlohy do súboru a načítať ich
4. **LINQ:** Vytvor vlastný extension metódy pre kolekcie
5. **Vlastný projekt:** Navrhni a implementuj vlastnú aplikáciu!

Hodně štěstí! 🚀
