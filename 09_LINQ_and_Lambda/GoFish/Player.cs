using System.Linq;
namespace GoFish;

 public class Player
{
    public static Random Random = new();

    List<Card> hand = new();
    List<Value> books = new();

    /// <summary>
    /// The cards currently in the player's hand.
    /// </summary>
    public IEnumerable<Card> Hand => hand;

    /// <summary>
    /// The books the player has pulled out.
    /// </summary>
    public IEnumerable<Value> Books => books;

    public readonly string Name;

    /// <summary>
    /// Player constructor
    /// </summary>
    /// <param name="name"></param>
    public Player(string name) => Name = name;

    /// <summary>
    /// Player  constructor with setting initial deck
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cards">Cards to initialize hand with</param>
    public Player(string name, IEnumerable<Card> cards)
    {
        Name = name;
        hand.AddRange(cards);
    }

    /// <summary>
    /// Returns "s" if int s is not equal to 1
    /// </summary>
    /// <param name="s">Value to compare to 1</param>
    /// <returns>"s" or ""</returns>
    public static string S(int s) => s == 1 ? "" : "s";

    /// <summary>
    /// Returns the current status of the player: the number of cards and books
    /// </summary>
    /// <returns>"{Name} has {cardCount} card and {bookCount} books","</returns>
    public string Status => 
        $"{Name} has {Hand.Count()} card{S(Hand.Count())} and {Books.Count()} book{S(Books.Count())}"
    + Environment.NewLine;


    /// <summary>
    /// Gets up to five cards from the stock and adds them to the player's hand.
    /// </summary>
    /// <param name="stock">Stock to draw cards from</param>
    public void GetNextHand(Deck stock)
    {
        while (hand.Count < 5 && stock.Deal(0) is {}card)
            hand.Add(card);
    }

    /// <summary>
    /// Return any cards that match the given value. If no cards remain after,
    /// get the next hand from the deck.
    /// </summary>
    /// <param name="value">Value to match against</param>
    /// <param name="stock">Deck to draw next hand from</param>
    /// <returns>Cards in Hand that matched the value</returns>
    public IEnumerable<Card> DoYouHaveAny(Value value, Deck stock)
    {

        var cardsToReturn = Hand
            .Where(card => card.Value == value)
            .OrderBy(card => card.Suit);
        hand = hand.Where(card => card.Value != value).ToList();
        if (!Hand.Any())
            GetNextHand(stock);
        return cardsToReturn;
    }

    /// <summary>
    /// Add cards to hand and pull out any matching books
    /// </summary>
    /// <param name="cards">Cards to be added to hand</param>
    public void AddCardsAndPullOutBooks(IEnumerable<Card> cards)
    {
        hand.AddRange(cards);

        var matchingBooks = hand
             .GroupBy(card => card.Value)
             .Where(group => group.Count() == 4)
             .Select(group => group.Key);

        books.AddRange(matchingBooks);
        books.Sort();

        hand = hand.Where(card => !books.Contains(card.Value)).ToList();
    }

    /// <summary>
    /// Draw a single card from the stock and add it to the hand.
    /// </summary>
    /// <param name="stock">Stock to draw from</param>
    public void DrawCard(Deck stock)
    {
        if (stock.Deal(0) is {} card)
            hand.Add(card);
    }

    /// <summary>
    /// Get a random value from the hand.
    /// </summary>
    /// <returns>Randomly selected value from the player's current hand</returns>
    public Value RandomValueFromHand() => hand
        .OrderBy(card => card.Value)
        .Select(card => card.Value)
        .Skip(Player.Random.Next(hand.Count()))
        .First();

    public override string ToString() => Name;
}
