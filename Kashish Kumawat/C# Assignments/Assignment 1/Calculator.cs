using System; 
class Program {
    static void Main() {
        while (true) {
            Console.WriteLine("\n1.Add 2.Sub 3.Mul 4.Div 5.Exit");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 5)
                break;

            Console.Write("Enter first number: ");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter second number: ");
            int b = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1: 
                    Console.WriteLine("Result: " + (a + b)); 
                    break;
                case 2: 
                    Console.WriteLine("Result: " + (a - b)); 
                    break;
                case 3: 
                    Console.WriteLine("Result: " + (a * b)); 
                    break;
                case 4: 
                    Console.WriteLine("Result: " + (a / b)); 
                    break;
                default: 
                    Console.WriteLine("Invalid choice"); 
                    break;
            }
        }
    }
}