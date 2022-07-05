class Program
{
    static void Main(string[] args)
    {
        var firstLine = "No first line was read";
        try
        {
            var lines = File.ReadAllLines(args[0]);
            firstLine = (lines.Length > 0) ? lines[0] : "File was empty";
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not read lines from file: {ex}");
        }
        finally
        {
            Console.WriteLine(firstLine);
        }
    }
}