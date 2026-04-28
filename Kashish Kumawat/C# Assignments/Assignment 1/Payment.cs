using System;

interface IPayment
{
    void Pay();
}

class CreditCard : IPayment
{
    public void Pay()
    {
        Console.WriteLine("Paid using Credit Card");
    }
}

class UPI : IPayment
{
    public void Pay()
    {
        Console.WriteLine("Paid using UPI");
    }
}

class Program
{
    static void Main()
    {
        IPayment p;

        Console.WriteLine("1.CreditCard 2.UPI");
        int ch = Convert.ToInt32(Console.ReadLine());

        if (ch == 1)
            p = new CreditCard();
        else
            p = new UPI();

        p.Pay();
    }
}