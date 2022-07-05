﻿class Guy
{
    public string Name { get; set; }
    public int Age { get; set; }
    public override string ToString() => $"a {Age}-year-old named {Name}";
}


class Program
{
    static void ModifyAnIntAndGuy(ref int valueRef, ref Guy guyRef)
    {
        valueRef += 10;
        guyRef.Name = "Bob";
        guyRef.Age = 37;
    }
    
    static void Main(string[] args)
    {
        var i = 1;
        var guy = new Guy() { Name = "Joe", Age = 26 };
        Console.WriteLine($"i is {i} and guy is {guy}");
        ModifyAnIntAndGuy(ref i, ref guy);
        Console.WriteLine($"i is {i} and guy is {guy}");
    }
}