class Program
{
    static void Main(string[] args)
    {
        Func<int, string> timesFour = (int i) => $"-> {i * 4} <-";

        int lineNumber = 0;
        Func<int, string, int, (int, int)> x = (int i, string s, int j) => (i + 1, j + 2);

        Action<string> writeLineNumber = (string s) => Console.WriteLine($"#{lineNumber++}: {s}");

        Enumerable.Range(1, 5).Select(timesFour).ToList().ForEach(writeLineNumber);
    }
}