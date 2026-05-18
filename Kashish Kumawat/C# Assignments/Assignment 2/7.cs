using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        await Task.WhenAll(
            Work("A"), Work("B")
        );

        Console.WriteLine("Done");
    }

    static async Task Work(string name)
    {
        Console.WriteLine(name + " start");
        await Task.Delay(2000);
        Console.WriteLine(name + " end");
    }
}