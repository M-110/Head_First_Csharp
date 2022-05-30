class Program
{
    static void Main(string[] args)
    {
        var deck = new Deck().Shuffle().Take(16);
        var grouped = GroupCards(deck);
        foreach (var group in grouped)
        {
            Console.WriteLine(@$"Group: {group.Key}
Count: {group.Count()}
Min: {group.Min()}
Max: {group.Max()}");
        }
        Console.ReadKey();
    }

    private static IOrderedEnumerable<IGrouping<Suit, Card>> GroupCards(IEnumerable<Card> deck)
    {
        return from card in deck
               group card by card.Suit into suitGroup
               orderby suitGroup.Key descending
               select suitGroup;
    }
}