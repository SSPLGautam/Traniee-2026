using System;

interface INotification
{
    void Send();
}

class Email : INotification
{
    public void Send()
    {
        Console.WriteLine("Email sent");
    }
}

class SMS : INotification
{
    public void Send()
    {
        Console.WriteLine("SMS sent");
    }
}

class Program
{
    static void Main()
    {
        INotification n;

        Console.WriteLine("1.Email 2.SMS");
        int ch = Convert.ToInt32(Console.ReadLine());

        if (ch == 1)
            n = new Email();
        else
            n = new SMS();

        n.Send();
    }
}