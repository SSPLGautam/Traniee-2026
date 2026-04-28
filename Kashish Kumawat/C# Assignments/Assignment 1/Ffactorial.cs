using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        int n = Convert.ToInt32(Console.ReadLine());

        int fact = 1;

        for (int i = 1; i <= n; i++)
        {
            fact = fact * i;
        }

        Console.WriteLine("Factorial: " + fact);
    }
}