using System;

class Shape
{
    public virtual void Area() { }
}

class Circle : Shape
{
    public override void Area()
    {
        Console.Write("Enter radius: ");
        double r = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Area of Circle: " + (3.14 * r * r));
    }
}

class Rectangle : Shape
{
    public override void Area()
    {
        Console.Write("Enter length: ");
        double l = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter width: ");
        double w = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Area of Rectangle: " + (l * w));
    }
}

class Triangle : Shape
{
    public override void Area()
    {
        Console.Write("Enter base: ");
        double b = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter height: ");
        double h = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Area of Triangle: " + (0.5 * b * h));
    }
}

class Program
{
    static void Main()
    {
        Shape s;

        Console.WriteLine("1.Circle  2.Rectangle  3.Triangle");
        Console.Write("Enter choice: ");
        int ch = Convert.ToInt32(Console.ReadLine());

        if (ch == 1)
            s = new Circle();

        else if (ch == 2)
            s = new Rectangle();

        else
            s = new Triangle();

        s.Area();
    }
}