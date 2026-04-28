using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter First Name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine();

        Console.WriteLine("\nFull Name: " + firstName + " " + lastName);
    }
}