using System.Diagnostics;

class EvilClone
{
    public static int CloneCount = 0;
    public int CloneID { get; } = ++CloneCount;

    public EvilClone() => Console.WriteLine($"Clone #{CloneID} is wreaking havoc!");

    ~EvilClone()
    {
        Console.WriteLine($"Clone #{CloneID} was destroyed");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var stopwatch = Stopwatch.StartNew();
        var clones = new List<EvilClone>();
        while (true)
        {
            switch (Console.ReadKey(true).KeyChar)
            {
                case 'a':
                    clones.Add(new EvilClone());
                    break;
                case 'c':
                    Console.WriteLine($"Clearing list at time {stopwatch.ElapsedMilliseconds}");
                    clones.Clear();
                    break;
                default:
                    Console.WriteLine($"Collecting at time {stopwatch.ElapsedMilliseconds}");
                    GC.Collect();
                    break;
            }
        }
    }
}