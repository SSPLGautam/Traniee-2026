using System;

class Program
{
    static void Main()
    {
        int n = 10;
        int a = 0, b = 1, c;

        for (int i = 1; i <= n; i++)
        {
            Console.Write(a + " ");

            c = a + b;
            a = b;
            b = c;
        }
    }
}