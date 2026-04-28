using System;

class LibraryItem
{
    public int id;
    public string title;

    public virtual void Input()
    {
        Console.Write("Enter ID: ");
        id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Title: ");
        title = Console.ReadLine();
    }

    public virtual void Display()
    {
        Console.WriteLine("ID: " + id);
        Console.WriteLine("Title: " + title);
    }
}

class Book : LibraryItem
{
    public string author;

    public override void Input()
    {
        base.Input();
        Console.Write("Enter Author: ");
        author = Console.ReadLine();
    }

    public override void Display()
    {
        base.Display();
        Console.WriteLine("Author: " + author);
    }
}

class DVD : LibraryItem
{
    public int duration;

    public override void Input()
    {
        base.Input();
        Console.Write("Enter Duration (min): ");
        duration = Convert.ToInt32(Console.ReadLine());
    }

    public override void Display()
    {
        base.Display();
        Console.WriteLine("Duration: " + duration + " min");
    }
}

class Program
{
    static void Main()
    {
        LibraryItem[] items = new LibraryItem[10];
        int count = 0;

        while (true)
        {
            Console.WriteLine("\n1.Add Book");
            Console.WriteLine("2.Add DVD");
            Console.WriteLine("3.Display All");
            Console.WriteLine("4.Search by ID");
            Console.WriteLine("5.Exit");

            Console.Write("Enter choice: ");
            int ch = Convert.ToInt32(Console.ReadLine());

            if (ch == 1)
            {
                if (count < 10)
                {
                    items[count] = new Book();
                    items[count].Input();
                    count++;
                }
                else
                {
                    Console.WriteLine("Library Full!");
                }
            }

            else if (ch == 2)
            {
                if (count < 10)
                {
                    items[count] = new DVD();
                    items[count].Input();
                    count++;
                }
                else
                {
                    Console.WriteLine("Library Full!");
                }
            }

            else if (ch == 3)
            {
                if (count == 0)
                {
                    Console.WriteLine("No items available!");
                }
                else
                {
                    Console.WriteLine("\n--- Library Items ---");
                    for (int i = 0; i < count; i++)
                    {
                        items[i].Display();
                        Console.WriteLine();
                    }
                }
            }

            else if (ch == 4)
            {
                Console.Write("Enter ID to search: ");
                int sid = Convert.ToInt32(Console.ReadLine());

                bool found = false;

                for (int i = 0; i < count; i++)
                {
                    if (items[i].id == sid)
                    {
                        items[i].Display();
                        found = true;
                        break;
                    }
                }

                if (!found)
                    Console.WriteLine("Item not found!");
            }

            else if (ch == 5)
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