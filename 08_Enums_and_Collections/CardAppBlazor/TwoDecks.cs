namespace CardAppBlazor;

public class TwoDecks
{
    Deck leftDeck = new();
    Deck rightDeck = new();
    
    public TwoDecks()
    {
        rightDeck.Clear();
    }

    public int LeftDeckCount => leftDeck.Count;
    public int RightDeckCount => rightDeck.Count;
    public int LeftCardSelected { get; set; }
    public int RightCardSelected { get; set; }
    public string LeftDeckCardName(int i) => leftDeck[i].ToString();
    public string RightDeckCardName(int i) => rightDeck[i].ToString();
    public void Shuffle() => leftDeck.Shuffle();
    public void Reset() => leftDeck.Reset();
    public void Clear() => rightDeck.Clear();
    public void Sort() => rightDeck.Sort();

    public void MoveCard(Direction direction)
    {
        if (direction == Direction.LeftToRight)
        {
            rightDeck.Add(leftDeck[LeftCardSelected]);
            leftDeck.RemoveAt(LeftCardSelected);
        }
        else
        {
            if (rightDeck.Count == 0) return;
            leftDeck.Add(rightDeck[RightCardSelected]);
            rightDeck.RemoveAt(RightCardSelected);
        }
    }
}
public enum Direction
{
    LeftToRight,
    RightToLeft
}

