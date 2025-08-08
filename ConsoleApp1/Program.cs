//moj prvy program
using System;

class Program {

    static void Main() {

        //spytame sa uzivatela
        Console.WriteLine("Napis cislo:");
        //ukladame vstup do premennej input
        string input = Console.ReadLine();
        int number;

        //skusime konvertovat vstup na cislo
        //TryParse vracia true alebo false, premena vstupu na cele cislo
        bool success = int.TryParse(input, out number);

        if (success == false) {
            Console.WriteLine("Zadal si nespravnu hodnotu.");
            return;
        }

        if (number % 2 == 0)
        {
            Console.WriteLine($"Cislo {number} je parne.");
        }
        else {
            Console.WriteLine($"Cislo {number} je neparne.");
        }

        Console.WriteLine($"Vypisem vsetky parne cisla od 0 po {number}");

        for (int i = 0; i < number; i += 2) {
            Console.Write(i + " ");
        }

        //zalomenie na konci
        Console.WriteLine();
    }
}