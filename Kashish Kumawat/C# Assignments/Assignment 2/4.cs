// using System;

// interface IWorker
// {
//     void Work();
//     void Eat();
//     void Sleep();
// }

// class Human : IWorker
// {
//     public void Work()
//     {
//         Console.WriteLine("Human working");
//     }

//     public void Eat()
//     {
//         Console.WriteLine("Human eating");
//     }

//     public void Sleep()
//     {
//         Console.WriteLine("Human sleeping");
//     }
// }

// class Robot : IWorker
// {
//     public void Work()
//     {
//         Console.WriteLine("Robot working");
//     }

//     public void Eat()
//     {
//         Console.WriteLine("Robot can't eat"); 
//     }

//     public void Sleep()
//     {
//         Console.WriteLine("Robot doesn't sleep");
//     }
// }

using System;
interface IWork
{
    void Work();
}

interface IEat
{
    void Eat();
}

interface ISleep
{
    void Sleep();
}

class Human : IWork, IEat, ISleep
{
    public void Work()
    {
        Console.WriteLine("Human can work");
    }

    public void Eat()
    {
        Console.WriteLine("Human can eat");
    }

    public void Sleep()
    {
        Console.WriteLine("Human can sleep");
    }
}

class Robot : IWork
{
    public void Work()
    {
        Console.WriteLine("Robot can only do work. It can't eat or sleep");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Human h = new Human();
        h.Work();
        h.Eat();
        h.Sleep();

        Robot r = new Robot();
        r.Work();
    }
}