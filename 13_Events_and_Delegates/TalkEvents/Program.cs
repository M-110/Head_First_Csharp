class Program
{
    static int count;

    static void Main(string[] args)
    {
        var talker = new Talker();
        while (true)
        {
            Console.Write("1 to chain SaySomething, 2 to chain SaySomethingElse, or a message: ");
            var line = Console.ReadLine();
            switch (line)
            {
                case "1":
                    Console.WriteLine("Adding SaySomething");
                    talker.TalkToMe += SaySomething;
                    break;
                case "2":
                    Console.WriteLine("Adding SaySomethingElse");
                    talker.TalkToMe += SaySomethingElse;
                    break;
                case "":
                    return;
                default:
                    count = 1;
                    Console.WriteLine("Raising the TalkToMe event");
                    talker.OnTalkToMe(line);
                    break;
            }
        }

        static void SaySomething(object sender, TalkEventArgs e)
        {
            Console.WriteLine($"Call #{++count}: I said something: {e.Message}");
        }

        static void SaySomethingElse(Object sender, TalkEventArgs e)
        {
            Console.WriteLine($"Call #{++count}: I said something else: {e.Message}");
        }
    }
}

class TalkEventArgs : EventArgs
{
    public string Message { get; }

    public TalkEventArgs(string message) => Message = message;
}

class Talker
{
    public event EventHandler<TalkEventArgs> TalkToMe;

    public void OnTalkToMe(string message) => TalkToMe?.Invoke(this, new TalkEventArgs(message));
}

