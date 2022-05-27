using System.Collections.Generic;

namespace Comic;

public class Program
{
    static public void Main(string[] Args)
    {
        IEnumerable<Comic> catalog = new List<Comic> 
        { 
            new Comic("Cat Man", 6),
            new Comic("Aquawoman", 1),
            new Comic("Wonder Dog", 54),
            new Comic("Moon Cat", 13)
        };

        IReadOnlyDictionary<int, decimal> prices = new Dictionary<int, decimal>
        {
            {6, 360M },
            {1, 1325M },
            {54, 25.34M },
            {13, 500M }
        };
        var x = from comic in catalog
                where prices[comic.Issue] > 400
                orderby prices[comic.Issue] descending
                select $"{comic} is woth {prices[comic.Issue]:c}";
        foreach(var c in x) Console.WriteLine(c);
   
        var comics = 
            catalog
            .Where(c => prices[c.Issue] > 400M)
            .OrderBy(c => -prices[c.Issue])
            .ToList();


        comics.ForEach(c => Console.WriteLine($"{c} is worth {prices[c.Issue]:c}"));
        Console.ReadKey();
    }
}

public class Comic
{
    public string Name { get; set; }
    public int Issue { get; set; }

    public Comic(string name, int issue)
    {
        Name = name;
        Issue = issue;
    }

    public override string ToString() => $"{Name} (Issue #{Issue})";
}