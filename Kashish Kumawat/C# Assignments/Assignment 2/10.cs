using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        await foreach (var x in GetData())
        {
            Console.WriteLine(x);
        }
    }

    static async IAsyncEnumerable<int> GetNumbers()
    {
        for (int i = 1; i <= 3; i++)
        {
            await Task.Delay(1000);
            yield return i;
        }
    }
}