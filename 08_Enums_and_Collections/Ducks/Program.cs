namespace Ducks;

class Program
{
    public static void Main(string[] args)
    {
        List<Duck> ducks = new()
        {
            new Duck() { Kind = KindOfDuck.Mallard, Size = 17 },
            new Duck() { Kind = KindOfDuck.Muscovy, Size = 18 },
            new Duck() { Kind = KindOfDuck.Loon, Size = 14 },
            new Duck() { Kind = KindOfDuck.Muscovy, Size = 11 },
            new Duck() { Kind = KindOfDuck.Mallard, Size = 14 },
            new Duck() { Kind = KindOfDuck.Loon, Size = 13 }
        };

        PrintDucks(ducks); 
        Console.WriteLine("Sorted:");
        // ducks.Sort();

        // Alternative: 
        var comparer = new DuckComparerBySize();
        ducks.Sort(comparer);

        PrintDucks(ducks);
        Console.ReadKey();
    }

    public static void PrintDucks(List<Duck> ducks)
    {
        foreach (Duck duck in ducks)
            Console.WriteLine($"{duck.Size} inch {duck.Kind}");
    }
}