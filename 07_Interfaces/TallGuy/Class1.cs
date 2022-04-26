using System;

interface INose
{
    int Ear();

    string Face { get; }
}

abstract class Picasso: INose
{
    string face;
    public virtual string Face => face;
    public abstract int Ear();

    public Picasso(string face)
    {
        this.face = face;
    }
}

class Clowns : Picasso
{
    public Clowns() : base("Clowns")
    {
    }

    public override int Ear() => 7;
}

class Acts : Picasso
{
    public Acts() : base("Acts")
    {
    }

    public override int Ear() => 5;
}

class Of2016 : Clowns
{
    public override string Face => "Of2016";

    public static void Main(string[] args)
    {
        string result = "";
        INose[] i = new INose [3];
        i[0] = new Acts();
        i[1] = new Clowns();
        i[2] = new Of2016();
        for (int x = 0; x < 3; x++)
            result += $"{i[x].Ear()} {i[x].Face}\n";
        Console.WriteLine(result);
        Console.ReadKey();
    }
}