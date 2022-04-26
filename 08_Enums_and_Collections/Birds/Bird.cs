namespace Birds;

class Bird
{
    public string Name { get; set; }

    public virtual void Fly(string destination)
    {
        Console.WriteLine($"{this} is flying to {destination}");
    }

    public override string ToString() => $"a bird named {Name}";

    public static void FlyAway(List<Bird> flock, string destination)
    {
        foreach (var bird in flock)
            bird.Fly(destination);
    }
}
