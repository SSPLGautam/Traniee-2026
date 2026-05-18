using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var cts = new CancellationTokenSource();
        Console.WriteLine("Press any key to stop...");
        var task = DoWork(cts.Token);
        Console.ReadKey();  
        cts.Cancel();        

        await task;
    }

    static async Task DoWork(CancellationToken token)
    {
        int i = 1;

        while (true)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("Stopped");
                break;
            }

            Console.WriteLine("Working " + i);
            i++;

            await Task.Delay(1000);
        }
    }
}