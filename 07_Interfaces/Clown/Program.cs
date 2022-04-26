class Program
{
    static void Main(string[] args)
    {
        IClown monsterClown = new ScaryScary("big red nose", 20);
        monsterClown.Honk();
        if (monsterClown is IScaryClown iScaryClown)
            iScaryClown.ScareAdults();
        monsterClown.Laugh();
        IClown.CarCapacity = 15;
        Console.WriteLine(IClown.ClownCarDescription());
        Console.ReadKey();
    }
}

interface IClown
{
    string FunnyThingIHave { get; }
    private static int carCapacity = 12;

    public static int CarCapacity
    {
        get => carCapacity;
        set
        {
            if (value > 10)
                carCapacity = value;
            else
                Console.Error.WriteLine($"Warning: {value} is too small of a car capacity");
        }
    }
    protected static Random random = new();

    public static string ClownCarDescription()
    {
        return $"A clown car with {random.Next(CarCapacity / 2, CarCapacity)} clowns";
    }
    void Honk();

    void Laugh()
    {
        Console.WriteLine("Hahahaha!");
    }
}

interface IScaryClown : IClown
{
    string ScaryThingIHave { get; }
    void ScareLittleChildren();

    void ScareAdults()
    {
        Console.WriteLine($@"I AM A SCARY CLOWN! 
Look at my {random.Next(4, 50)} sharp teeth!

Also...");
        ScareLittleChildren();
    }
}

class FunnyFunny : IClown
{
    readonly string funnyThingIHave;
    public string FunnyThingIHave => funnyThingIHave;

    public FunnyFunny(string funnyThingIHave)
    {
        this.funnyThingIHave = funnyThingIHave;
    }
    public void Honk()
    {
        Console.WriteLine($"Hello everyone, I have {FunnyThingIHave}");
    }
}

class ScaryScary : FunnyFunny, IScaryClown
{
    readonly int scaryThingsCount;

    public string ScaryThingIHave => $"{scaryThingsCount} spiders";

    public ScaryScary(string funnyThingIHave, int scaryThingsCount) : base(funnyThingIHave)
    {
        this.scaryThingsCount = scaryThingsCount;
    }
    
    public void ScareLittleChildren()
    {
        Console.WriteLine($"Boo! I got you! Look at my {ScaryThingIHave}!");
    }
}