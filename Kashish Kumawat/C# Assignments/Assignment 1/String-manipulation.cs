using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a sentence: ");
        string str = Console.ReadLine();

       
        string[] words = str.Split(' ');
        Console.WriteLine("Word Count: " + words.Length);

        Console.WriteLine("Characters (with spaces): " + str.Length);
        Console.WriteLine("Characters (without spaces): " + str.Replace(" ", "").Length);

        string longest = "";
        foreach (string w in words)
        {
            if (w.Length > longest.Length)
                longest = w;
        }
        Console.WriteLine("Longest word: " + longest);

      
        char[] arr = str.ToCharArray();
        Array.Reverse(arr);
        Console.WriteLine("Reversed: " + new string(arr));

        Console.ReadLine();
    }
}