using System;

class Program
{
    static void Main()
    {
        int n = 5;

        // Upper part
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= i; j++)
                Console.Write("*");

            for (int j = 1; j <= 2 * (n - i); j++)
                Console.Write(" ");

            for (int j = 1; j <= i; j++)
                Console.Write("*");

            Console.WriteLine();
        }

        // Lower part
        for (int i = n; i >= 1; i--)
        {
            for (int j = 1; j <= i; j++)
                Console.Write("*");

            for (int j = 1; j <= 2 * (n - i); j++)
                Console.Write(" ");

            for (int j = 1; j <= i; j++)
                Console.Write("*");

            Console.WriteLine();
        }
    }
}