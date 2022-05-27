using System.Collections.ObjectModel;

namespace DeckWriter;

class Deck : ObservableCollection<Card>
{
    static Random random = new();

    public Deck()
    {
        Reset();
    }

    public Deck(string file)
    {
        foreach (var line in System.IO.File.ReadLines(file))
        {
            var card_data = line.Split(" of ");
            Enum.TryParse<Value>(card_data[0], out var value);
            Enum.TryParse<Suit>(card_data[1], out var suit);
            Add(new Card(suit, value));
        }
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

    public void WriteToFile(string file)
    {
        using var writer = new StreamWriter(file);
        foreach (var card in this)
        {
            writer.WriteLine(card.ToString());
        }
    }

}

