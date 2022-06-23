namespace HideAndSeek;


public class SavedGame
{
    public string PlayerLocation { get; set; }
    public Dictionary<string, string> OpponentLocations { get; set; }
    public List<string> FoundOpponents { get; set; }
    public int Moves { get; set; }

    public SavedGame(string playerLocation,
        Dictionary<string, string> opponentLocations,
        List<string> foundOpponents, int moves)
    {
        PlayerLocation = playerLocation;
        OpponentLocations = opponentLocations;
        FoundOpponents = foundOpponents;
        Moves = moves;
    }
}
