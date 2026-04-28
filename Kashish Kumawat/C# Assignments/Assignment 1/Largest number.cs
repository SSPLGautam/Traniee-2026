using System;
class Program {
    static void Main() {
        Console.Write("Enter 3 numbers: ");
        int a = Convert.ToInt32(Console.ReadLine());
        int b = Convert.ToInt32(Console.ReadLine());
        int c = Convert.ToInt32(Console.ReadLine());

        int largest = a;

        if (b > largest) largest = b;
        if (c > largest) largest = c;

        Console.WriteLine("Largest number is: " + largest);
    }
}