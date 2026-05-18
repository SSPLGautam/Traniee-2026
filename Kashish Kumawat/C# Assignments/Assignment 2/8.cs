using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        try
        {
            await Test();
        }
        catch
        {
            Console.WriteLine("Error");
        }
    }

    static async Task Test()
    {
        await Task.Delay(1000);
        throw new Exception();
    }
}