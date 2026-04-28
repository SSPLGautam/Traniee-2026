using System;
class Program{
    static void Main()     {
        int[] arr = new int[10];
        int sum = 0;

        Console.WriteLine("Enter 10 numbers:");

        for (int i = 0; i < 10; i++) {
            arr[i] = Convert.ToInt32(Console.ReadLine());
            sum += arr[i];
        }

        double avg = sum / 10.0;

        int max = arr[0], min = arr[0];

        for (int i = 1; i < 10; i++) {
            if (arr[i] > max) max = arr[i];
            if (arr[i] < min) min = arr[i];
        }

        Console.WriteLine("Sum: " + sum);
        Console.WriteLine("Average: " + avg);
        Console.WriteLine("Max: " + max);
        Console.WriteLine("Min: " + min);

        Console.WriteLine("Reverse order:");
        for (int i = 9; i >= 0; i--) {
            Console.Write(arr[i] + " ");
        }

        Console.ReadLine();
    }
}