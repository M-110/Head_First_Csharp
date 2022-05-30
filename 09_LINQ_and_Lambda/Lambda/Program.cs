class Program
{
    static Random random => new();

    static double GetRandomDouble(int max) => max * random.NextDouble();

    static void PrintValue(double d) => Console.WriteLine(d);

    static void Main(string[] args)
    {
        var value = GetRandomDouble(100);
        PrintValue(value);
        Console.ReadKey();  
    }
}