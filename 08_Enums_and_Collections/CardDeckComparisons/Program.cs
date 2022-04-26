namespace CardDeckComparisons;

class Program
{
    public static void Main(string[] args)
    {
        Random random = new();
        List<Card> cards = new List<Card>();
        for (int i = 0; i < 5; i++)
        {
            cards.Add(new Card((Suit)random.Next(4), (Value)random.Next(13)));
        }
        PrintCards(cards);
        var comparer = new CardComparerByValue();
        cards.Sort(comparer);
        Console.WriteLine("\nSorted:");
        PrintCards(cards);
        Console.ReadKey();
    }

    public static void PrintCards(List<Card> cards)
    {
        foreach (var card in cards)
            Console.WriteLine(card);
    }
}