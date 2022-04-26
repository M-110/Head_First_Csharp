namespace CardAppBlazor;

class Deck : List<Card>
{
    static Random random = new();

    public Deck()
    {
        Reset();
    }


    public void Reset()
    {
        Clear();
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 13; j++)
                Add(new Card((Suit)i, (Value)j));
    }

    public Card Deal(int index)
    {
        throw new NotImplementedException();
    }

    public void Shuffle()
    {
        var shuffledDeck = new List<Card>();
        while (Count > 0)
        {
            var index = random.Next(Count);
            shuffledDeck.Add(this[index]);
            RemoveAt(index);
        }

        foreach (var card in shuffledDeck)
            Add(card);
    }

    public void Sort()
    {
        List<Card> sortedCards = new List<Card>(this);
        var comparer = new CardComparerByValue();
        sortedCards.Sort(comparer);
        Clear();
        foreach (var card in sortedCards)
            Add(card);
    }

}
