namespace CardAppBlazor;

class Card
{
    public Suit Suit { get; set; }
    public Value Value { get; set; }

    public Card(Suit suit, Value value)
    {
        Suit = suit;
        Value = value;
    }

    public override string ToString() => $"{Value} of {Suit}";
}

enum Suit
{
    Hearts,
    Spades,
    Diamonds,
    Clubs
}

enum Value
{
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

class CardComparerByValue : IComparer<Card>
{
    public int Compare(Card x, Card y)
    {
        return x.Value.CompareTo(y.Value);
    }
}

