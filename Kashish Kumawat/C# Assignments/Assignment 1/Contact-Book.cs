using System;

class Program
{
    static void Main()
    {
        string[] names = new string[10];
        string[] phones = new string[10];
        int count = 0;

        while (true)
        {
            Console.WriteLine("\n1.Add  2.Search  3.Display  4.Exit");
            Console.Write("Enter choice: ");
            int ch = Convert.ToInt32(Console.ReadLine());

            if (ch == 1)
            {
                if (count < 10)
                {
                    Console.Write("Enter Name: ");
                    names[count] = Console.ReadLine();

                    Console.Write("Enter Phone: ");
                    phones[count] = Console.ReadLine();

                    count++;
                    Console.WriteLine("Contact Added!");
                }
                else
                {
                    Console.WriteLine("Contact list is full!");
                }
            }

            else if (ch == 2)
            {
                Console.Write("Enter name to search: ");
                string search = Console.ReadLine().ToLower();

                bool found = false;

                for (int i = 0; i < count; i++)
                {
                    if (names[i].ToLower() == search)
                    {
                        Console.WriteLine("Phone: " + phones[i]);
                        found = true;
                        break; // important
                    }
                }

                if (!found)
                    Console.WriteLine("Contact not found!");
            }

            else if (ch == 3)
            {
                if (count == 0)
                {
                    Console.WriteLine("No contacts available!");
                }
                else
                {
                    Console.WriteLine("\n--- Contact List ---");
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine(names[i] + " - " + phones[i]);
                    }
                }
            }

            else if (ch == 4)
            {
                break;
            }

            else
            {
                Console.WriteLine("Invalid choice!");
            }
        }
    }
}