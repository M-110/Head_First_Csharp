namespace MauiMatchGame;

public partial class MainPage : ContentPage
{
	private List<string> animals = new()
	{
		"🐯", "🐺", "🐷", "🐭", "🐸", "🐻", "🐶", "🐵",
		"🐯", "🐺", "🐷", "🐭", "🐸", "🐻", "🐶", "🐵"
	};

	string selectedAnimal = "";

	public MainPage()
	{
		InitializeComponent();
		SetupAnimals();
	}

	void SetupAnimals()
	{
		var random = new Random();
		animals = animals.OrderBy(_ => random.Next()).ToList();
		for (var i = 0; i < 16; i++)
			(MainGrid.Children[i] as Button).Text = animals[i];
	}

	void BlockClicked(object sender, EventArgs args)
	{
		var button = sender as Button;
		button.Text = "HELLO";

	}
}

