# 🚀 Ako spustiť projekty

## Rýchly štart

### 1. Otvor terminál v priečinku projektu

V VS Code:
- Stlač `Ctrl + `` (backtick) pre otvorenie terminálu
- Alebo: Menu → Terminal → New Terminal

### 2. Prejdi do priečinka projektu

```powershell
# Pre príklady tried:
cd CSharpLearning\01_Classes

# Pre static:
cd CSharpLearning\02_Static

# Pre exceptions:
cd CSharpLearning\03_Exceptions

# Pre LINQ:
cd CSharpLearning\04_LINQ

# Pre cvičenia:
cd CSharpLearning\05_Exercises
```

### 3. Skomiluj a spusti

```powershell
dotnet run
```

To je všetko! 🎉

---

## Podrobný návod

### Kompilácia bez spustenia

```powershell
dotnet build
```

### Spustenie už skompilovaného programu

```powershell
# Pre 01_Classes:
.\bin\Debug\net8.0\01_Classes.exe

# Pre 02_Static:
.\bin\Debug\net8.0\02_Static.exe

# Atď...
```

### Vyčistenie build súborov

```powershell
dotnet clean
```

---

## Odporúčané poradie učenia

1. **Najprv čítaj teóriu** v `README.md`
2. **Spusti príklad** - napr. `01_Classes`
3. **Preskúmaj kód** v `Program.cs`
4. **Experimentuj** - zmeň kód a pozri, co sa stane
5. **Skús cvičenia** v `05_Exercises`

---

## Tipy

### Pozri výstup krok po kroku
Program čaká na stlačenie klávesy na konci - máš čas prečítať výstup.

### Debuggovanie
- Vlož breakpoint: klikni vľavo od čísla riadku (červený bod)
- Stlač `F5` pre spustenie v debug móde
- Použi `F10` (Step Over) a `F11` (Step Into)

### Upravovanie kódu
- Skús zmeniť hodnoty
- Pridaj nové premenné
- Vytvor vlastné metódy
- Nezabudni uložiť (`Ctrl+S`) pred spustením!

---

## Riešenie problémov

### "dotnet command not found"
.NET SDK nie je nainštalované alebo nie je v PATH.
Riešenie: Nainštaluj .NET SDK z https://dotnet.microsoft.com/download

### Chyby pri kompilácii
- Prečítaj si chybovú hlášku - často povie presne, čo je zle
- Skontroluj syntax
- Skontroluj, či si uložil súbor

### Program spadne
- Pozri výnimku (error message)
- Použi try-catch na debuggovanie
- Použi Console.WriteLine na sledovanie priebehu

---

Enjoy learning! 🎓
