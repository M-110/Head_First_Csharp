
namespace Retirement;
class Program
{
    public static void Main(string[] args)
    {
        Dictionary<int, RetiredPlayer> retiredPlayers = new()
        {
            { 3, new RetiredPlayer("Babe Ruth", 1948) },
            { 4, new RetiredPlayer("Lou Gehrig", 1939) },
            { 5, new RetiredPlayer("Joe DiMaggio", 1952) },
            { 7, new RetiredPlayer("Micky Mantle", 1969) },
            { 42, new RetiredPlayer("Jackie Robinson", 1993) },
        };

        foreach (var jerseyNumber in retiredPlayers.Keys)
        {
            var player = retiredPlayers[jerseyNumber];
            Console.WriteLine($"{player.Name} #{jerseyNumber} retired in {player.YearRetired}");
        }
        Console.ReadKey();
    }
}

class RetiredPlayer
{
    public string Name { get; private set; }
    public int YearRetired { get; private set; }

    public RetiredPlayer(string name, int yearRetired)
    {
        Name = name;
        YearRetired = yearRetired;
    }
}