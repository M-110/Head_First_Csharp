using System.Collections.Generic;
using System.Linq;

namespace BasketballRoster.Model;

class Roster
{
    public string TeamName { get; private set; }
    public readonly List<Player> _players = new();

    public IEnumerable<Player> Players => new List<Player>(_players);

    public Roster(string teamName, IEnumerable<Player> players)
    {
        TeamName = teamName;
        _players = players.ToList();
    }
}
