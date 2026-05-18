// using System;
// using System.Threading;
// class Program
// {
//     static void Main()
//     {
//         Console.WriteLine("Start");

//         Console.WriteLine("Downloading...");
//         Thread.Sleep(3000); 
//         Console.WriteLine("Download Complete");

//         Console.WriteLine("End");
//     }
// }

using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Start");

        await Download();

        Console.WriteLine("End");
    }

    static async Task Download()
    {
        Console.WriteLine("Downloading...");

        await Task.Delay(3000);

        Console.WriteLine("Download Complete");
    }
}