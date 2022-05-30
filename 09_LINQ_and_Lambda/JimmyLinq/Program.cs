using System.Collections.Generic;

namespace JimmyLinq;
class Program
{
    public static readonly IEnumerable<Comic> catalog = new List<Comic>
    {
        new Comic("Cat Man", 6),
        new Comic("Aquawoman", 1),
        new Comic("Wonder Dog", 54),
        new Comic("Moon Cat", 13)
    };

    public static readonly IReadOnlyDictionary<int, decimal> prices = new Dictionary<int, decimal>
    {
        {6, 360M },
        {1, 1325M },
        {54, 800M },
        {13, 300M }
    };

    public static readonly IEnumerable<Review> Reviews = new[]
    {
        new Review(6, Critics.MuddyCritic, 37.6f),
        new Review(54, Critics.RottenTornadoes, 22.8f),
        new Review(1, Critics.RottenTornadoes, 84.2f),
        new Review(1, Critics.MuddyCritic, 98.1f),
        new Review(13, Critics.MuddyCritic, 89.4f)
    };

    static public void Main(string[] Args)
    {
        Console.WriteLine("GROUPED BY PRICE:");
        GroupComicsByPrice(catalog, prices);
        Console.WriteLine("\nREVIEWS:");
        GetReviews();
        Console.ReadKey();
    }

    private static void GroupComicsByPrice(IEnumerable<Comic> comics,
        IReadOnlyDictionary<int, decimal> prices)
    {
        var groups = ComicAnalyzer.GroupComicsByPrice(comics, prices);
        foreach(var group in groups)
        {
            Console.WriteLine($"{group.Key} comics:");
            foreach (var comic in group) 
                Console.WriteLine($"#{comic.Issue} {comic.Name}: {prices[comic.Issue]:c}");
        }
    }

    private static void GetReviews()
    {
        var reviews = ComicAnalyzer.GetReviews(Program.catalog, Program.Reviews);
        foreach (var review in reviews)
            Console.WriteLine(review);
    }
}

public static class ComicAnalyzer
{
    static PriceRange CalculatePriceRange(Comic comic, IReadOnlyDictionary<int, decimal> prices)
    {
        return prices[comic.Issue] < 500M ? PriceRange.Cheap : PriceRange.Expensive;
    }

    public static IEnumerable<IGrouping<PriceRange, Comic>> GroupComicsByPrice(
        IEnumerable<Comic> comics, IReadOnlyDictionary<int, decimal> prices)
    {
        return comics
            .OrderBy(comic => prices[comic.Issue])
            .GroupBy(comic => CalculatePriceRange(comic, prices));
        return from comic in comics
               orderby prices[comic.Issue]
               group comic by CalculatePriceRange(comic, prices) into priceGroup
               select priceGroup;
    }

    public static IEnumerable<string> GetReviews(IEnumerable<Comic> comics, IEnumerable<Review> reviews)
    {
        return comics
            .OrderBy(comic => comic.Issue)
            .Join(reviews,
                comic => comic.Issue,
                review => review.Issue,
                (comic, review) => 
                $"{review.Critic} rated #{comic.Issue} '{comic.Name}': {review.Score:0.00}");
        return from comic in comics
               orderby comic.Issue
               join review in reviews
               on comic.Issue equals review.Issue
               select $"{review.Critic} rated #{comic.Issue} '{comic.Name}': {review.Score:0.00}";
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

public class Review
{
    public int Issue { get; set; }
    public Critics Critic { get; set; }
    public float Score { get; set; }

    public Review(int issue, Critics critic, float score)
    {
        Issue = issue;
        Critic = critic;
        Score = score;
    }

    public override string ToString() => $"{Critic}: {Issue}, {Score}";
}

public enum PriceRange
{
    Cheap,
    Expensive
}

public enum Critics
{
    MuddyCritic,
    RottenTornadoes
}