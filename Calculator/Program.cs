using System;

//kalkutor
class Calculator {
    public double Add(double a, double b) => a + b;
    public double Subtract(double a, double b) => a - b;
    public double Multiply(double a, double b) => a * b;
    public double Divide(double a, double b) {
        if (b == 0) {
            throw new DivideByZeroException("Cannot divide by zero");
        }
        return a / b;
    }
}

class Program
{

    static void Main()
    {
        Calculator kalkulacka = new Calculator();
        double result = 0;
        bool first = true;

        while (true)
        {
            double a;
            if (first)
            {
                Console.WriteLine("Toto je vylepsena kalkulacka");
                Console.Write("Zadaj prve cislo : ");
                if (!double.TryParse(Console.ReadLine(), out a))
                {
                    Console.WriteLine("Neplatne cislo");
                    continue;
                }
                first = false;
            }
            else
            {
                a = result;
                Console.WriteLine($"Aktualny vysledok: {a}");
            }

            Console.WriteLine("Zadaj operator +, -, *, /");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Nezadal si znak");
                continue;
            }
            char op = input[0];

            Console.WriteLine("Zadaj druhe cislo : ");
            if (!double.TryParse(Console.ReadLine(), out double b))
            {
                Console.WriteLine("Neplatne cislo");
                continue;
            }

            try
            {
                switch (op)
                {
                    case '+':
                        result = kalkulacka.Add(a, b);
                        break;
                    case '-':
                        result = kalkulacka.Subtract(a, b);
                        break;
                    case '*':
                        result = kalkulacka.Multiply(a, b);
                        break;
                    case '/':
                        result = kalkulacka.Divide(a, b);
                        break;
                    default:
                        throw new InvalidOperationException("Neznama operacia");
                }
                Console.WriteLine($"Vysledok = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba: {ex.Message}");
            }

            Console.Write("Chces pokracovat ? (a/n): ");
            string vysledok = Console.ReadLine();
            if (vysledok.ToLower() != "a")
            {
                break;
            }
        }
        Console.WriteLine("Koniec programu.");
    }
}

//ahoj
