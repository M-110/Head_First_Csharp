
class Program
{
    static Random random = new();

    public static void Main(string[] args)
    {
        SwordDamage swordDamage = new(RollDice());

        while (true)
        {
            Console.WriteLine("0 for no magic/flaming, 1 for magic, 2 for flaming, " +
                       "3 for both, anything else to quit: ");
            var key = Console.ReadKey(false).KeyChar;
            if (key is not ('0' or '1' or '2' or '3')) return;

            swordDamage.Roll = RollDice();

            swordDamage.Magic = key is '1' or '3';
            swordDamage.Flaming = key is '2' or '3';
            Console.WriteLine($"\nRolled {swordDamage.Roll} for {swordDamage.Damage} HP\n");
        }
    }

    public static int RollDice()
    {
        return random.Next(1, 7) + random.Next(1, 7) + random.Next(1, 7);
    }
}

class SwordDamage
{
    public const int BaseDamage = 3;
    public const int FlameDamage = 2;

    int roll;
    bool flaming;
    bool magic;

    public SwordDamage(int initialRoll)
    {
        Roll = initialRoll;
    }

    public int Damage { get; private set; }

    public int Roll
    {
        get => roll;
        set
        {
            roll = value;
            CalculateDamage();
        }
    }

    public bool Flaming
    {
        get => flaming;
        set
        {
            flaming = value;
            CalculateDamage();
        }
    }

    public bool Magic
    {
        get => magic;
        set
        {
            magic = value;
            CalculateDamage();
        }
    }

    void CalculateDamage()
    {
        var magicMultiplier = Magic ? 1.75f : 1f;
        var flameDamage = Flaming ? FlameDamage : 0;
        Damage = (int)(Roll * magicMultiplier) + BaseDamage + flameDamage;
    }
}