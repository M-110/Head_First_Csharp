#nullable enable

class Guy
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public override string ToString() => $"{Name}: {Description}";
}

class Program
{
    static void Main(string[] args)
    {
        Guy guy;
        guy = new Guy() {Description = "Hello"};
        var name = guy.Name ?? "missing name";
        guy.Name ??= "missing name";

        int? j = null;
        int x;
        x = j ?? 0;
        Console.WriteLine(x);

        Console.WriteLine(name);
    }
}