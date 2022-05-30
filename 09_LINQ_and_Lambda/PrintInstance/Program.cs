class Program
{
    static void Main(string[] args)
    {
        var listOfObjects = new List<PrintWhenGetting>();
        for (int i = 1; i < 5; i++)
            listOfObjects.Add(new PrintWhenGetting(i));

        Console.WriteLine("Set up the query");
        var result = from o in listOfObjects
                     select o.InstanceNumber;
        Console.WriteLine("Run the foreach");
        foreach(var n in result)
            Console.WriteLine($"Writing #{n}");
        Console.ReadKey();
    }
}

class PrintWhenGetting
{
    int instanceNumber;
    public int InstanceNumber
    {
        set { instanceNumber = value; }
        get 
        {
            Console.WriteLine($"Getting #{instanceNumber}");
            return instanceNumber;
        }
    }
    public PrintWhenGetting(int instanceNumber)
    {
        this.instanceNumber = instanceNumber;
    }
}