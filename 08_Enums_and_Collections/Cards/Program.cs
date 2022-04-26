using Cards;


class Program
{
    static void Main(string[] args)
    {
        Random random = new();
        //for (var i = 0; i < 5; i++)
        //{
        //    var randomValue = (Values)random.Next(13);
        //    var randomSuit = (Suits)random.Next(4);
        //    Card myCard = new(randomValue, randomSuit);
        //    Console.WriteLine(myCard.Name);
        //}

        Deck deck = new();
        deck.PrintCards();

        Console.ReadKey();
    }
}

class Card
{
    public Values Value { get; set; }
    public Suits Suit { get; set; }
    public string Name => $"{Value} of {Suit}";

    public Card(Values value, Suits suit)
    {
        Value = value;
        Suit = suit;
    }
}

class Deck
{
    readonly List<Card> cards = new();

    public Deck()
    {
        for (var i = 0; i < 4; i++)
        for (var j = 0; j < 13; j++)
            cards.Add(new Card((Values) j, (Suits) i));
    }

    public void PrintCards()
    {
        foreach (var card in cards)
            Console.WriteLine(card.Name);
    }
}