class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Bird bird;
            Console.Write("\nPress P for pigeon, O for ostrich: ");
            var key = char.ToUpper(Console.ReadKey().KeyChar);
            if (key == 'P') bird = new Pigeon();
            else if (key == 'O') bird = new Ostrich();
            else return;
            Console.Write("\nHow many eggs should it lay? ");
            if (!int.TryParse(Console.ReadLine(), out var numberOfEggs)) return;
            Egg[] eggs = bird.LayEggs(numberOfEggs);
            foreach (var egg in eggs)
                Console.WriteLine(egg.Description);
        }
    }
}

class Bird
{
    public static Random Randomizer = new();

    public virtual Egg[] LayEggs(int numberOfEggs)
    {
        Console.Error.WriteLine("Bird.LayEggs shouldn't be called");
        return Array.Empty<Egg>();
    }
}

class Pigeon : Bird
{
    public override Egg[] LayEggs(int numberOfEggs)
    {
        var eggs = new Egg[numberOfEggs];
        for (var i = 0; i < eggs.Length; i++)
            eggs[i] = new Egg(Randomizer.NextDouble() * 2 + 1, "white");
        return eggs;
    }
}

class Ostrich : Bird
{
    public override Egg[] LayEggs(int numberOfEggs)
    {
        var eggs = new Egg[numberOfEggs];
        for (var i = 0; i < eggs.Length; i++)
            if (Randomizer.Next(4) == 0)
                eggs[i] = new BrokenEgg("speckled");
            else
                eggs[i] = new Egg(Randomizer.NextDouble() + 12, "speckled");
        return eggs;
    }
}

class Egg
{
    public double Size { get; }
    public string Color { get; }

    public Egg(double size, string color)
    {
        Size = size;
        Color = color;
    }

    public string Description => $"A {Size:0.0}cm {Color} egg";
}

class BrokenEgg : Egg
{
    public BrokenEgg(string color) : base(0, $"broken {color}")
    {
        Console.WriteLine("A bird laid a broken egg!");
    }
}