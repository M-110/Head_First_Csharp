namespace Paintball;

class Program
{
    static void Main(string[] args)
    {
        var numberOfBalls = ReadInt(20, "Number of balls");
        var magazineSize = ReadInt(16, "Magazine size");
        Console.Write("Loaded [false]: ");
        bool.TryParse(Console.ReadLine(), out var isLoaded);
        PaintballGun gun = new(numberOfBalls, magazineSize, isLoaded);
        while (true)
        {
            Console.WriteLine($"{gun.Balls} balls, {gun.BallsLoaded} loaded");
            if (gun.IsEmpty()) Console.WriteLine("WARNING: You're out of ammo.");
            Console.WriteLine("Space to shoot, r to reload, + to add ammo, q to quit");
            var key = Console.ReadKey(true).KeyChar;
            if (key == ' ') Console.WriteLine($"Shooting returned {gun.Shoot()}");
            else if (key == 'r') gun.Reload();
            else if (key == '+') gun.Balls += gun.MagazineSize;
            else if (key == 'q') return;
        }
    }

    static int ReadInt(int lastUsedValue, string prompt)
    {
        Console.Write(prompt + " [" + lastUsedValue + "]: ");
        var line = Console.ReadLine();
        if (int.TryParse(line, out var value))
        {
            Console.WriteLine(" using value " + value);
            return value;
        }
        else
        {
            Console.WriteLine(" using default value " + lastUsedValue);
            return lastUsedValue;
        }
    }
}

class PaintballGun
{
    public int MagazineSize { get; private set; } = 16;

    int balls = 0;

    public int BallsLoaded { get; private set; }
    public bool IsEmpty() => BallsLoaded == 0;

    public int Balls
    {
        get => balls;
        set
        {
            if (value > 0)
                balls = value;
            Reload();
        }
    }

    public PaintballGun(int balls, int magazineSize, bool loaded)
    {
        this.balls = balls;
        MagazineSize = magazineSize;
        if (!loaded) Reload();
    }

    public void Reload()
    {
        BallsLoaded = balls > MagazineSize ? MagazineSize : balls;
    }

    public bool Shoot()
    {
        if (BallsLoaded == 0) return false;
        BallsLoaded--;
        balls--;
        return true;
    }
}