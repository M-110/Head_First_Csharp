namespace MauiMatchGame;

public partial class MainPage : ContentPage
{
    List<string> animals = new() 
    {
        "🐯", "🐺", "🐷", "🐭", "🐸", "🐻", "🐶", "🐵", 
        "🐯", "🐺", "🐷", "🐭", "🐸", "🐻", "🐶", "🐵" 
    };

    Button selectedButton;
    
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
            ((Button)MainGrid.Children[i]).Text = animals[i];
    }

    void BlockClicked(object sender, EventArgs args)
    {
        var button = sender as Button;

        if (selectedButton is not null)
        {
            if (button.Text == selectedButton.Text)
            {
                button.Text = "X";
                selectedButton.Text = "X";
            }
            else
            {
                selectedButton = null;
            }
        }
        else
        {
            selectedButton = button;
        }
    }
}
