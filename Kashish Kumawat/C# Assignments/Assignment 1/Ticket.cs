using System;
class Program {
    static void Main()     {
        int total = 0;

        Console.Write("Enter number of family members: ");
        int n = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i <= n; i++)
        {
            Console.Write("Enter age of person " + i + ": ");
            int age = Convert.ToInt32(Console.ReadLine());

            if (age < 5)
                total += 0;

            else if (age <= 12)
                total += 10;

            else if (age <= 60)
                total += 25;

            else
                total += 15;
        }

        Console.WriteLine("Total Ticket Price: $" + total);
        Console.ReadLine();
    }
}