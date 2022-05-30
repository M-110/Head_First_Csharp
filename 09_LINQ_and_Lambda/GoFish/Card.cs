namespace GoFish;
public class Card : IComparable<Card>
{
    public Suit Suit { get; set; }
    public Value Value { get; set; }

    public Card(Suit suit, Value value)
    {
        Suit = suit;
        Value = value;
    }

    public override string ToString() => $"{Value} of {Suit}";

    public int CompareTo(Card? other)
    {
        return new CardComparerByValue().Compare(this, other);
    }
}

public enum Suit
{
    Diamonds,
    Hearts,
    Spades,
    Clubs
}

public enum Value
{
    Ace,
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
    King
}

public class CardComparerByValue : IComparer<Card>
{
    public int Compare(Card x, Card y)
    {
        return x.Value.CompareTo(y.Value);
    }
}

