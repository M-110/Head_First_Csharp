namespace Safe;

class Program
{
    public static void Main(string[] args)
    {
        SafeOwner owner = new();
        Safe safe = new();
        JewelThief jewelThief = new();
        jewelThief.OpenSafe(safe, owner);
        Console.ReadKey(true);
    }
}

class Safe
{
    string contents = "ancient artifact";
    string safeCombination = "12345";

    public string Open(string combination)
    {
        return combination == safeCombination ? contents : "";
    }

    public void PickLock(Locksmith lockpicker)
    {
        lockpicker.Combination = safeCombination;
    }
}

class SafeOwner
{
    string valuables = "";

    public void ReceiveContents(string safeContents)
    {
        valuables = safeContents;
        Console.WriteLine($"Thank you for returning my {valuables}!");
    }
}

class Locksmith
{
    public string Combination { private get; set; } = "";
    public void OpenSafe(Safe safe, SafeOwner owner)
    {
        safe.PickLock(this);
        var safeContents = safe.Open(Combination);
        ReturnContents(safeContents, owner);
    }

    protected virtual void ReturnContents(string safeContents, SafeOwner owner)
    {
        owner.ReceiveContents(safeContents);
    }
}

class JewelThief : Locksmith
{
    string stolenJewels = "";

    protected override void ReturnContents(string safeContents, SafeOwner owner)
    {
        stolenJewels = safeContents;
        Console.WriteLine($"I'm stealing the jewels! I stole {stolenJewels}!");
    }
}