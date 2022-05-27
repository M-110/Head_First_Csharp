namespace GoFish;

public class GameState
{
    public readonly IEnumerable<Player> Players;
    public readonly IEnumerable<Player> Opponents;
    public readonly Player HumanPlayer;
    public bool GameOver { get; private set; } = false;
    public readonly Deck Stock;

    /// <summary>
    /// GameState constructor that creates players and deals first hands.
    /// </summary>
    /// <param name="humanName">Name of human player</param>
    /// <param name="opponentNames">Names of computer players</param>
    /// <param name="stock">Shuffled deck of cards to deal from</param>
    public GameState(string humanName, IEnumerable<string> opponentNames, Deck stock)
    {
        Stock = stock;
        HumanPlayer = new Player(humanName);
        Opponents = opponentNames.Select(name => new Player(name)).ToList();
        Players = new List<Player> {HumanPlayer}.Concat(Opponents).ToList();

        foreach (var player in Players)
            player.GetNextHand(stock);
    }

    /// <summary>
    /// Gets a random player, excluding current player.
    /// </summary>
    /// <param name="currentPlayer">Player to exclude from random choice</param>
    /// <returns>A random player</returns>
    public Player RandomPlayer(Player currentPlayer) => 
        Players
        .Where(player => player != currentPlayer)
        .Skip(Player.Random.Next(Players.Count() - 1))
        .First();

    /// <summary>
    /// Play a single player for a round.
    /// </summary>
    /// <param name="player">Player asking for a card</param>
    /// <param name="playerToAsk">Player being asked</param>
    /// <param name="valueToAskFor">Value to ask for</param>
    /// <param name="stock">Deck to draw cards from</param>
    /// <returns>Message describing the result</returns>
    public string PlayRound(Player player, Player playerToAsk, Value valueToAskFor, Deck stock)
    {
        var plural = valueToAskFor == Value.Six ? "es" : "s";
        var message = $"{player.Name} asked {playerToAsk.Name} for {valueToAskFor}{plural}" +
                      Environment.NewLine;
        var result = playerToAsk.DoYouHaveAny(valueToAskFor, stock).ToList();
        if (result.Any())
        {
            player.AddCardsAndPullOutBooks(result);
            message += $"{playerToAsk.Name} has {result.Count} {valueToAskFor} card{Player.S(result.Count)}"
                + Environment.NewLine;
        }
        else if (stock.Count == 0)
            message += "The stock is out of cards"
                + Environment.NewLine;
        else
        {
            player.DrawCard(stock);
            message += $"{player.Name} drew a card"
                + Environment.NewLine;
        }
        if (!player.Hand.Any())
        {
            player.GetNextHand(stock);
            message += $"{player.Name} ran out of cards, drew {player.Hand.Count()} from stock"
                + Environment.NewLine;
        }
        return message;
    }

    /// <summary>
    /// Checks if there are cards left and return the winner.
    /// </summary>
    /// <returns>String containing the winners' names</returns>
    public string CheckForWinner()
    {
        if (Players.Select(player => player.Hand.Count()).Sum() != 0)
            return "";
        var maxBooks = Players.Select(player => player.Books.Count()).Max();
        var winningPlayers = Players
            .Where(player => player.Books.Count() == maxBooks)
            .Select(player => player.Name).ToList();
        var plural = winningPlayers.Count > 1 ? "s are" : " is";
        var winners = string
            .Join(" and ", winningPlayers);
        return $"The winner{plural} {winners}" + Environment.NewLine;
    }
}
