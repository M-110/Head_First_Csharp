namespace BasketballRoster.ViewModel;

class PlayerViewModel
{
    public string Name { get; }
    public int Number { get; }

    public PlayerViewModel(string name, int number)
    {
        Name = name;
        Number = number;
    }

    public override string ToString() => $"{Name} (#{Number})";
}
