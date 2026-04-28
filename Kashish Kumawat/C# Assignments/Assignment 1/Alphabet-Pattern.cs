using System;
class Program {
    static void Main() {
        int n = 5;

        for (int i = 1; i <= n; i++)
        {
            char ch = 'A';

            for (int j = 1; j <= i; j++)
            {
                Console.Write(ch + " ");
                ch++;
            }

            Console.WriteLine();
        }
    }
}