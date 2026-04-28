using System;
class Vehicle {
    public string brand;
    public double rate;
    public void Show()    {
        Console.WriteLine("Brand: " + brand);
        Console.WriteLine("Rate: " + rate);
    }
}

class Car : Vehicle {
    public int doors;
}
class Bike : Vehicle {
    public bool carrier;
}
class Program {
    static void Main()
    {
        Car c = new Car();
        c.brand = "Mahindra Scorpio";
        c.rate = 1000;
        c.doors = 4;
        c.Show();

        Bike b = new Bike();
        b.brand = "Hunter 350";
        b.rate = 500;
        b.carrier = true;
        b.Show();
    }
}