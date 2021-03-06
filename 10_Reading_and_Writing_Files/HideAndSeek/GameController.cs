using System.Text.Json;

namespace HideAndSeek;

public class GameController
{

    /// <summary>
    /// List of opponents to be found
    /// </summary>
    public List<Opponent> Opponents = new ()
    {
        new Opponent("Joe"),
        new Opponent("Bob"),
        new Opponent("Ana"),
        new Opponent("Owen"),
        new Opponent("Jimmy")
    };

    /// <summary>
    /// Private list of opponents that have been found
    /// </summary>
    public List<Opponent> foundOpponents = new List<Opponent>();

    /// <summary>
    /// Player's current location in the house
    /// </summary>
    public Location CurrentLocation { get; private set; }

    /// <summary>
    /// The number of moves the player has made
    /// </summary>
    public int MoveNumber { get; private set; } = 1;

    /// <summary>
    /// Returns true if game is over
    /// </summary>
    public bool GameOver => Opponents.Count() == foundOpponents.Count();

    /// <summary>
    /// Player's current status
    /// </summary>
    public string Status => LocationStatus() + HidingPlaceStatus() + FoundOpponentsStatus();

    public string LocationStatus()
    {
        return $"You are in the {CurrentLocation.Name}. You see the following exits:"
            + Environment.NewLine
            + string.Join(Environment.NewLine, CurrentLocation.Exits
                .OrderBy(exit => exit.Key.ToString())
                .Select(exit
                    => $" - the {exit.Value.Name} {DirectionPreposition(exit.Key)} {exit.Key}"));
    }

    string DirectionPreposition(Direction direction)
    {
        if (direction is Direction.Up or Direction.Down or Direction.In or Direction.Out)
            return "is";
        return "is to the";
    }

    string HidingPlaceStatus()
    {
        if (CurrentLocation is not LocationWithHidingPlace hidingPlace)
            return "";
        return Environment.NewLine + $"Someone could hide {hidingPlace.HidingPlace}";
    }

    string FoundOpponentsStatus()
    {
        if (!foundOpponents.Any())
            return Environment.NewLine + "You have not found any opponents";
        var plural = Opponents.Count != 1 ? "s" : "";
        var opponentsString = string.Join(", ", foundOpponents.Select(opponent => opponent.Name));
        return Environment.NewLine + $"You have found {foundOpponents.Count} of {Opponents.Count} opponent{plural}: {opponentsString}";
    }

    /// <summary>
    /// Prompt to display to the player
    /// </summary>
    public string Prompt => $"{MoveNumber}: Which direction do you want to go (or type 'check'): ";

    public GameController()
    {
        House.ClearHidingPlaces();
        foreach (var opponent in Opponents)
            opponent.Hide();
        CurrentLocation = House.Entry;
    }

    /// <summary>
    /// Move to the location in a direction
    /// </summary>
    /// <param name="direction">Direction to move</param>
    /// <returns>True if the player moved successfully</returns>
    public bool Move(Direction direction)
    {
        var newLocation = CurrentLocation.GetExit(direction);

        if (newLocation == CurrentLocation) return false;

        CurrentLocation = newLocation;
        return true;

    }

    /// <summary>
    /// Parse input from player and update the status
    /// </summary>
    /// <param name="input">Input to parse</param>
    /// <returns>Resulting command of the input</returns>
    public string ParseInput(string input)
    {
        if (input.ToLower().StartsWith("save"))
            return Save(input);
        if (input.ToLower().StartsWith("load"))
            return Load(input);
        MoveNumber += 1;
        if (input.Contains("check", StringComparison.CurrentCultureIgnoreCase))
            return CheckForHider();

        if (!Enum.TryParse<Direction>(input, true, out var direction))
            return "That's not a valid direction";

        var newLocation = CurrentLocation.GetExit(direction);

        if (newLocation == CurrentLocation)
            return "There's no exit in that direction";
        CurrentLocation = newLocation;
        return $"Moving {direction}";
    }

    string Save(string input)
    {
        var fileName = input.Replace("save ", "");
        var opponentLocations = new Dictionary<string, string>();
        foreach (var location in House._locations.OfType<LocationWithHidingPlace>())
            foreach (var opponent in location.hiddenOpponents)
                opponentLocations[opponent.Name] = location.Name;

        var savedGame = new SavedGame(
            CurrentLocation.Name,
            opponentLocations,
            foundOpponents.Select(opponent => opponent.Name).ToList(),
            MoveNumber
        );
        var jsonString = JsonSerializer.Serialize(savedGame);
        File.WriteAllText(fileName, jsonString);
        return $"Saved to file \"{fileName}\"";
    }

    string Load(string input)
    {
        var fileName = input.Replace("load ", "");
        var jsonString = File.ReadAllText(fileName);
        var loadedGame = JsonSerializer.Deserialize<SavedGame>(jsonString);


        House.ClearHidingPlaces();

        var newOpponentList = new List<Opponent>();
        var newFoundOpponentList = new List<Opponent>();
        foreach (var opponentName in loadedGame.FoundOpponents)
        {
            var newOpponent = new Opponent(opponentName);

            newOpponentList.Add(newOpponent);
            newFoundOpponentList.Add(newOpponent);
        }

        foreach (var opponentPair in loadedGame.OpponentLocations)
        {
            var newOpponent = new Opponent(opponentPair.Key);
            var location = House.GetLocationByName(opponentPair.Value) as LocationWithHidingPlace;
            location.hiddenOpponents.Add(newOpponent);
            newOpponentList.Add(newOpponent);
        }

        CurrentLocation = House.GetLocationByName(loadedGame.PlayerLocation);
        Opponents = newOpponentList;
        foundOpponents = newFoundOpponentList;
        MoveNumber = loadedGame.Moves;


        return $"Loaded from file \"{fileName}\"";
    }

    string CheckForHider()
    {
        if (CurrentLocation is not LocationWithHidingPlace hidingPlace)
            return $"There is no hiding place in the {CurrentLocation.Name}";
        var hiders = hidingPlace.CheckHidingPlace().ToList();
        foundOpponents.AddRange(hiders);
        var hidersCount = hiders.Count;
        var plural = hidersCount > 1 ? "s" : "";
        if (hidersCount > 0)
            return $"You found {hidersCount} opponent{plural} hiding {hidingPlace.HidingPlace}";
        return $"Nobody was hiding {hidingPlace.HidingPlace}";
    }
}