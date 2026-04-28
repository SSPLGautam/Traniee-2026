using System;

class BankAccount
{
    private double balance;
    private string name;
    private int accNo;

    public void CreateAccount()
    {
        Console.Write("Enter Account Number: ");
        accNo = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Name: ");
        name = Console.ReadLine();

        balance = 0;
        Console.WriteLine("Account Created Successfully");
    }

    public void Deposit()
    {
        Console.Write("Enter amount to deposit: ");
        double amt = Convert.ToDouble(Console.ReadLine());

        if (amt > 0)
        {
            balance += amt;
            Console.WriteLine("Amount Deposited");
        }
        else
        {
            Console.WriteLine("Invalid amount");
        }
    }

    public void Withdraw()
    {
        Console.Write("Enter amount to withdraw: ");
        double amt = Convert.ToDouble(Console.ReadLine());

        if (amt <= balance)
        {
            balance -= amt;
            Console.WriteLine("Withdrawal Successful");
        }
        else
        {
            Console.WriteLine("Insufficient Balance");
        }
    }

    public void ShowDetails()
    {
        Console.WriteLine("\n--- Account Details ---");
        Console.WriteLine("Account No: " + accNo);
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Balance: " + balance);
    }
}

class Program {
    static void Main()     {
        BankAccount b = new BankAccount();

        while (true)         {
            Console.WriteLine("\n1.Create Account");
            Console.WriteLine("2.Deposit");
            Console.WriteLine("3.Withdraw");
            Console.WriteLine("4.Show Details");
            Console.WriteLine("5.Exit");

            Console.Write("Enter choice: ");
            int ch = Convert.ToInt32(Console.ReadLine());

            if (ch == 1)
                b.CreateAccount();

            else if (ch == 2)
                b.Deposit();

            else if (ch == 3)
                b.Withdraw();

            else if (ch == 4)
                b.ShowDetails();

            else if (ch == 5)
                break;

            else
                Console.WriteLine("Invalid Choice");
        }
    }
}