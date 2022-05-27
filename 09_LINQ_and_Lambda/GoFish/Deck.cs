using System.Collections.ObjectModel;

namespace GoFish;

public class Deck : ObservableCollection<Card>
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

    public Card? Deal(int index)
    {
        if (!this.Any())
            return null;
        var card = this[index];
        RemoveAt(index);
        return card;
    }

    public Deck Shuffle()
    {
        var shuffledDeck = new List<Card>();
        while (Count > 0)
        {
            var index = Player.Random.Next(Count);
            shuffledDeck.Add(this[index]);
            RemoveAt(index);
        }

        foreach (var card in shuffledDeck)
            Add(card);
        return this;
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
