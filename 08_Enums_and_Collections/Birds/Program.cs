namespace Birds;

class Program
{
    public static void Main(string[] args)
    {
        List<Duck> ducks = new()
        {
            new Duck(17, KindOfDuck.Mallard),
            new Duck(18, KindOfDuck.Muscovy),
            new Duck(14, KindOfDuck.Loon),
            new Duck(11, KindOfDuck.Muscovy),
            new Duck(14, KindOfDuck.Mallard),
            new Duck(13, KindOfDuck.Loon),
        };
        IEnumerable<Bird> upcastDucks = ducks;
        Bird.FlyAway(upcastDucks.ToList(), "Minnesota");
        Console.
    }
}

