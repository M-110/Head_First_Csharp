namespace CardDeckComparisons;

class CardComparerByValue : IComparer<Card>
{
    public int Compare(Card x, Card y)
    {
        return x.Value.CompareTo(y.Value);
    }
}
