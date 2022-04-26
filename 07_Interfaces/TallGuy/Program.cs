class Program
{
    static void Main(string[] args)
    {
        TallGuy tallGuy = new () {height = 76, name = "Joseph"};
        tallGuy.TalkAboutYourself();
        tallGuy.Honk();
        Console.WriteLine($"Tall guy has a {tallGuy.FunnyThingIHave}");

        Console.ReadKey();
    }
}

class TallGuy : IClown
{
    public string name;
    public int height;

    public void TalkAboutYourself()
    {
        Console.WriteLine($"My name is {name} and I'm {height} inches tall.");
    }

    public void Honk()
    {
        Console.WriteLine("Honk honk");
    }

    public string FunnyThingIHave { get; } = "flower";
}

interface IClown
{
    void Honk();

    string FunnyThingIHave { get; }
}