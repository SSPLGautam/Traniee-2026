using System;
class Animal{
    public virtual void Sound()  {
        Console.WriteLine("Animal sound");
    }
}

class Dog : Animal {
    public override void Sound()  {
        Console.WriteLine("Dog says Woof");
    }
}

class Cat : Animal {
    public override void Sound() {
        Console.WriteLine("Cat says Meow");
    }
}

class Program {
    static void Main()   {
        Animal a;

        Console.WriteLine("1.Dog 2.Cat");
        int ch = Convert.ToInt32(Console.ReadLine());

        if (ch == 1)
            a = new Dog();
        else
            a = new Cat();

        a.Sound();
    }
}