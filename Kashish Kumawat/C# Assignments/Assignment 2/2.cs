// using System;

// class Payment
// {
//     public void Pay()
//     {
//         if (method == "card")
//         {
//             Console.WriteLine("Paid using Credit Card");
//         }
//         else if (method == "paypal")
//         {
//             Console.WriteLine("Paid using PayPal");
//         }
//         else if(MethodAccessException == "Cryptocurrency")
//         {
//             Console.WriteLine("Paid by Cryptocurrency");
//         }
//     }
// }

// class Program
// {
//     static void Main(string[] args)
//     {
//         Payment p = new Payment();
//         p.Pay("card");
//         p.Pay("paypal");
//         p.Pay("Cryptocurrency");
//     }
// }

using System;
interface IPayment
{
    void Pay();
}

class CardPayment : IPayment
{
    public void Pay()
    {
        Console.WriteLine("Paid using Credit Card");
    }
}

class PaypalPayment : IPayment
{
    public void Pay()
    {
        Console.WriteLine("Paid using PayPal");
    }
}

class CryptoPayment : IPayment
{
    public void Pay()
    {
        Console.WriteLine("Paid using CryptoCurrency");
    }
}
class Program
{
    static void Main(string[] args)
    {
        IPayment p1 = new CardPayment();
        p1.Pay();

        IPayment p2 = new PaypalPayment();
        p2.Pay();

        IPayment p3 = new CryptoPayment();
        p3.Pay();
    }
}
