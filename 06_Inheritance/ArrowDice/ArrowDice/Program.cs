namespace ArrowDice;

class Program
{
    static Random random = new();

    public static void Main(string[] args)
    {
        SwordDamage swordDamage = new(RollDice(3));
        ArrowDamage arrowDamage = new(RollDice(1));

        while (true)
        {
            Console.WriteLine("S for sword, A for arrow, anything else for quit: ");
            var weaponKey = char.ToUpper(Console.ReadKey().KeyChar);
            switch (weaponKey)
            {
                case 'S':
                    Console.WriteLine("0 for no magic/flaming, 1 for magic, 2 for flaming, " +
                                      "3 for both, anything else to quit: ");
                    var swordKey = Console.ReadKey(false).KeyChar;
                    swordDamage.Roll = RollDice(3);
                    swordDamage.Magic = swordKey is '1' or '3';
                    swordDamage.Flaming = swordKey is '2' or '3';
                    Console.WriteLine($"\nRolled {swordDamage.Roll} for {swordDamage.Damage} HP\n");
                    break;
                case 'A':
                    Console.WriteLine("0 for no magic/flaming, 1 for magic, 2 for flaming, " +
                                      "3 for both, anything else to quit: ");
                    var arrowKey = Console.ReadKey(false).KeyChar;
                    if (arrowKey is not ('0' or '1' or '2' or '3')) return;
                    arrowDamage.Roll = RollDice(1);
                    arrowDamage.Magic = arrowKey is '1' or '3';
                    arrowDamage.Flaming = arrowKey is '2' or '3';
                    Console.WriteLine($"\nRolled {arrowDamage.Roll} for {arrowDamage.Damage} HP\n");
                    break;
                default:
                    return;
            }
        }
    }

    static int RollDice(int numberOfDice)
    {
        var sum = 0;
        for (var i = 0; i < numberOfDice; i++)
            sum += random.Next(1, 7);
        return sum;
    }
}

class WeaponDamage
{
    int roll;
    bool flaming;
    bool magic;

    public WeaponDamage(int initialRoll)
    {
        Roll = initialRoll;
    }

    public int Damage { get; protected set; }

    public int Roll
    {
        get => roll;
        set { roll = value; CalculateDamage(); }
    }

    public bool Flaming
    {
        get => flaming;
        set { flaming = value; CalculateDamage(); }
    }

    public bool Magic
    {
        get => magic;
        set { magic = value; CalculateDamage(); }
    }

    protected virtual void CalculateDamage()
    {
        throw new NotImplementedException();
    }

}

class ArrowDamage : WeaponDamage
{
    const decimal BaseMultiplier = 0.35M;
    const decimal MagicMultiplier = 2.5M;
    const decimal FlameDamage = 1.25M;

    public ArrowDamage(int initialRoll) : base(initialRoll)
    {
    }

    protected override void CalculateDamage()
    {
        var baseDamage = Roll * BaseMultiplier;
        if (Magic) baseDamage *= MagicMultiplier;
        if (Flaming) baseDamage += FlameDamage;
        Damage = (int)Math.Ceiling(baseDamage);
    }
}

class SwordDamage : WeaponDamage
{
    public const decimal BaseMultiplier = 0.35M;
    public const decimal MagicMultiplier = 1.75M;
    public const decimal FlameDamage = 2.5M;
    public SwordDamage(int initialRoll) : base(initialRoll)
    {
    }

    protected override void CalculateDamage()
    {
        var baseDamage = Roll * BaseMultiplier;
        if (Magic) baseDamage *= MagicMultiplier;
        if (Flaming) baseDamage += FlameDamage;
        Damage = (int)Math.Ceiling(baseDamage);
    }

}