// using System;

// class EmailNotification
// {
//     public void Send()
//     {
//         Console.WriteLine("Email sent");
//     }
// }

// class NotificationService
// {
//     EmailNotification email = new EmailNotification();

//     public void Notify()
//     {
//         email.Send();
//     }
// }

// class Program
// {
//     static void Main(string[] args)
//     {
//         NotificationService service = new NotificationService();
//         service.Notify();
//     }
// }

using System;

interface INotification
{
    void Send();
}

class EmailNotification : INotification
{
    public void Send()
    {
        Console.WriteLine("Email sent");
    }
}

class SMSNotification : INotification
{
    public void Send()
    {
        Console.WriteLine("SMS sent");
    }
}
class SlackNotification : INotification
{
    public void Send()
    {
        Console.WriteLine("Slack sent");
    }
}
class NotificationService
{
    INotification notification;

    public NotificationService(INotification n)
    {
        notification = n;
    }

    public void Notify()
    {
        notification.Send();
    }
}
class Program
{
    static void Main(string[] args)
    {
        NotificationService s1 = new NotificationService(new EmailNotification());
        s1.Notify();

        NotificationService s2 = new NotificationService(new SMSNotification());
        s2.Notify();

        NotificationService s3 = new NotificationService(new SlackNotification());
        s3.Notify();
    }
}