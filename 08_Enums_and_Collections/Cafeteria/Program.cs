namespace Cafeteria;

class Program
{
    const int NumberOfLumberjacks = 3;
    public static void Main(string[] args)
    { 
        var queue = new Queue<Lumberjack>();

        for (int i = 0; i < NumberOfLumberjacks; i++)
            queue.Enqueue(GetLumberjackFromUserInput(i));

        while (queue.Count > 0)
        {
            queue.Dequeue().EatFlapjacks();
        }
        Console.ReadKey();

    }

    static Lumberjack GetLumberjackFromUserInput(int i)
    {
       Console.Write($"\nName of Lumberjack #{i + 1}: ");
       var name = Console.ReadLine();
       Console.Write($"\nNumber of flapjacks? ");
       if (int.TryParse(Console.ReadLine(), out int number))
        return new Lumberjack(name, number);
       return GetLumberjackFromUserInput(i);
    }
}

class Lumberjack
{
    Random random = new();
    string Name { get; }
    Stack<Flapjack> flapjackStack = new();

    public Lumberjack(string name, int number)
    {
        Name = name;
        for (int i = 0; i < number; i++)
            TakeFlapjack((Flapjack)random.Next(4));
    }
    
    void TakeFlapjack(Flapjack flapjack) => flapjackStack.Push(flapjack);

    public void EatFlapjacks()
    {
        while (flapjackStack.Count > 0)
            EatFlapjack();
    }

    void EatFlapjack() => Console.WriteLine($"{Name} eats a {flapjackStack.Pop()} flapjack.");
}

enum Flapjack
{
    Crispy,
    Soggy,
    Browned,
    Banana
}