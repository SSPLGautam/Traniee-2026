// using System;

// class User
// {
//     public string Name;
//     public string Email;

//     public void Saveindatabase()
//     {
//         Console.WriteLine("User saved to database");
//     }

//     public void SendEmail()
//     {
//         Console.WriteLine("Email sent to user");
//     }
// }

// class Program
// {
//     static void Main(string[] args)
//     {
//         User user = new User();
//         user.Name = "Kashish";
//         user.Email = "kashish@766gmail.com";

//         user.SaveToDatabase();
//         user.SendEmail();
//     }
// }

using System;

class User
{
    public string Name;
    public string Email;
}

class UserDB
{
    public void SaveUser(User u)
    {
        Console.WriteLine("User data saved in database");
    }
}
class EmailSender
{
    public void Send(User u)
    {
        Console.WriteLine("Email sent to " + u.Name);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // user object banaya
        User u = new User();
        u.Name = "Kashish";
        u.Email = "kashish@gmail.com";

        UserDB db = new UserDB();
        db.SaveUser(u);

        EmailSender email = new EmailSender();
        email.Send(u);
    }
}