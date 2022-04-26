class Program
{
    static void Main(string[] args)
    {
        App app = new();
        app.Run();
    }
}

class App
{
    ShoeCloset shoeCloset = new();

    public void Run()
    {
        while (true)
        {
            shoeCloset.PrintShoes();
            Console.WriteLine("Press 'a' to add a shoe or 'r' to remove a shoe: ");
            char key = Console.ReadKey().KeyChar;

            if (key == 'a')
                AddShoe();
            else if (key == 'r')
                RemoveShoe();
            else
                return;
        }
    }

    void AddShoe()
    {
        Console.WriteLine("\nAdd a shoe");
        for (var i = 0; i < 6; i++)
            Console.WriteLine($"Press {i} to add a {(Style)i}");
        Console.Write($"Enter a style: ");
        if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out var style))
        {
            Console.Write("\nEnter the color: ");
            var color = Console.ReadLine();
            Shoe shoe = new((Style) style, color);
            shoeCloset.Add(shoe);
        }
    }

    void RemoveShoe()
    {
        Console.Write($"\nEnter shoe index to remove: ");
        if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out var index))
            shoeCloset.Remove(index);
    }

}

class Shoe
{
    public Style Style { get; private set; }
    public string Color { get; private set; }
    public string Description => $"A {Color} {Style}";

    public Shoe(Style style, string color)
    {
        Style = style;
        Color = color;
    }
}

class ShoeCloset
{
    List<Shoe> shoes = new();

    public void PrintShoes()
    {
        Console.WriteLine(shoes.Count == 0 ? "The shoe closet is empty." : "The shoe closet contains:");
        for (var i = 0; i < shoes.Count; i++)
            Console.WriteLine($"Shoe #{i}: {shoes[i].Description}");
    }

    public void Add(Shoe shoe)
    {
        shoes.Add(shoe);
    }

    public void Remove(int index)
    {
        if (index >= 0 && index < shoes.Count)
            shoes.RemoveAt(index);
    }

}

enum Style
{
    Sneaker,
    Loafer,
    Sandal,
    Flipflop,
    Wingtip,
    Clog
}