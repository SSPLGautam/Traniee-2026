using System;
class Program { 
    static void Main()  {
        Console.Write("Choose option: ");
        Console.WriteLine("1. Celsius to Fahrenheit");
        Console.WriteLine("2. Fahrenheit to Celsius");
        
        int choice = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter temperature: ");
        double temp = Convert.ToDouble(Console.ReadLine());

        if (choice == 1) {
            double f = (temp * 9 / 5) + 32;
            Console.WriteLine("Fahrenheit: " + f);
        }
        else if (choice == 2) {
            double c = (temp - 32) * 5 / 9;
            Console.WriteLine("Celsius: " + c);
        }
        else {
            Console.WriteLine("Invalid choice");
        }
    }
}