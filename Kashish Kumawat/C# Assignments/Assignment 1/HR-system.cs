using System;

class Employee
{
    private double salary;
    public string name;

    public void AddEmployee()
    {
        Console.Write("Enter Name: ");
        name = Console.ReadLine();

        Console.Write("Enter Salary: ");
        double s = Convert.ToDouble(Console.ReadLine());

        if (s > 0)
            salary = s;
        else
            Console.WriteLine("Invalid salary");
    }

    public void GiveRaise()
    {
        Console.Write("Enter raise %: ");
        double percent = Convert.ToDouble(Console.ReadLine());

        if (percent > 0)
            salary += salary * percent / 100;
        else
            Console.WriteLine("Invalid percent");
    }

    public void Display()
    {
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Salary: " + salary);
    }

    public void AnnualSalary()
    {
        Console.WriteLine("Annual Salary: " + (salary * 12));
    }
}

class Program
{
    static void Main()
    {
        Employee e = new Employee();

        while (true)
        {
            Console.WriteLine("\n1.Add Employee");
            Console.WriteLine("2.Give Raise");
            Console.WriteLine("3.Display Details");
            Console.WriteLine("4.Annual Salary");
            Console.WriteLine("5.Exit");

            int ch = Convert.ToInt32(Console.ReadLine());

            if (ch == 1)
                e.AddEmployee();

            else if (ch == 2)
                e.GiveRaise();

            else if (ch == 3)
                e.Display();

            else if (ch == 4)
                e.AnnualSalary();

            else
                break;
        }
    }
}