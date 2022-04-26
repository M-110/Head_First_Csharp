using System;

class App
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the game of Hi Lo!!");
        Console.WriteLine($"Guess a number between 1 and {HiLoGame.MAXIMUM}.");
        HiLoGame.Hint();
        while (HiLoGame.GetPot() > 0)
        {
            Console.WriteLine($"Press h for higher, l for lower, ? to buy a hint, " +
                $"or any other key to quit with {HiLoGame.GetPot()}.");
            char key = Console.ReadKey(true).KeyChar;
            if (key == 'h') HiLoGame.Guess(true);
            else if (key == 'l') HiLoGame.Guess(false);
            else if (key == '?') HiLoGame.Hint();
            else return;
        }
        Console.WriteLine("The pot is empty. Bye!");
    }
}

static class HiLoGame
{
    public const int MAXIMUM = 100;
    static Random random = new();
    static int currentNumber = random.Next(0, MAXIMUM);
    static int pot = 50;

    public static void Guess(bool guessHigher)
    {
        int nextNumber = random.Next(1, MAXIMUM + 1);
        if (guessHigher == (nextNumber >= currentNumber))
        {
            Console.WriteLine("You guessed right!");
            pot += 20;
        }
        else
        {
            Console.WriteLine("You guessed wrong!");
            pot -= 20;
        }
        currentNumber = nextNumber;
        Console.WriteLine($"The next number is {currentNumber}");
    }

    public static int GetPot() => pot;

    public static void Hint()
    {
        int half = MAXIMUM / 2;
        if (half > currentNumber)
            Console.WriteLine($"The number is at most {half}");
        else
            Console.WriteLine($"The number is at least {half}");
        pot -= 5;

    }

}