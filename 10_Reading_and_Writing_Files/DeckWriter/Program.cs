using DeckWriter;

class Program
{
    public static void Main(string[] args)
    {
        var deck = new Deck();
        deck.Shuffle();
        deck.WriteToFile("cards.txt");

        var deck2 = new Deck("cards.txt");
        deck2.WriteToFile("cards2.txt");
    }
}