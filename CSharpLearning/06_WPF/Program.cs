using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WPFExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# WPF - MVVM PATTERN ===\n");

            // ===== PRÍKLAD 1: INotifyPropertyChanged =====
            Console.WriteLine("--- PRÍKLAD 1: INotifyPropertyChanged ---");
            Priklad1_INotifyPropertyChanged();
            Console.WriteLine();

            // ===== PRÍKLAD 2: MVVM Model =====
            Console.WriteLine("--- PRÍKLAD 2: MVVM Model ---");
            Priklad2_MVVMModel();
            Console.WriteLine();

            // ===== PRÍKLAD 3: DelegateCommand =====
            Console.WriteLine("--- PRÍKLAD 3: DelegateCommand ---");
            Priklad3_DelegateCommand();
            Console.WriteLine();

            // ===== PRÍKLAD 4: Kompletný MVVM Príklad =====
            Console.WriteLine("--- PRÍKLAD 4: Kompletný MVVM Príklad ---");
            Priklad4_KompletnyMVVM();
            Console.WriteLine();

            Console.WriteLine("\nStlač ENTER pre ukončenie...");
            Console.ReadLine();
        }

        // ===== PRÍKLAD 1: INotifyPropertyChanged =====
        static void Priklad1_INotifyPropertyChanged()
        {
            Console.WriteLine("INotifyPropertyChanged umožňuje notifikáciu zmien properties pre databinding.\n");

            Osoba osoba = new Osoba
            {
                Meno = "Peter",
                Vek = 25
            };

            // Prihlásenie sa na udalosť PropertyChanged
            osoba.PropertyChanged += (sender, e) =>
            {
                Console.WriteLine($"Property '{e.PropertyName}' sa zmenila!");
            };

            Console.WriteLine($"Pôvodné meno: {osoba.Meno}, vek: {osoba.Vek}");
            
            osoba.Meno = "Jozef";  // Vyvolá PropertyChanged
            osoba.Vek = 30;        // Vyvolá PropertyChanged

            Console.WriteLine($"Nové meno: {osoba.Meno}, vek: {osoba.Vek}");
        }

        // ===== PRÍKLAD 2: MVVM Model =====
        static void Priklad2_MVVMModel()
        {
            Console.WriteLine("MVVM (Model-View-ViewModel) oddeľuje UI logiku od business logiky.\n");

            ProduktModel produkt = new ProduktModel
            {
                Nazov = "Laptop",
                Cena = 999.99m,
                NaSkladе = 10
            };

            Console.WriteLine($"Model: {produkt.Nazov}");
            Console.WriteLine($"Cena: {produkt.Cena}€");
            Console.WriteLine($"Na sklade: {produkt.NaSkladе} ks");
            Console.WriteLine($"Je dostupný: {(produkt.JeDostupny ? "Áno" : "Nie")}");
        }

        // ===== PRÍKLAD 3: DelegateCommand =====
        static void Priklad3_DelegateCommand()
        {
            Console.WriteLine("DelegateCommand implementuje ICommand pre binding príkazov v MVVM.\n");

            int pocitadlo = 0;

            var zvysCommand = new DelegateCommand(
                execute: () =>
                {
                    pocitadlo++;
                    Console.WriteLine($"Počítadlo zvýšené na: {pocitadlo}");
                },
                canExecute: () => pocitadlo < 10
            );

            Console.WriteLine($"Začíname s počítadlom: {pocitadlo}");
            
            while (zvysCommand.CanExecute(null))
            {
                zvysCommand.Execute(null);
            }

            Console.WriteLine($"Príkaz už nemožno vykonať (počítadlo >= 10)");
        }

        // ===== PRÍKLAD 4: Kompletný MVVM Príklad =====
        static void Priklad4_KompletnyMVVM()
        {
            Console.WriteLine("Kompletný MVVM príklad s Model, ViewModel a Command.\n");

            UzivatelViewModel viewModel = new UzivatelViewModel();

            // Sledovanie zmien
            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Pozdrav")
                {
                    Console.WriteLine($"  → Pozdrav aktualizovaný: {viewModel.Pozdrav}");
                }
            };

            Console.WriteLine("Zadávam meno 'Martin':");
            viewModel.Meno = "Martin";

            Console.WriteLine("\nVykonávam PozdravCommand:");
            if (viewModel.PozdravCommand.CanExecute(null))
            {
                viewModel.PozdravCommand.Execute(null);
            }

            Console.WriteLine($"\nVýsledok: {viewModel.Pozdrav}");
        }
    }

    // ===== INotifyPropertyChanged Implementácia =====
    public class Osoba : INotifyPropertyChanged
    {
        private string meno;
        private int vek;

        public string Meno
        {
            get => meno;
            set
            {
                if (meno != value)
                {
                    meno = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Vek
        {
            get => vek;
            set
            {
                if (vek != value)
                {
                    vek = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // ===== Model =====
    public class ProduktModel
    {
        public string Nazov { get; set; }
        public decimal Cena { get; set; }
        public int NaSkladе { get; set; }
        public bool JeDostupny => NaSkladе > 0;
    }

    // ===== DelegateCommand Implementácia =====
    public class DelegateCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public DelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        public void Execute(object parameter)
        {
            execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    // ===== ViewModel s generickým DelegateCommand =====
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            execute((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    // ===== BaseViewModel s INotifyPropertyChanged =====
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    // ===== Kompletný ViewModel Príklad =====
    public class UzivatelViewModel : BaseViewModel
    {
        private string meno;
        private string pozdrav;

        public string Meno
        {
            get => meno;
            set
            {
                if (SetProperty(ref meno, value))
                {
                    // Aktualizuj stav príkazu
                    (PozdravCommand as DelegateCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public string Pozdrav
        {
            get => pozdrav;
            private set => SetProperty(ref pozdrav, value);
        }

        public ICommand PozdravCommand { get; }

        public UzivatelViewModel()
        {
            PozdravCommand = new DelegateCommand(
                execute: () =>
                {
                    Pozdrav = $"Ahoj, {Meno}! Vitaj v MVVM svete.";
                },
                canExecute: () => !string.IsNullOrWhiteSpace(Meno)
            );
        }
    }
}
