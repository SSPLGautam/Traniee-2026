using System;

class Student
{
    public int roll;
    public string name;
    public int marks;
}

class Program
{
    static void Main()
    {
        Student[] s = new Student[5];
        int count = 0;

        while (true)
        {
            Console.WriteLine("\n1.Add 2.Display 3.Average 4.Search 5.Exit");
            int ch = Convert.ToInt32(Console.ReadLine());

            if (ch == 1)
            {
                s[count] = new Student();
                Console.Write("Roll: ");
                s[count].roll = Convert.ToInt32(Console.ReadLine());
                Console.Write("Name: ");
                s[count].name = Console.ReadLine();
                Console.Write("Marks: ");
                s[count].marks = Convert.ToInt32(Console.ReadLine());
                count++;
            }
            else if (ch == 2)
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine(s[i].roll + " " + s[i].name + " " + s[i].marks);
                }
            }
            else if (ch == 3)
            {
                int sum = 0;
                for (int i = 0; i < count; i++)
                    sum += s[i].marks;

                Console.WriteLine("Average: " + (sum / count));
            }
            else if (ch == 4)
            {
                Console.Write("Enter roll: ");
                int r = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < count; i++)
                {
                    if (s[i].roll == r)
                        Console.WriteLine(s[i].name + " " + s[i].marks);
                }
            }
            else break;
        }
    }
}