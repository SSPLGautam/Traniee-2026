// using System;

// class Bird
// {
//     public virtual void Fly()
//     {
//         Console.WriteLine("Bird is flying");
//     }
// }

// class Sparrow : Bird
// {
//     public override void Fly()
//     {
//         Console.WriteLine("Sparrow is flying");
//     }
// }

// class Penguin : Bird
// {   
//     public override void Fly()
//     {
//         Console.WriteLine("Penguin can't fly"); 
//     }
// }

// class Program
// {
//     static void Main(string[] args)
//     {
//         Bird b1 = new Sparrow();
//         b1.Fly();

//         Bird b2 = new Penguin();
//         b2.Fly();
//     }
// }

using System;

class Bird
{
    
}
interface IFly
{
    void Fly();
}


class Sparrow : Bird, IFly
{
    public void Fly()
    {
        Console.WriteLine("Sparrow can fly");
    }
}

class Penguin : Bird
{
    public void Swim()
    {
        Console.WriteLine("Penguin can swim");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Sparrow s = new Sparrow();
        s.Fly();

        Penguin p = new Penguin();
        p.Swim();
    }
}
