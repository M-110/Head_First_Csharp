namespace HideAndSeek;

public class LocationWithHidingPlace : Location
{
    public List<Opponent> hiddenOpponents = new();
    public string HidingPlace {get;}
    public LocationWithHidingPlace(string name, string description) : base(name)
    {
        HidingPlace = description;
    }

    public void Hide(Opponent opponent)
    {
        hiddenOpponents.Add(opponent);
    }

    public IEnumerable<Opponent> CheckHidingPlace()
    {
        var result = hiddenOpponents;
        hiddenOpponents = new List<Opponent>();
        return result;
    }
}