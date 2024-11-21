using System.Diagnostics.Metrics;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter the first number: ");
        if (!int.TryParse(Console.ReadLine(), out int a))
        {
            Console.WriteLine("Not a number!");
            return;
        }

        Console.WriteLine("Enter the second number: ");
        if (!int.TryParse(Console.ReadLine(), out int b))
        {
            Console.WriteLine("Not a number!");
            return;
        }

        Console.WriteLine("Enter the operation (&, | or ^): ");
        var s = Console.ReadLine();

        if (s.Length == 0 || s.Length > 1)
        {
            Console.WriteLine("Wrong sign");
        }

        int result;
        switch (s[0])
        {
            case '&':
                result = a & b;
                break;

            case '|':
                result = a | b;
                break;

            case '^':
                result = a ^ b;
                break;

            default:
                result = 0;
                Console.WriteLine("Wrong sign");
                break;
        }
        
        Console.WriteLine($"The result is in decimal form: {result}");
        Console.WriteLine($"The result is in binary form: {Convert.ToString(result, 2)}");
        Console.WriteLine($"The result is in hexadecimal form: {Convert.ToString(result, 16)}");
    }
}