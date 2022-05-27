using System.Net.Mail;

namespace GoFish;

public class GameController
{
    GameState gameState;
    public static Random random => new Random();
    public bool GameOver => gameState.GameOver;

    public Player HumanPlayer => gameState.HumanPlayer;
    public IEnumerable<Player> Opponents => gameState.Opponents;
    public string Status { get; private set; }

    /// <summary>
    /// Constructor for Game Controller
    /// </summary>
    /// <param name="humanPlayerName">Name of human player</param>
    /// <param name="computerPlayerNames">Names of computer players</param>
    public GameController(string humanPlayerName, IEnumerable<string> computerPlayerNames)
    {
        gameState = new GameState(humanPlayerName, computerPlayerNames, new Deck().Shuffle());
        var playerNames = string.Join(", ", gameState.Players);
        Status = $"Starting a new game with players {playerNames}";
    }

    /// <summary>
    /// Play the next round and end the game if there are no more cards.
    /// </summary>
    /// <param name="playerToAsk">Player to be asked of a card for</param>
    /// <param name="valueToAskFor">Value being asked for</param>
    public void NextRound(Player playerToAsk, Value valueToAskFor)
    {
        Status = gameState.PlayRound(HumanPlayer, playerToAsk, valueToAskFor, gameState.Stock);
        ComputerPlayersPlayerNextRound();
        Status += string.Join("", gameState.Players.Select(player => player.Status));
        Status += $"The stock has {gameState.Stock.Count} card{Player.S(gameState.Stock.Count)}"
                  + Environment.NewLine;
        Status += gameState.CheckForWinner()
                  + Environment.NewLine;
    }

    /// <summary>
    /// Simulate a round for a computer player.
    /// </summary>
    void ComputerPlayersPlayerNextRound()
    {
        while (true)
        {
            var playersWithCards = gameState.Opponents.Where(opponent => opponent.Hand.Any()).ToList();
            foreach (var computer in playersWithCards)
            {
                if (!computer.Hand.Any())
                    continue;
                var playerToAsk = gameState.RandomPlayer(computer);
                var valueToAskFor = computer.RandomValueFromHand();
                Status += gameState.PlayRound(computer, playerToAsk, valueToAskFor, gameState.Stock);
            }

            if (!playersWithCards.Any() || HumanPlayer.Hand.Any())
                break;
        }
    }

    /// <summary>
    /// Initialize a new game, keeping the player names. 
    /// </summary>
    public void NewGame()
    {
        Status = "Starting a new game";
        gameState = new GameState(
            gameState.HumanPlayer.Name,
            gameState.Opponents.Select(player => player.Name),
            new Deck().Shuffle());
    }
}
