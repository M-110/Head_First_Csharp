class Program
{
    static void Main(string[] args)
    {
        var ball = new Ball();
        var pitcher = new Pitcher(ball);
        var fan = new Fan(ball);

        while (true)
        {
            Console.Write("Enter a number for the angle: ");
            if (!int.TryParse(Console.ReadLine(), out var angle))
                return;
            Console.Write("Enter a number for the distance: ");
            if (!int.TryParse(Console.ReadLine(), out var distance))
                return;
            var ballEventArgs = new BallEventArgs(angle, distance);
            var bat = ball.GetNewBat();
            bat.HitBall(ballEventArgs);
        }
    }
}

class BallEventArgs : EventArgs
{
    public int Angle { get; private set; }
    public int Distance { get; private set; }

    public BallEventArgs(int angle, int distance)
    {
        Angle = angle;
        Distance = distance;
    }
}

class Ball
{
    public event EventHandler<BallEventArgs> BallInPlay;

    public Bat GetNewBat() => new(new(OnBallInPlay));

    protected void OnBallInPlay(BallEventArgs e) => BallInPlay?.Invoke(this, e);
}

class Pitcher
{
    int pitchNumber;

    public Pitcher(Ball ball) => ball.BallInPlay += Ball_BallInPlay;

    void Ball_BallInPlay(object? sender, BallEventArgs e)
    {
        pitchNumber++;

        if (e.Distance < 95 && e.Angle < 60)
            Console.WriteLine($"Pitch #{pitchNumber}: I caught the ball");
        else
            Console.WriteLine($"Pitch #{pitchNumber}: I covered first base");

    }
}

class Fan
{
    public Fan(Ball ball) => ball.BallInPlay += Ball_BallInPlay;

    void Ball_BallInPlay(object? sender, BallEventArgs e)
    {
        if (e.Distance > 400 & e.Angle > 30)
            Console.WriteLine("Fan: I'm getting out my glove to catch this home run!");
        else
            Console.WriteLine("Fans: Boo!! We want home runs!");
    }
}

delegate void BatCallback(BallEventArgs e);

class Bat
{
    BatCallback batCallback;

    public Bat(BatCallback batCallback)
    {
        this.batCallback = batCallback;
    }

    public void HitBall(BallEventArgs e) => batCallback?.Invoke(e);
}