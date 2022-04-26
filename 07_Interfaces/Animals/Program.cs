class Program
{
    static void Main(string[] args)
    {
        Animal[] animals =
        {
            new Wolf(false),
            new Hippo(),
            new Wolf(true),
            new Wolf(false),
            new Hippo()
        };

        foreach (var animal in animals)
        {
            animal.MakeNoise();
            if (animal is ISwimmer swimmer)
                swimmer.Swim();
            if (animal is IPackHunter packHunter)
                packHunter.HuntInPack();
            Console.WriteLine();
        }
        Console.ReadKey();
    }
}

abstract class Animal
{
    public abstract void MakeNoise();
}

interface ISwimmer
{
    void Swim();
}

interface IPackHunter
{
    void HuntInPack();
}

class Hippo : Animal, ISwimmer
{
    public override void MakeNoise()
    {
        Console.WriteLine("Chomp chomp");
    }

    public void Swim()
    {
        Console.WriteLine("Splish splash");
    }
}

abstract class Canine : Animal
{
    public bool BelongsToPack { get; protected set; } = false;
}

class Wolf : Canine, IPackHunter
{
    public Wolf(bool belongsToPack) => 
        BelongsToPack = belongsToPack;

    public override void MakeNoise() => 
        Console.WriteLine(BelongsToPack ? "I love my pack." : "I wish I had a pack");

    public void HuntInPack() =>
        Console.WriteLine(BelongsToPack ? "I'm hunting with my pack" : "I'm hunting alone :(");
}