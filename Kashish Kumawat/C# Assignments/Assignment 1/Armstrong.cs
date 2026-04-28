using System;
class Program {
    static void Main()     {
        Console.Write("Enter a number: ");
        int num = Convert.ToInt32(Console.ReadLine());

        int original = num;
        int sum = 0;

        while (num > 0)
        {
            int digit = num % 10;
            sum += digit * digit * digit;   // cube
            num = num / 10;
        }

        if (sum == original)
            Console.WriteLine("Armstrong Number");
        else
            Console.WriteLine("Not Armstrong Number");
    }
}