namespace HideAndSeek;

public class Opponent
{
    public string Name { get; }

    public Opponent(string name) => Name = name;

    public override string ToString() => Name;

    public void Hide()
    {
        var location = House.Entry;
        var moveCount = House.Random.Next(10, 50);
        for (var i = 0; i < moveCount; i++)
            location = House.RandomExit(location);
        while (location is not LocationWithHidingPlace)
            location = House.RandomExit(location);
        (location as LocationWithHidingPlace)?.Hide(this);
    }
}