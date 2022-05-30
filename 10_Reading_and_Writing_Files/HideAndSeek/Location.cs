namespace HideAndSeek;

public class Location
{
    /// <summary>
    /// Name of this location
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Exits out of this location
    /// </summary>
    public IDictionary<Direction, Location> Exits { get; } = new Dictionary<Direction, Location>();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">Name of location</param>
    public Location(string name) => Name = name;

    public override string ToString() => Name;

    /// <summary>
    /// Sequence of descriptions of exits sorted by direction
    /// </summary>
    public IEnumerable<string> ExitList => Exits.OrderBy(pair => pair.Key.ToString()).Select(pair => pair.Value.Name);


    /// <summary>
    /// Add an exit to this location
    /// </summary>
    /// <param name="direction">Direction to the connecting location</param>
    /// <param name="location">The connecting location to be added</param>
    public void AddExit(Direction direction, Location location)
    {
        if (Exits.TryGetValue(direction, out _)) return;

        var reverseDirection = (Direction)(- (int) direction);
        Exits[direction] = location;
        location.AddExit(reverseDirection, this);
    }
    

    /// <summary>
    /// Get exit from this location based on direction
    /// </summary>
    /// <param name="direction">Direction of the exit</param>
    /// <returns>The exit location if it exists, otherwise returns itself</returns>
    public Location GetExit(Direction direction) => Exits.ContainsKey(direction) ?  Exits[direction] : this;
}

