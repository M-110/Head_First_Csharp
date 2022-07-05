public struct Dog
{
    public string Name { get; set; }
    public string Breed { get; set; }

    public Dog(string name, string breed)
    {
        Name = name;
        Breed = breed;
    }

    public void Speak()
    {
        Console.WriteLine($"My name is {Name} and I'm a {Breed}");
    }
}

public class Canine
{
    public string Name { get; set; }
    public string Breed { get; set; }

    public Canine(string name, string breed)
    {
        Name = name;
        Breed = breed;
    }

    public void Speak()
    {
        Console.WriteLine($"My name is {Name} and I'm a {Breed}");
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Canine spot = new("Spot", "pug");
        Canine bob = spot;

        bob.Name = "Spike";
        bob.Breed = "Beagle";

        spot.Speak();
        bob.Speak();


        Dog jake = new("Jake", "Poodle");
        Dog betty = jake;

        betty.Name = "Betty";
        betty.Breed = "pit bull";
        jake.Speak();
        betty.Speak();

        Console.ReadKey();
    }
}