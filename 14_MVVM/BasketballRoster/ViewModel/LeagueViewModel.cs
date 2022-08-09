using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BasketballRoster.Model;

namespace BasketballRoster.ViewModel;


class LeagueViewModel
{
    public RosterViewModel JimmysTeam { get; }
    public RosterViewModel AnasTeam { get; }

    public LeagueViewModel()
    {
        var jimmysRoster = new Roster("The Bombers", GetBomberPlayers());
        var anasRoster = new Roster("The Amazin", GetAmazinPlayers());
        JimmysTeam = new RosterViewModel(jimmysRoster);
        AnasTeam = new RosterViewModel(anasRoster);
    }

    IEnumerable<Player>  GetBomberPlayers()
    {
        return new List<Player>()
        {
            new Player("Ana", 31, true),
            new Player("Lloyd", 23, true),
            new Player("Kathleen",6, true),
            new Player("Mike", 0, true),
            new Player("Joe", 42, true),
            new Player("Herb",32, false),
            new Player("Fingers",8, false)
        };
    }

    IEnumerable<Player> GetAmazinPlayers()
    {
        return new List<Player>()
        {
            new Player("Jimmy",42, true),
            new Player("Henry",11, true),
            new Player("Bob",4, true),
            new Player("Lucinda", 18, true),
            new Player("Kim", 16, true),
            new Player("Bertha", 23, false),
            new Player("Ed",21, false)
        };
    }

}
