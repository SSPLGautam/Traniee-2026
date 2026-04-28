using System;
class Program {
    static void Main()   {
        Console.Write("Enter weight (kg): ");
        double weight = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter height (meters): ");
        double height = Convert.ToDouble(Console.ReadLine());

        double bmi = weight / (height * height);

        Console.WriteLine("BMI: " + bmi);

        if (bmi < 18.5)
            Console.WriteLine("Underweight - Eat more healthy food");

        else if (bmi < 25)
            Console.WriteLine("Normal - Keep it up");

        else if (bmi < 30)
            Console.WriteLine("Overweight - Exercise regularly");

        else
            Console.WriteLine("Weak - Consult doctor");
        
        Console.ReadLine();
    }
}