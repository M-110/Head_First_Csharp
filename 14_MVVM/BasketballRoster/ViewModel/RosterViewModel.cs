using BasketballRoster.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace BasketballRoster.ViewModel;

class RosterViewModel
{
    public string TeamName { get; private set; }
    public ObservableCollection<PlayerViewModel> Starters { get; private set; }
    public ObservableCollection<PlayerViewModel> Bench { get; private set; }

    Roster roster;

    public RosterViewModel(Roster roster)
    {
        this.roster = roster;
        TeamName = roster.TeamName;

        Starters = new();
        Bench = new();

        UpdateRosters();
    }

    void UpdateRosters()
    {
        roster.Players
            .Where(player => player.Starter)
            .Select(player => new PlayerViewModel(player.Name, player.Number))
            .ToList()
            .ForEach(player => Starters.Add(player));
        roster.Players
             .Where(player => !player.Starter)
             .Select(player => new PlayerViewModel(player.Name, player.Number))
             .ToList()
             .ForEach(player => Bench.Add(player));
    }
}
